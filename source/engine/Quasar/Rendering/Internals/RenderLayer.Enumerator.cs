//-----------------------------------------------------------------------
// <copyright file="RenderLayer.Enumerator.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;

namespace Quasar.Rendering.Internals
{
    /// <summary>
    /// Render layer enumerator implementation.
    /// </summary>
    internal partial class RenderingLayer
    {
        /// <summary>
        /// Render layer enumerator.
        /// </summary>
        /// <seealso cref="IEnumerator{RenderLayer}" />
        public struct Enumerator : IEnumerator<RenderingLayer>
        {
            private readonly RenderingLayer[] renderLayers;
            private readonly LayerMask layerMask;
            private int index;


            /// <summary>
            /// Initializes a new instance of the <see cref="Enumerator" /> struct.
            /// </summary>
            /// <param name="renderLayers">The render layers.</param>
            /// <param name="layerMask">The layer mask.</param>
            public Enumerator(RenderingLayer[] renderLayers, LayerMask layerMask)
            {
                this.renderLayers = renderLayers;
                this.layerMask = layerMask;

                index = -1;
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
            }


            /// <summary>
            /// Gets the current render layer.
            /// </summary>
            public RenderingLayer Current { get; private set; }

            /// <summary>
            /// Gets the current value.
            /// </summary>
            object IEnumerator.Current => Current;

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            ///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.
            /// </returns>
            public bool MoveNext()
            {
                index++;
                while (index < renderLayers.Length)
                {
                    var renderLayer = renderLayers[index];
                    if (layerMask.HasFlag(renderLayer.LayerMask))
                    {
                        Current = renderLayer;
                        return true;
                    }

                    index++;
                }

                Current = null;
                return false;
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            public void Reset()
            {
                index = -1;
                Current = null;
            }
        }
    }
}
