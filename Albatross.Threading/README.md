# About

Utility library for working with threads in C#

# Features

* [AsyncLock](./AsyncLock.cs) - Use the `IDisposable` pattern for the `SemaphoreSlim` class. When disposed, the
  underlying `SemaphoreSlim` will be released once. It doesn't dispose the `SemaphoreSlim` class itself.
* [ReaderLock](./ReaderLock.cs), [WriterLock](./WriterLock.cs) - Use the `IDisposable` pattern for the `ReaderLockSlim`
  and `WriterLockSlim`. When disposed, it would release the underlying locks.
* [TaskCallback](./TaskCallback.cs) - The TaskCallback class implements the async task callback pattern. 