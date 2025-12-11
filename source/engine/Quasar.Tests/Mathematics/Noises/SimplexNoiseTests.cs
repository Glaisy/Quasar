using NUnit.Framework;

using Quasar.Mathematics.Noises;
using Quasar.Tests.Extensions;
using Quasar.Tests.Mathematics.Noises.Legacy;

namespace Quasar.Tests.Mathematics.Noises
{
    [TestFixture]
    internal class SimplexNoiseTests
    {
        private const double DerivativeLimit = 2.1;

        [Test]
        public void Sample1D()
        {
            // arrange
            const int N = 100;
            var delta = 0.23231234;
            var x = 0.6573324;
            // arrange and act
            for (var i = 0; i < N; i++, x += delta)
            {
                var old = SimplexNoiseLegacy.Sample(x);
                var value = SimplexNoise.Sample(x);
                var withDerivative = SimplexNoise.SampleWithDerivative(x);

                Assert.That(AssertExtensions.EqualTo(old, value), Is.True);
                Assert.That(AssertExtensions.EqualTo(old, withDerivative.X), Is.True);
                Assert.That(withDerivative.Y, Is.LessThanOrEqualTo(DerivativeLimit));
                Assert.That(withDerivative.Y, Is.GreaterThanOrEqualTo(-DerivativeLimit));
            }
        }

        [Test]
        public void Sample2D()
        {
            // arrange
            const int N = 100;
            var deltaX = 0.23231234;
            var deltaY = -0.11023466556;
            var x = 0.6573324;
            var y = 0.9233323;
            // arrange and act
            for (var i = 0; i < N; i++, x += deltaX, y += deltaY)
            {
                var old = SimplexNoiseLegacy.Sample(x, y);
                var value = SimplexNoise.Sample(x, y);
                var withDerivative = SimplexNoise.SampleWithDerivatives(x, y);

                Assert.That(AssertExtensions.EqualTo(old, value), Is.True);
                Assert.That(AssertExtensions.EqualTo(old, withDerivative.X), Is.True);
                Assert.That(withDerivative.Y, Is.LessThanOrEqualTo(DerivativeLimit));
                Assert.That(withDerivative.Y, Is.GreaterThanOrEqualTo(-DerivativeLimit));
                Assert.That(withDerivative.Z, Is.LessThanOrEqualTo(DerivativeLimit));
                Assert.That(withDerivative.Z, Is.GreaterThanOrEqualTo(-DerivativeLimit));
            }
        }
    }
}
