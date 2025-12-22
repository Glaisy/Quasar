//-----------------------------------------------------------------------
// <copyright file="VisualElement.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using Microsoft.Extensions.DependencyInjection;

using Quasar.Graphics;
using Quasar.Graphics.Internals;
using Quasar.UI.VisualElements.Internals;
using Quasar.UI.VisualElements.Layouts;
using Quasar.UI.VisualElements.Layouts.Internals;
using Quasar.UI.VisualElements.Styles;
using Quasar.UI.VisualElements.Styles.Internals;
using Quasar.UI.VisualElements.Themes;
using Quasar.Utilities;

using Space.Core;
using Space.Core.Extensions;

namespace Quasar.UI.VisualElements
{
    /// <summary>
    /// Represents a basic UI visual element.
    /// </summary>
    /// <seealso cref="InvalidatableBase" />
    /// <seealso cref="IDisposable" />
    public partial class VisualElement : InvalidatableBase, IDisposable
    {
        private static readonly List<VisualElement> emptyChildren = new List<VisualElement>();
        private static readonly List<string> emptyClasses = new List<string>();
        private static readonly List<GridColumn> emptyColumns = new List<GridColumn>();
        private static readonly List<GridRow> emptyRows = new List<GridRow>();
        private readonly bool createCanvas;
        private static ILayoutManager[] layoutManagers;
        private static IStringOperationContext stringOperationContext;
        private static IUIContext uiContext;
        private static IPropertyValueParser propertyValueParser;
        private static IVisualElementEventProcessor eventProcessor;
        private static IThemeProvider themeProvider;
        private static IStyleBuilder styleBuilder;
        private static VisualElementContext context;
        private static Font defaultFont;
        private ILayoutManager layoutManager;
        private Vector2 textPosition;
        private PseudoClass pseudoClass;
        private Style mergedStyle;


        /// <summary>
        /// Initializes a new instance of the <see cref="VisualElement"/> class.
        /// </summary>
        public VisualElement()
            : this(false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VisualElement"/> class.
        /// </summary>
        /// <param name="createCanvas">if set to <c>true</c> [create canvas].</param>
        protected VisualElement(bool createCanvas)
        {
            this.createCanvas = createCanvas;

            Name = default;
            Container = this;
            layoutManager = layoutManagers[(int)LayoutType.VerticalStack];
            Invalidate(InvalidationFlags.All);

            eventProcessor.AddToLoadList(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="VisualElement"/> class.
        /// </summary>
        ~VisualElement()
        {
            Dispose(false);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            eventProcessor.RemoveFromLoadList(this);

            // invoke unload event handler
            OnUnload();

            // dispose children
            foreach (var child in children)
            {
                child.Dispose();
            }

            if (children != emptyChildren)
            {
                context.VisualElementListPool.Release(children);
            }

            children = null;

            // release class list
            if (classes != emptyClasses)
            {
                stringOperationContext.ListPool.Release(classes);
            }

            classes = null;

            // release styles
            context.StylePool.Release(mergedStyle);
            context.StylePool.Release(style);
            context.StylePool.Release(inlineStyle);
            mergedStyle = style = inlineStyle = null;

            // release column and row collections
            if (columns != emptyColumns)
            {
                context.GridColumnListPool.Release(columns);
            }

            if (rows != emptyRows)
            {
                context.GridRowListPool.Release(rows);
            }

            columns = null;
            rows = null;
        }


        private Sprite background;
        /// <summary>
        /// Gets the background.
        /// </summary>
        public Sprite Background
        {
            get => background;
            private set
            {
                if (background == value)
                {
                    return;
                }

                background = value;
                Invalidate(InvalidationFlags.Canvas);
            }
        }

        private Color backgroundColor;
        /// <summary>
        /// Gets the background color.
        /// </summary>
        public Color BackgroundColor
        {
            get => backgroundColor;
            private set
            {
                if (backgroundColor == value)
                {
                    return;
                }

                backgroundColor = value;
                Invalidate(InvalidationFlags.Canvas);
            }
        }

        /// <summary>
        /// Gets the visual element's bounding box (including content, padding and margin areas).
        /// Relative to the parent visual element's content box.
        /// </summary>
        public Rectangle BoundingBox { get; private set; }

        private List<VisualElement> children = emptyChildren;
        /// <summary>
        /// Gets the list of first level child visual elements.
        /// </summary>
        public IReadOnlyList<VisualElement> Children => children;

        private List<string> classes = emptyClasses;
        /// <summary>
        /// Gets the classes.
        /// </summary>
        public IReadOnlyList<string> Classes => classes;

        private Color color;
        /// <summary>
        /// Gets the color.
        /// </summary>
        public Color Color
        {
            get => color;
            private set
            {
                if (color == value)
                {
                    return;
                }

                color = value;
                Invalidate(InvalidationFlags.Canvas);
            }
        }

        private int column;
        /// <summary>
        /// Gets or sets the column index in a grid layout.
        /// </summary>
        public int Column
        {
            get => column;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value, nameof(Column));
                if (column == value)
                {
                    return;
                }

                column = value;
                InvalidateParentGridLayout();
            }
        }

        private List<GridColumn> columns = emptyColumns;
        /// <summary>
        /// Gets the column list.
        /// </summary>
        public IReadOnlyList<GridColumn> Columns => columns;

        private int columnSpan = 1;
        /// <summary>
        /// Gets or sets the column span in a grid layout.
        /// </summary>
        public int ColumnSpan
        {
            get => columnSpan;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value, nameof(ColumnSpan));
                if (columnSpan == value)
                {
                    return;
                }

                columnSpan = value;
                InvalidateParentGridLayout();
            }
        }

        /// <summary>
        /// Gets or sets the container element which hold the children if this element.
        /// Usually this is the element itself.
        /// </summary>
        public VisualElement Container { get; protected set; }

        /// <summary>
        /// Gets the visual element's context box.
        /// Relative to the visual element's padding box.
        /// </summary>
        public Rectangle ContentBox { get; private set; }

        private DisplayStyle display;
        /// <summary>
        /// Gets the display style.
        /// </summary>
        public DisplayStyle Display
        {
            get => display;
            private set
            {
                if (display == value)
                {
                    return;
                }

                display = value;
                InvalidateParentLayout();
            }
        }

        private Font font;
        /// <summary>
        /// Gets the font.
        /// </summary>
        public Font Font
        {
            get => font;
            private set
            {
                if (font == value)
                {
                    return;
                }

                font = value;
                Invalidate(InvalidationFlags.ContentAlignment | InvalidationFlags.PreferredSize);
            }
        }

        private Alignment horizontalAlignment;
        /// <summary>
        /// Gets the horizontal alignment of the content.
        /// </summary>
        public Alignment HorizontalAlignment
        {
            get => horizontalAlignment;
            private set
            {
                if (horizontalAlignment == value)
                {
                    return;
                }

                horizontalAlignment = value;
                Invalidate(InvalidationFlags.ContentAlignment);
            }
        }

        private Style inlineStyle;
        /// <summary>
        /// Gets the inline style.
        /// </summary>
        public IStyle InlineStyle => inlineStyle;

        private ItemAlignment horizontalItemAlignment;
        /// <summary>
        /// Gets the horizontal item alignment.
        /// </summary>
        public ItemAlignment HorizontalItemAlignment
        {
            get => horizontalItemAlignment;
            private set
            {
                if (horizontalItemAlignment == value)
                {
                    return;
                }

                horizontalItemAlignment = value;
                Invalidate(InvalidationFlags.Layout);
            }
        }

        private bool isEnabled = true;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="VisualElement"/> is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                if (isEnabled == value)
                {
                    return;
                }

                isEnabled = value;
                Invalidate(InvalidationFlags.PseudoClass);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this element has the input focus.
        /// </summary>
        public bool IsFocused { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="VisualElement"/> is visible.
        /// </summary>
        public bool IsVisible => visibility == Visibility.Visible && display == DisplayStyle.Display;

        private Vector2 itemSpacing;
        /// <summary>
        /// Gets the item spacing.
        /// </summary>
        public Vector2 ItemSpacing
        {
            get => itemSpacing;
            private set
            {
                if (itemSpacing == value)
                {
                    return;
                }

                itemSpacing = value;
                Invalidate(InvalidationFlags.Layout);
            }
        }

        /// <summary>
        /// Gets or sets the layout type.
        /// </summary>
        public LayoutType LayoutType
        {
            get => layoutManager.LayoutType;
            set
            {
                if (layoutManager.LayoutType == value)
                {
                    return;
                }

                layoutManager = layoutManagers[(int)value];
                Invalidate(InvalidationFlags.Layout);
            }
        }

        private Vector2 maximumSize;
        /// <summary>
        /// Gets the maximum size.
        /// </summary>
        public Vector2 MaximumSize
        {
            get => maximumSize;
            private set
            {
                if (maximumSize == value)
                {
                    return;
                }

                maximumSize = value;
                InvalidateParentLayout();
            }
        }

        private Vector2 minimumSize;
        /// <summary>
        /// Gets the minimum size.
        /// </summary>
        public Vector2 MinimumSize
        {
            get => minimumSize;
            private set
            {
                if (minimumSize == value)
                {
                    return;
                }

                minimumSize = value;
                InvalidateParentLayout();
            }
        }

        private string name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get => name;
            set => name = String.IsNullOrEmpty(value) ? GetType().Name : value;
        }

        private Overflow overflow;
        /// <summary>
        /// Gets the overflow.
        /// </summary>
        public Overflow Overflow
        {
            get => overflow;
            private set
            {
                if (overflow == value)
                {
                    return;
                }

                overflow = value;
                Invalidate(InvalidationFlags.Canvas);
            }
        }

        private OverflowClippingMode overflowClippingMode;
        /// <summary>
        /// Gets the overflow clipping mode.
        /// </summary>
        public OverflowClippingMode OverflowClippingMode
        {
            get => overflowClippingMode;
            private set
            {
                if (overflowClippingMode == value)
                {
                    return;
                }

                overflowClippingMode = value;
                Invalidate(InvalidationFlags.Canvas);
            }
        }

        /// <summary>
        /// Gets the parent visual element.
        /// </summary>
        public VisualElement Parent { get; private set; }

        /// <summary>
        /// Gets the padding box (content + padding area).
        /// Relative to the visual element's bounding box.
        /// </summary>
        public Rectangle PaddingBox { get; private set; }

        private bool pointerOver;
        /// <summary>
        /// Gets a value indicating whether the pointer cursor is over this element.
        /// </summary>
        public bool PointerOver
        {
            get => pointerOver;
            internal set
            {
                Assertion.ThrowIfNotEqual(pointerOver == value, false, nameof(PointerOver));

                pointerOver = value;
                Invalidate(InvalidationFlags.PseudoClass);

                if (value)
                {
                    OnPointerEnter();
                }
                else
                {
                    OnPointerLeave();
                }
            }
        }

        private Position position;
        /// <summary>
        /// Gets the position.
        /// </summary>
        public Position Position
        {
            get => position;
            private set
            {
                if (position == value)
                {
                    return;
                }

                position = value;
                InvalidateParentLayout();
            }
        }

        private Vector2 preferredSize;
        /// <summary>
        /// Gets the preferred size.
        /// </summary>
        public Vector2 PreferredSize
        {
            get => preferredSize;
            private set
            {
                if (preferredSize == value)
                {
                    return;
                }

                preferredSize = value;
                InvalidateParentLayout();
            }
        }

        private int row;
        /// <summary>
        /// Gets or sets the row index in a grid layout.
        /// </summary>
        public int Row
        {
            get => row;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value, nameof(Row));
                if (row == value)
                {
                    return;
                }

                row = value;
                InvalidateParentGridLayout();
            }
        }

        private List<GridRow> rows = emptyRows;
        /// <summary>
        /// Gets the row collection.
        /// </summary>
        public IReadOnlyList<GridRow> Rows => rows;

        private int rowSpan = 1;
        /// <summary>
        /// Gets or sets the row span in a grid layout.
        /// </summary>
        public int RowSpan
        {
            get => rowSpan;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value, nameof(RowSpan));
                if (rowSpan == value)
                {
                    return;
                }

                rowSpan = value;
                InvalidateParentGridLayout();
            }
        }

        private string text;
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public virtual string Text
        {
            get => text;
            set
            {
                if (text == value)
                {
                    return;
                }

                text = value;
                Invalidate(InvalidationFlags.PreferredSize | InvalidationFlags.ContentAlignment);
            }
        }

        private Alignment verticalAlignment;
        /// <summary>
        /// Gets the vertical alignment of the content.
        /// </summary>
        public Alignment VerticalAlignment
        {
            get => verticalAlignment;
            private set
            {
                if (verticalAlignment == value)
                {
                    return;
                }

                verticalAlignment = value;
                Invalidate(InvalidationFlags.ContentAlignment);
            }
        }

        private ItemAlignment verticalItemAlignment;
        /// <summary>
        /// Gets the vertical item alignment.
        /// </summary>
        public ItemAlignment VerticalItemAlignment
        {
            get => verticalItemAlignment;
            private set
            {
                if (verticalItemAlignment == value)
                {
                    return;
                }

                verticalItemAlignment = value;
                Invalidate(InvalidationFlags.Layout);
            }
        }

        private Visibility visibility;
        /// <summary>
        /// Gets the visibility.
        /// </summary>
        public Visibility Visibility
        {
            get => visibility;
            private set
            {
                if (visibility == value)
                {
                    return;
                }

                visibility = value;
                Invalidate(InvalidationFlags.Canvas);
            }
        }


        /// <summary>
        /// Adds the specified visual element to the Container's child controls.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        public void Add(VisualElement visualElement)
        {
            ArgumentNullException.ThrowIfNull(visualElement, nameof(visualElement));
            if (visualElement.Parent != null)
            {
                throw new UIException($"#{visualElement.name} visual element already has a parent visual element: #{visualElement.Parent.name}");
            }

            UIContext.Validate();
            Container.AddChild(visualElement);
        }

        /// <summary>
        /// Adds a new column to the layout definition.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="type">The unit type.</param>
        public void AddColumn(float width, GridLengthType type)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(width, nameof(width));
            uiContext.Validate();

            if (columns == emptyColumns)
            {
                columns = context.GridColumnListPool.Allocate();
            }

            var column = context.GridColumnPool.Allocate();
            column.DefinedWidth = new GridLength(width, type);
            columns.Add(column);

            if (LayoutType == LayoutType.Grid)
            {
                Invalidate(InvalidationFlags.Layout);
            }
        }

        /// <summary>
        /// Adds to style class to the visual element's class list.
        /// </summary>
        /// <param name="class">The class.</param>
        public void AddToClassList(string @class)
        {
            ArgumentException.ThrowIfNullOrEmpty(@class, nameof(@class));
            uiContext.Validate();

            if (classes.Contains(@class))
            {
                return;
            }

            if (classes == emptyClasses)
            {
                classes = stringOperationContext.ListPool.Allocate();
            }

            classes.Add(@class);
            Invalidate(InvalidationFlags.Styles);
        }

        /// <summary>
        /// Adds a new row to the layout definition.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <param name="type">The unit type.</param>
        public void AddRow(float height, GridLengthType type)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height, nameof(height));
            uiContext.Validate();

            if (rows == emptyRows)
            {
                rows = context.GridRowListPool.Allocate();
            }

            var row = context.GridRowPool.Allocate();
            row.DefinedHeight = new GridLength(height, type);
            rows.Add(row);

            if (LayoutType == LayoutType.Grid)
            {
                Invalidate(InvalidationFlags.Layout);
            }
        }

        /// <summary>
        /// Grabs the input focus by the control.
        /// </summary>
        /// <returns>True if the control got the input focus; otherwise false.</returns>
        public bool Focus()
        {
            if (!IsFocusable ||
                !IsVisible ||
                !isEnabled)
            {
                return false;
            }

            eventProcessor.ProcessFocusChanged(this);
            return true;
        }

        /// <summary>
        /// Queries the visual element hierarchy to tries to find a visual elemet of the specified type and name.
        /// If the name is null or empty the fitst matching (by type) visual element is returned.
        /// </summary>
        /// <typeparam name="T">Visual element type.</typeparam>
        /// <param name="name">The name.</param>
        /// <returns>The matching visual element or null if there is no match.</returns>
        public T Q<T>(string name = null)
            where T : VisualElement
        {
            uiContext.Validate();

            // try to find among top level children
            foreach (var visualElement in Container.children)
            {
                if (visualElement is not T)
                {
                    continue;
                }

                if (name == null ||
                   visualElement.Name == name)
                {
                    return (T)visualElement;
                }
            }

            // recursive search
            foreach (var visualElement in Container.children)
            {
                var result = visualElement.Q<T>(name);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Removes the specified visual element from the Container's child controls.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        /// <returns>True if found among children and removed; otherwise false.</returns>
        public bool Remove(VisualElement visualElement)
        {
            ArgumentNullException.ThrowIfNull(visualElement, nameof(visualElement));
            if (visualElement.Parent != this)
            {
                throw new UIException($"#{visualElement.name} visual element does not belong to the visual element: #{name}");
            }

            UIContext.Validate();
            return Container.RemoveChild(visualElement);
        }

        /// <summary>
        /// Removes the column at the index.
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveColumn(int index)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, columns.Count, nameof(index));
            uiContext.Validate();

            var column = columns[index];
            columns.RemoveAt(index);
            context.GridColumnPool.Release(column);

            if (LayoutType == LayoutType.Grid)
            {
                Invalidate(InvalidationFlags.Layout);
            }
        }

        /// <summary>
        /// Removes the specified class from the element's class list.
        /// </summary>
        /// <param name="class">The class.</param>
        public void RemoveFromClassList(string @class)
        {
            ArgumentException.ThrowIfNullOrEmpty(@class, nameof(@class));
            uiContext.Validate();

            classes.Remove(@class);
            Invalidate(InvalidationFlags.Styles);
        }

        /// <summary>
        /// Removes the row at the index.
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveRow(int index)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, rows.Count, nameof(index));
            uiContext.Validate();

            var row = rows[index];
            rows.RemoveAt(index);
            context.GridRowPool.Release(row);

            if (LayoutType == LayoutType.Grid)
            {
                Invalidate(InvalidationFlags.Layout);
            }
        }

        /// <summary>
        /// Sets the column width.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="width">The width.</param>
        /// <param name="type">The type.</param>
        public void SetColumn(int index, float width, GridLengthType type)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, columns.Count, nameof(index));
            ArgumentOutOfRangeException.ThrowIfNegative(width, nameof(width));
            uiContext.Validate();

            columns[index].DefinedWidth = new GridLength(width, type);
            if (LayoutType == LayoutType.Grid)
            {
                Invalidate(InvalidationFlags.Layout);
            }
        }

        /// <summary>
        /// Sets the row height.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="height">The height.</param>
        /// <param name="type">The type.</param>
        public void SetRow(int index, float height, GridLengthType type)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, rows.Count, nameof(index));
            ArgumentOutOfRangeException.ThrowIfNegative(height, nameof(height));
            uiContext.Validate();

            rows[index].DefinedHeight = new GridLength(height, type);
            if (LayoutType == LayoutType.Grid)
            {
                Invalidate(InvalidationFlags.Layout);
            }
        }


        /// <summary>
        /// Gets the absolute position (screen).
        /// </summary>
        internal Vector2 AbsolutePosition { get; private set; }

        /// <summary>
        /// Gets the canvas position.
        /// </summary>
        internal Vector2 CanvasPosition { get; private set; }

        private Style style;
        /// <summary>
        /// Gets the merged style of the visual element.
        /// </summary>
        internal IStyle Style => style;


        /// <summary>
        /// Initializes the static services.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        internal static void InitializeStaticServices(IServiceProvider serviceProvider)
        {
            context = serviceProvider.GetRequiredService<VisualElementContext>();
            stringOperationContext = serviceProvider.GetRequiredService<IStringOperationContext>();
            uiContext = serviceProvider.GetRequiredService<IUIContext>();
            propertyValueParser = serviceProvider.GetRequiredService<IPropertyValueParser>();
            eventProcessor = serviceProvider.GetRequiredService<IVisualElementEventProcessor>();
            themeProvider = serviceProvider.GetRequiredService<IThemeProvider>();
            styleBuilder = serviceProvider.GetRequiredService<IStyleBuilder>();

            layoutManagers = serviceProvider.GetServices<ILayoutManager>()
                .OrderBy(x => x.LayoutType)
                .ToArray();

            defaultFont = new Font(FontFamilyConstants.DefaultName, FontFamilyConstants.DefaultFontSize);
        }

        /// <summary>
        /// Sets the inline style from the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        internal void SetInlineStyle(Style value)
        {
            inlineStyle = value;
        }

        /// <summary>
        /// Sets the property value by name and string representation.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        internal void SetProperty(string name, string value)
        {
            SetProperty(name, value, propertyValueParser);
        }


        /// <summary>
        /// Gets the element name selector.
        /// </summary>
        protected virtual string ElementTagSelector { get; } = ElementTagSelectors.VisualElement;

        /// <summary>
        /// Gets a value indicating whether this control is focusable.
        /// </summary>
        protected virtual bool IsFocusable => true;

        /// <summary>
        /// Gets the UI context.
        /// </summary>
        protected IUIContext UIContext => uiContext;


        /// <summary>
        /// Adds visual element to the back of the child collection.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        protected void AddChild(VisualElement visualElement)
        {
            Assertion.ThrowIfNull(visualElement, nameof(visualElement));

            if (children == emptyChildren)
            {
                children = context.VisualElementListPool.Allocate();
            }

            children.Add(visualElement);
            visualElement.Parent = this;
        }

        /// <summary>
        /// Invalidates the parent's grid layout.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void InvalidateParentGridLayout()
        {
            if (Parent == null ||
                Parent.LayoutType != LayoutType.Grid)
            {
                return;
            }

            Parent.Invalidate(InvalidationFlags.Layout);
        }

        /// <summary>
        /// Invalidates the parent's layout.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void InvalidateParentLayout()
        {
            Parent?.Invalidate(InvalidationFlags.Layout);
        }

        /// <summary>
        /// Removes the visual element from the child collection.
        /// </summary>
        /// <param name="visualElement">The visual element.</param>
        /// <returns>True if found among children and removed; otherwise false.</returns>
        protected bool RemoveChild(VisualElement visualElement)
        {
            Assertion.ThrowIfNull(visualElement, nameof(visualElement));

            if (children.Remove(visualElement))
            {
                visualElement.Parent = null;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="valueParser">The value parser.</param>
        protected virtual void SetProperty(string name, string value, IPropertyValueParser valueParser)
        {
            switch (name)
            {
                case nameof(Classes):
                    if (String.IsNullOrEmpty(value))
                    {
                        break;
                    }

                    List<string> splitBuffer = null;
                    try
                    {
                        splitBuffer = stringOperationContext.ListPool.Allocate();
                        value.Split(
                            splitBuffer,
                            StyleConstants.ClassSeparator,
                            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                        if (splitBuffer.Count == 0)
                        {
                            break;
                        }

                        AddToClassListInteral(splitBuffer);
                        Invalidate(InvalidationFlags.Styles);
                    }
                    finally
                    {
                        stringOperationContext.ListPool.Release(splitBuffer);
                    }

                    break;

                case nameof(Column):
                    Column = valueParser.ParseInt32(value);
                    break;

                case nameof(Columns):
                    if (columns == emptyColumns)
                    {
                        columns = context.GridColumnListPool.Allocate();
                    }

                    valueParser.ParseGridColumns(columns, value);
                    Invalidate(InvalidationFlags.Layout);
                    break;

                case nameof(ColumnSpan):
                    ColumnSpan = valueParser.ParseInt32(value);
                    break;

                case "Enabled":
                    IsEnabled = valueParser.ParseBoolean(value);
                    break;

                case nameof(LayoutType):
                    LayoutType = valueParser.ParseEnum<LayoutType>(value);
                    break;

                case nameof(Row):
                    Row = valueParser.ParseInt32(value);
                    break;

                case nameof(Rows):
                    if (rows == emptyRows)
                    {
                        rows = context.GridRowListPool.Allocate();
                    }

                    valueParser.ParseGridRows(rows, value);
                    Invalidate(InvalidationFlags.Layout);
                    break;

                case nameof(RowSpan):
                    RowSpan = valueParser.ParseInt32(value);
                    break;

                case nameof(Text):
                    Text = value;
                    break;
            }
        }


        private void AddToClassListInteral(IEnumerable<string> classesToAdd)
        {
            if (classes == emptyClasses)
            {
                classes = stringOperationContext.ListPool.Allocate();
            }

            var classListChanged = false;
            foreach (var @class in classesToAdd)
            {
                if (classes.Contains(@class))
                {
                    continue;
                }

                classListChanged = true;
                classes.Add(@class);
            }

            if (!classListChanged)
            {
                return;
            }

            Invalidate(InvalidationFlags.Styles);
        }
    }
}
