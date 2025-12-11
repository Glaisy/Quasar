using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace Quasar.Tests.Performance
{
    [TestFixture]
    [Ignore("Only for performance testing")]
    internal sealed class TransformPerformanceTests
    {
        private const int TransformCount = 10000;
        private const int LoopCount = 10000;
        private const double RootProbability = 0.15;
        private static readonly Vector3 PositionDelta = new Vector3(0.1f, 0.2f, -0.3f);
        private static readonly Vector3 ScaleDelta = new Vector3(0.0002f, 0.0001f, 0.00003f);
        private static readonly Quaternion RotationDelta = new Quaternion(1f, -1f, 0.5f, 0.7f).Normalize();
        private static int ExpectedResult = 195717;


        [Test]
        public void Performance_Legacy()
        {
            // arrange and act
            var result = Benchmark(
                Legacy.Transform.Root,
                (i, parent) => new Legacy.Transform(parent, i.ToString()),
                transform =>
                {
                    transform.LocalPosition += PositionDelta;
                    transform.LocalScale += ScaleDelta;
                    transform.LocalRotation *= RotationDelta;
                });

            // assert
            Assert.That(result, Is.EqualTo(ExpectedResult));
        }

        [Test]
        public void Performance_Quasar()
        {
            // arrange and act
            var result = Benchmark(
                (Transform)Transform.Root,
                (i, parent) => new Transform(parent, i.ToString()),
                transform =>
                {
                    transform.LocalPosition += PositionDelta;
                    transform.LocalScale += ScaleDelta;
                    transform.LocalRotation *= RotationDelta;
                });

            // assert
            Assert.That(result, Is.EqualTo(ExpectedResult));
        }


        private static int Benchmark<TTransform>(
            TTransform root,
            Func<int, TTransform, TTransform> factory,
            Action<TTransform> change)
            where TTransform : ITransform
        {
            // Arrange
            var random = new Random(0);
            var transforms = new List<TTransform>(TransformCount);
            for (var i = 0; i < TransformCount; i++)
            {
                TTransform parent;
                if (random.NextDouble() < RootProbability || i == 0)
                {
                    parent = root;
                }
                else
                {
                    parent = transforms[random.Next(i)];

                }

                var transform = factory(i, parent);
                transforms.Add(transform);
            }

            // act
            for (var i = 0; i < LoopCount; i++)
            {
                foreach (var transform in transforms)
                {
                    change(transform);
                }

                foreach (var transform in transforms)
                {
                    _ = transform.Position;
                    _ = transform.PositiveX;
                    _ = transform.Rotation;
                    _ = transform.Scale;
                }
            }

            // result
            var testTransform = transforms[TransformCount / 2];
            return (int)MathF.Round(testTransform.Position.Length + testTransform.Scale.Length + testTransform.Rotation.Z * 1000);
        }
    }
}
