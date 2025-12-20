using System;

using NUnit.Framework;

namespace Quasar.Collections.Tests
{
    [TestFixture]
    public unsafe class NativeArrayTests
    {
        [Test]
        public void Constructor_WithPositiveCapacity_InitializesCorrectly()
        {
            // Arrange
            var expectedCapacity = 10;

            NativeArray<int> sut = default;
            try
            {
                // Act
                sut = new NativeArray<int>(expectedCapacity);
                var result = *(int**)&sut;

                // Assert
                Assert.That(sut.Capacity, Is.EqualTo(expectedCapacity));
                Assert.That(sut.Length, Is.EqualTo(0));
                Assert.That(new IntPtr(result), Is.Not.EqualTo(IntPtr.Zero));
            }
            finally
            {
                sut.Dispose();
            }
        }

        [Test]
        public void Constructor_WithZeroCapacity_InitializesCorrectly()
        {
            // Arrange
            var expectedCapacity = 0;

            NativeArray<int> sut = default;
            try
            {
                // Act
                sut = new NativeArray<int>(expectedCapacity);
                var result = *(int**)&sut;

                // Assert
                Assert.That(sut.Capacity, Is.EqualTo(expectedCapacity));
                Assert.That(sut.Length, Is.EqualTo(0));
                Assert.That(new IntPtr(result), Is.EqualTo(IntPtr.Zero));
            }
            finally
            {
                sut.Dispose();
            }
        }

        [Test]
        public void Dispose_ReleasesBuffer()
        {
            // Arrange
            var sut = new NativeArray<int>(2);

            // Act
            sut.Dispose();
            var result = *(int**)&sut;

            // Assert
            Assert.That(new IntPtr(result), Is.EqualTo(IntPtr.Zero));
        }

        [Test]
        public void Indexer_SetAndGetValue_WorksCorrectly()
        {
            // Arrange
            NativeArray<int> sut = default;
            try
            {
                sut = new NativeArray<int>(5);
                sut.Expand(4);

                // Act
                sut[2] = 42;
                var result = sut[2];

                // Assert
                Assert.That(sut.Capacity, Is.EqualTo(5));
                Assert.That(sut.Length, Is.EqualTo(4));
                Assert.That(result, Is.EqualTo(42));
            }
            finally
            {
                sut.Dispose();
            }
        }

        [Test]
        public void Expand_IncreasesLengthAndCapacity()
        {
            // Arrange
            NativeArray<int> sut = default;
            try
            {
                sut = new NativeArray<int>(2);

                // Act
                sut.Expand(5);
                sut[4] = 42;
                var result = sut[4];

                // Assert
                Assert.That(sut.Capacity, Is.EqualTo(6)); // 2->3->4->6
                Assert.That(sut.Length, Is.EqualTo(5));
                Assert.That(result, Is.EqualTo(42));
            }
            finally
            {
                sut.Dispose();
            }
        }

        [Test]
        public void Shrink_DecreasesLength()
        {
            // Arrange
            NativeArray<int> sut = default;
            try
            {
                sut = new NativeArray<int>(5);
                sut.Expand(4);

                // Act
                sut.Shrink(2);

                // Assert
                Assert.That(sut.Capacity, Is.EqualTo(5));
                Assert.That(sut.Length, Is.EqualTo(2)); // 4->2
            }
            finally
            {
                sut.Dispose();
            }
        }

        [Test]
        public void Clear_ResetsLengthToZero()
        {
            // Arrange
            NativeArray<int> sut = default;
            try
            {
                sut = new NativeArray<int>(5);
                sut.Expand(3);

                // Act
                sut.Clear();

                // Assert
                Assert.That(sut.Capacity, Is.EqualTo(5));
                Assert.That(sut.Length, Is.EqualTo(0)); // 3->0
            }
            finally
            {
                sut.Dispose();
            }
        }

        [Test]
        public void GetData_ReturnsPointerToCorrectElement()
        {
            // Arrange
            NativeArray<int> sut = default;
            try
            {
                sut = new NativeArray<int>(5);
                sut.Expand(3);
                sut[1] = 99;

                // Act
                var result = *sut.GetData(1);

                // Assert
                Assert.That(result, Is.EqualTo(99));
            }
            finally
            {
                sut.Dispose();
            }
        }

#if QUASAR_BOUNDS_CHECKS
        [Test]
        public void GetData_WithNegativeIndex_Throws()
        {
            // Arrange
            NativeArray<int> sut = default;
            try
            {
                sut = new NativeArray<int>(5);

                // Act & assert
                Assert.Throws<ArgumentOutOfRangeException>(() => sut.GetData(-1));
            }
            finally
            {
                sut.Dispose();
            }
        }

        [Test]
        public void GetData_WithTooBigIndex_Throws()
        {
            // Arrange
            NativeArray<int> sut = default;
            try
            {
                sut = new NativeArray<int>(5);
                sut.Expand(3);

                // Act & assert
                Assert.Throws<ArgumentOutOfRangeException>(() => sut.GetData(3));
            }
            finally
            {
                sut.Dispose();
            }
        }
#endif

        [Test]
        public void Expand_WithNegativeCount_Throws()
        {
            // Arrange
            NativeArray<int> sut = default;
            try
            {
                sut = new NativeArray<int>(5);

                // Act & assert
                Assert.Throws<ArgumentOutOfRangeException>(() => sut.Expand(-1));
            }
            finally
            {
                sut.Dispose();
            }
        }

        [Test]
        public void Shrink_WithNegativeCount_Throws()
        {
            // Arrange
            NativeArray<int> sut = default;
            try
            {
                sut = new NativeArray<int>(5);

                // Act & assert
                Assert.Throws<ArgumentOutOfRangeException>(() => sut.Shrink(-1));
            }
            finally
            {
                sut.Dispose();
            }
        }

        [Test]
        public void Shrink_WithCountGreaterThanLength_Throws()
        {

            // Arrange
            NativeArray<int> sut = default;
            try
            {
                sut = new NativeArray<int>(5);
                sut.Expand(2);

                // Act & assert
                Assert.Throws<ArgumentOutOfRangeException>(() => sut.Shrink(3));
            }
            finally
            {
                sut.Dispose();
            }
        }
    }
}