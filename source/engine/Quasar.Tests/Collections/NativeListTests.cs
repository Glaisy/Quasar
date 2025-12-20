using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace Quasar.Collections.Tests
{
    [TestFixture]
    public unsafe class NativeListTests
    {
        [Test]
        public void Constructor_WithPositiveCapacity_InitializesCorrectly()
        {
            // Arrange
            var expectedCapacity = 10;

            NativeList<int> sut = default;
            try
            {
                // Act
                sut = new NativeList<int>(expectedCapacity);
                var result = *(int**)&sut;

                // Assert
                Assert.That(sut.Capacity, Is.EqualTo(expectedCapacity));
                Assert.That(sut.Count, Is.EqualTo(0));
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

            NativeList<int> sut = default;
            try
            {
                // Act
                sut = new NativeList<int>(expectedCapacity);
                var result = *(int**)&sut;

                // Assert
                Assert.That(sut.Capacity, Is.EqualTo(expectedCapacity));
                Assert.That(sut.Count, Is.EqualTo(0));
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
            var sut = new NativeList<int>(2);

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
            NativeList<int> sut = default;
            try
            {
                sut = new NativeList<int>(5);
                sut.AddRange([1, 2, 3]);

                // Act
                sut[2] = 42;
                var result = sut[2];

                // Assert
                Assert.That(sut.Capacity, Is.EqualTo(5));
                Assert.That(sut.Count, Is.EqualTo(3));
                Assert.That(result, Is.EqualTo(42));
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
            NativeList<int> sut = default;
            try
            {
                sut = new NativeList<int>(5);
                sut.AddRange([1, 2, 3]);

                // Act
                sut.Clear();

                // Assert
                Assert.That(sut.Capacity, Is.EqualTo(5));
                Assert.That(sut.Count, Is.EqualTo(0)); // 3->0
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
            NativeList<int> sut = default;
            try
            {
                sut = new NativeList<int>(5);
                sut.AddRange([1, 2, 3]);
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
            NativeList<int> sut = default;
            try
            {
                sut = new NativeList<int>(5);

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
            NativeList<int> sut = default;
            try
            {
                sut = new NativeList<int>(5);
                sut.AddRange([1, 2, 3]);

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
        public void Add_Value()
        {
            // Arrange
            NativeList<int> sut = default;
            try
            {
                sut = new NativeList<int>(0);

                // Act
                sut.Add(0);
                sut.Add(6);
                sut.Add(2);

                // Assert
                Assert.That(sut.Count, Is.EqualTo(3));
                Assert.That(sut[0], Is.EqualTo(0));
                Assert.That(sut[1], Is.EqualTo(6));
                Assert.That(sut[2], Is.EqualTo(2));
            }
            finally
            {
                sut.Dispose();
            }
        }

        [Test]
        public void AddRange_Pointer()
        {
            // Arrange
            NativeList<int> sut = default;
            try
            {
                sut = new NativeList<int>(0);
                var array = new int[] { 0, 6, 2 };

                // Act
                fixed (int* arrayPtr = array)
                {
                    sut.AddRange(arrayPtr, array.Length);
                }

                // Assert
                Assert.That(sut.Count, Is.EqualTo(3));
                Assert.That(sut[0], Is.EqualTo(array[0]));
                Assert.That(sut[1], Is.EqualTo(array[1]));
                Assert.That(sut[2], Is.EqualTo(array[2]));
            }
            finally
            {
                sut.Dispose();
            }
        }

        [Test]
        public void AddRange_Pointer_Null_Throws()
        {
            // Arrange
            NativeList<int> sut = default;
            try
            {
                sut = new NativeList<int>(0);

                // Act & assert
                Assert.Throws<ArgumentOutOfRangeException>(() => sut.AddRange(null, 1));
            }
            finally
            {
                sut.Dispose();
            }
        }

        [Test]
        public void AddRange_Pointer_Negative_Throws()
        {
            // Arrange
            NativeList<int> sut = default;
            try
            {
                sut = new NativeList<int>(0);

                // Act & assert
                Assert.Throws<ArgumentOutOfRangeException>(() => sut.AddRange((int*)1212, -1));
            }
            finally
            {
                sut.Dispose();
            }
        }

        [Test]
        public void AddRange_Collection()
        {
            // Arrange
            NativeList<int> sut = default;
            try
            {
                sut = new NativeList<int>(0);
                var array = new int[] { 4, 7, 3 };

                // Act
                sut.AddRange((IReadOnlyCollection<int>)array);

                // Assert
                Assert.That(sut.Count, Is.EqualTo(3));
                Assert.That(sut[0], Is.EqualTo(array[0]));
                Assert.That(sut[1], Is.EqualTo(array[1]));
                Assert.That(sut[2], Is.EqualTo(array[2]));
            }
            finally
            {
                sut.Dispose();
            }
        }

        [Test]
        public void AddRange_Collection_Null_Throws()
        {
            // Arrange
            NativeList<int> sut = default;
            try
            {
                sut = new NativeList<int>(0);

                // Act & assert
                Assert.Throws<ArgumentNullException>(() => sut.AddRange((IReadOnlyCollection<int>)null));
            }
            finally
            {
                sut.Dispose();
            }
        }

        [Test]
        public void AddRange_Array()
        {
            // Arrange
            NativeList<int> sut = default;
            try
            {
                sut = new NativeList<int>(0);
                var array = new int[] { 4, 7, 3 };

                // Act
                sut.AddRange(array);

                // Assert
                Assert.That(sut.Count, Is.EqualTo(3));
                Assert.That(sut[0], Is.EqualTo(array[0]));
                Assert.That(sut[1], Is.EqualTo(array[1]));
                Assert.That(sut[2], Is.EqualTo(array[2]));
            }
            finally
            {
                sut.Dispose();
            }
        }

        [Test]
        public void AddRange_Array_Null_Throws()
        {
            // Arrange
            NativeList<int> sut = default;
            try
            {
                sut = new NativeList<int>(0);

                // Act & assert
                Assert.Throws<ArgumentNullException>(() => sut.AddRange((int[])null));
            }
            finally
            {
                sut.Dispose();
            }
        }
    }
}