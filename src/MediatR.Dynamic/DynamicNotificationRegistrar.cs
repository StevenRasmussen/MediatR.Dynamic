using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;
#if DEBUG
using System.Diagnostics;
#endif
namespace MediatR.Dynamic
{
#if DEBUG
    public
#else
    internal
#endif
    sealed class DynamicNotificationRegistrar<TNotification> : IDynamicNotificationManager<TNotification>
        where TNotification : INotification
    {
        // I only want to lock when RegisterHandler and UnRegisterHandler
        private SemaphoreSlim _regLock = new SemaphoreSlim(1,1);
        // removed the lock that was in place.
        // it had 2 issues. 1, is not ascy friendly, 2. was static on the class.. so it was trying to lock on every notification type.
        // private static readonly object _collectionLock = new object();
        private List<IDynamicNotificationHandler<TNotification>> _handlers = new List<IDynamicNotificationHandler<TNotification>>();

        public async Task Handle(TNotification notification, CancellationToken cancellationToken)
        {
            // check and wait if it's in a registration process 
            while(this._regLock.CurrentCount == 0)
            {
#if DEBUG
                // for unit testing
                Debug.WriteLine($"lock waiting");
#endif
                await Task.Delay(1, cancellationToken);
            }
            // Copy the collection so that the collection is static (not modified)
            List<IDynamicNotificationHandler<TNotification>> handlersToExecute = new List<IDynamicNotificationHandler<TNotification>>(this._handlers);
            foreach (var handler in handlersToExecute)
            {
                if (!cancellationToken.IsCancellationRequested)
                { 
                    try
                    {
                        await handler.Handle(notification, cancellationToken).ConfigureAwait(false);
                    }
                    catch { }
                } 
            }
        }

        public void RegisterHandler(IDynamicNotificationHandler<TNotification> handler)
        {
            _regLock.Wait();
            try
            {
                if (!this._handlers.Contains(handler))
                {
                    this._handlers.Add(handler);
                }
            }
            catch { }
            finally
            {
                _regLock.Release();
            } 
        }

        public void UnRegisterHandler(IDynamicNotificationHandler<TNotification> handler)
        {
            _regLock.Wait();
            try
            {
                if (this._handlers.Contains(handler))
                {
                    this._handlers.Remove(handler);
                }
            }
            catch { }
            finally
            {
                _regLock.Release();
            }
        }
    }
}
