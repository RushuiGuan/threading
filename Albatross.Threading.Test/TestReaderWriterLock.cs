using System.Threading;
using Xunit;

namespace Albatross.Threading.Test {
	public class TestReaderWriterLock {
		[Fact]
		public void TestNotUpgradableReaderLock() {
			using var slimLock = new ReaderWriterLockSlim();
			using (var readerLock = new ReaderLock(slimLock, false)) {
				Assert.True(slimLock.IsReadLockHeld);
				Assert.False(slimLock.IsUpgradeableReadLockHeld);
				Assert.False(slimLock.IsWriteLockHeld);
			}
			Assert.False(slimLock.IsReadLockHeld);
		}

		[Fact]
		public void TestUpgradableReaderLock() {
			using var slimLock = new ReaderWriterLockSlim();
			using (var readerLock = new ReaderLock(slimLock, true)) {
				Assert.False(slimLock.IsReadLockHeld);
				Assert.True(slimLock.IsUpgradeableReadLockHeld);
				Assert.False(slimLock.IsWriteLockHeld);
			}
			Assert.False(slimLock.IsUpgradeableReadLockHeld);
		}

		[Fact]
		public void TestWriterLock() {
			using var slimLock = new ReaderWriterLockSlim();
			using (var readerLock = new WriterLock(slimLock)) {
				Assert.False(slimLock.IsReadLockHeld);
				Assert.False(slimLock.IsUpgradeableReadLockHeld);
				Assert.True(slimLock.IsWriteLockHeld);
			}
			Assert.False(slimLock.IsWriteLockHeld);
		}

		[Fact]
		public void TestAsyncLock() {
			using SemaphoreSlim semaphore = new SemaphoreSlim(3);
			using (new AsyncLock(semaphore)) {
				Assert.Equal(2, semaphore.CurrentCount);
				using (new AsyncLock(semaphore)) {
					Assert.Equal(1, semaphore.CurrentCount);
					using (new AsyncLock(semaphore)) {
						Assert.Equal(0, semaphore.CurrentCount);
					}
				}
			}

			Assert.Equal(3, semaphore.CurrentCount);
		}
	}
}
