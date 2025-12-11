//-----------------------------------------------------------------------
// <copyright file="IsEx.cs" company="Space Development">
//      Copyright (c) Space Development. All rights reserved.
// </copyright>
// <summary>
//     This file is subject to the terms and conditions defined in
//     file 'LICENSE.txt', which is part of this source code package.
// </summary>
// <author>Balazs Meszaros</author>
//-----------------------------------------------------------------------

using System;
using System.Globalization;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Quasar.Tests.Extensions
{
    /// <summary>
    /// Is extension class.
    /// </summary>
    public abstract class IsEx : Is
    {
        private class NearlyEqualToConstraint : Constraint
        {
            private readonly double expectedValue;
            private readonly double errorLimit;


            /// <summary>
            /// Initializes a new instance of the <see cref="NearlyEqualToConstraint" /> class.
            /// </summary>
            /// <param name="expectedValue">The expected value.</param>
            /// <param name="errorLimit">The error limit.</param>
            public NearlyEqualToConstraint(double expectedValue, double errorLimit)
            {
                ArgumentOutOfRangeException.ThrowIfNegative(errorLimit, nameof(errorLimit));

                this.expectedValue = expectedValue;
                this.errorLimit = errorLimit;
            }

            /// <summary>
            /// Applies the constraint to an actual value, returning a ConstraintResult.
            /// </summary>
            /// <typeparam name="TActual"></typeparam>
            /// <param name="actual">The value to be tested</param>
            /// <returns>
            /// A ConstraintResult
            /// </returns>
            public override ConstraintResult ApplyTo<TActual>(TActual actual)
            {
                if (actual is double doubleValue)
                {
                    return new ConstraintResult(this, actual, NearlyEqualTo(doubleValue));
                }
                else if (actual is float floatValue)
                {
                    return new ConstraintResult(this, actual, NearlyEqualTo(floatValue));
                }

                return new ConstraintResult(this, actual, false);
            }

            /// <summary>
            /// Gets the description.
            /// </summary>
            public override string Description => FormatDouble(expectedValue);


            private static string FormatDouble(double d)
            {
                if (Double.IsNaN(d) || Double.IsInfinity(d))
                {
                    return d.ToString();
                }
                else
                {
                    var s = d.ToString("G17", CultureInfo.InvariantCulture);
                    if (s.IndexOf('.') > 0)
                    {
                        return s + "d";
                    }

                    return s + ".0d";
                }
            }
            private bool NearlyEqualTo(double a)
            {
                if (expectedValue == 0.0)
                {
                    return Math.Abs(a) <= errorLimit;
                }

                return Math.Abs((a - expectedValue) / expectedValue) <= errorLimit;
            }
        }

        /// <summary>
        /// Determines whether the relative difference between the expected value and the actual value is not greater then the error limit.
        /// </summary>
        /// <param name="expectedValue">The expected value.</param>
        /// <param name="errorLimit">The error limit.</param>
        public static Constraint NearlyEqualTo(double expectedValue, double errorLimit = 1E-3)
        {
            return new NearlyEqualToConstraint(expectedValue, errorLimit);
        }

        /// <summary>
        /// Determines whether the relative difference between the expected value and the actual value is not greater then the error limit.
        /// </summary>
        /// <param name="expectedValue">The expected value.</param>
        /// <param name="errorLimit">The error limit.</param>
        public static Constraint NearlyEqualTo(float expectedValue, float errorLimit = 1E-3f)
        {
            return new NearlyEqualToConstraint(expectedValue, errorLimit);
        }
    }
}
