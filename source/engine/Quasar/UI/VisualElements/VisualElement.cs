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
using System.Runtime.CompilerServices;

using Microsoft.Extensions.DependencyInjection;

using Quasar.UI.VisualElements.Styles;

using Space.Core;

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
        private static IUIContext context;


        /// <summary>
        /// Initializes a new instance of the <see cref="VisualElement"/> class.
        /// </summary>
        public VisualElement()
        {
            Name = default;
            Container = this;
            Invalidate(InvalidationFlags.All);
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

            ////// release class list
            ////if (classes != emptyClasses)
            ////{
            ////    poolProvider.StringListPool.Release(classes);
            ////}

            ////classes = null;

            ////// release styles
            ////poolProvider.StylePool.Release(mergedStyle);
            ////poolProvider.StylePool.Release(style);
            ////poolProvider.StylePool.Release(inlineStyle);
            ////mergedStyle = style = inlineStyle = null;

            ////// release column and row collections
            ////if (columns != emptyColumns)
            ////{
            ////    poolProvider.GridColumnListPool.Release(columns);
            ////}

            ////if (rows != emptyRows)
            ////{
            ////    poolProvider.GridRowListPool.Release(rows);
            ////}

            ////columns = null;
            ////rows = null;
        }


        private List<VisualElement> children = emptyChildren;
        /// <summary>
        /// Gets the list of first level child visual elements.
        /// </summary>
        public IReadOnlyList<VisualElement> Children => children;

        /// <summary>
        /// Gets or sets the container element which hold the children if this element.
        /// Usually this is the element itself.
        /// </summary>
        public VisualElement Container { get; protected set; }

        private DisplayMode display;
        /// <summary>
        /// Gets the display mode.
        /// </summary>
        public DisplayMode Display
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

        private string name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get => name;
            set => name = String.IsNullOrEmpty(value) ? GetType().Name : value;
        }

        /// <summary>
        /// Gets the parent visual element.
        /// </summary>
        public VisualElement Parent { get; private set; }

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

            Context.Validate();
            Container.AddChild(visualElement);
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

            Context.Validate();
            return Container.RemoveChild(visualElement);
        }


        /// <summary>
        /// Initializes the static services.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        internal static void InitializeStaticServices(IServiceProvider serviceProvider)
        {
            context = serviceProvider.GetRequiredService<IUIContext>();
        }


        /// <summary>
        /// Gets the context.
        /// </summary>
        protected IUIContext Context => context;



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
        /// Invalidates the parent's layout.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void InvalidateParentLayout()
        {
            Parent.Invalidate(InvalidationFlags.Layout);
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
    }
}
