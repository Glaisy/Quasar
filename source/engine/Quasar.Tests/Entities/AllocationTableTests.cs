using System;

using NUnit.Framework;

using Quasar.Entities.Internals;

namespace Quasar.Entities.Tests.Internals
{
    [TestFixture]
    public class AllocationTableTests
    {
        [Test]
        public void Initialize_AllocateAll()
        {
            // Arrange
            var sut = new AllocationTable();
            sut.Initialize();

            // Act & Assert
            bool success;
            int index;
            for (var i = 0; i < AllocationTable.EntryCount; i++)
            {
                success = sut.TryAllocate(out index);
                Assert.That(success, Is.True);
                Assert.That(index, Is.EqualTo(i));
            }

            success = sut.TryAllocate(out index);
            Assert.That(success, Is.False);
            Assert.That(index, Is.EqualTo(0));
        }

        [Test]
        public void Initialize_Allocate_Free_ReAllocate()
        {
            // Arrange
            var expectedResult1 = 23;
            var expectedResult2 = 77;
            var numberOfAllocations = 96;
            var expectedResult3 = numberOfAllocations;
            var sut = new AllocationTable();
            sut.Initialize();
            for (var i = 0; i < numberOfAllocations; i++)
            {
                sut.TryAllocate(out _);
            }

            sut.Free(expectedResult2);
            sut.Free(expectedResult1);

            // Act
            var success1 = sut.TryAllocate(out var result1);
            var success2 = sut.TryAllocate(out var result2);
            var success3 = sut.TryAllocate(out var result3);

            // Assert
            Assert.That(success1, Is.True);
            Assert.That(result1, Is.EqualTo(expectedResult1));
            Assert.That(success2, Is.True);
            Assert.That(result2, Is.EqualTo(expectedResult2));
            Assert.That(success3, Is.True);
            Assert.That(result3, Is.EqualTo(expectedResult3));
        }

        [Test]
        public void DebugOnly_Free_InvalidIndex_ThrowsException()
        {
#if DEBUG
            // Arrange
            var sut = new AllocationTable();
            sut.Initialize();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.Free(AllocationTable.EntryCount));
#endif
        }

        [Test]
        public void DebugOnly_Free_UnallocatedSlot_ThrowsException()
        {
#if DEBUG
            // Arrange
            var sut = new AllocationTable();
            sut.Initialize();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => sut.Free(23));
#endif
        }
    }
}