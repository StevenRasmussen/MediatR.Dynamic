using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
#if DEBUG
using System.Diagnostics;
#endif
namespace MediatR.Dynamic
{
    /// <summary>
    /// Dynamic notification Manager
    /// </summary>
    /// <typeparam name="TNotification"></typeparam>
#if DEBUG
    public
#else
    internal
#endif
      sealed class DynamicFilteredNotificationManager<TNotification> : IDynamicFilteredNotificationManager<TNotification>
        where TNotification : IDynamicFilteredNotification
    {
        private SemaphoreSlim _regLock = new SemaphoreSlim(1, 1);
        private List<IDynamicFilteredNotificationHandler<TNotification>> _handlers = new List<IDynamicFilteredNotificationHandler<TNotification>>();

        public async Task Handle(TNotification notification, CancellationToken cancellationToken)
        {

            // check and wait if it's in a registration process 
            while (this._regLock.CurrentCount == 0)
            {
#if DEBUG
                // for unit testing
                Debug.WriteLine($"lock waiting");
#endif
                await Task.Delay(1, cancellationToken);
            }
            // Copy the collection so that the collection is static (not modified)
            List<IDynamicFilteredNotificationHandler<TNotification>> handlersToExecute; 
            if(notification.Params != null)
            {
                handlersToExecute = new List<IDynamicFilteredNotificationHandler<TNotification>>(
                    this._handlers.Where(
                        item => (item.Params != null &&
                             (notification.Params.All(f => item.Params.ContainsKey(f.Key) &&
                                        (item.Params[f.Key]) == f.Value)))
                                || (item.Params != null && item.Params.ContainsKey("ALL"))) 
                    );
            }
            else
            {
                handlersToExecute =
                    new List<IDynamicFilteredNotificationHandler<TNotification>>(this._handlers);
            }

            foreach (var handler in handlersToExecute)
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    // add try catch in case object was disposed before executed.
                    try
                    {
                        await handler.Handle(notification, cancellationToken).ConfigureAwait(false);
                    }
                    catch { }
                }
            }
        }

        public void RegisterHandler(IDynamicFilteredNotificationHandler<TNotification> handler)
        {
#if DEBUG
            if(handler == null)
            {
                return;
            }
#endif
            _regLock.Wait();
            try
            {
#if DEBUG
                if (handler == null)
                {
                    return;
                }
#endif
                if (!this._handlers.Contains(handler))
                {
                    this._handlers.Add(handler);
                }
            }
#if DEBUG
            catch (Exception ex)
            {
                Debug.WriteLine($"Error RegisterHandler: {ex.Message}");
            }
#else
            catch {}
#endif

            finally
            {
                _regLock.Release();
            }
        }

        public void UnRegisterHandler(IDynamicFilteredNotificationHandler<TNotification> handler)
        {
            _regLock.Wait();
            try
            {
                if (this._handlers.Contains(handler))
                {
                    this._handlers.Remove(handler);
                }
#if DEBUG
                else
                {

                }
#endif

            }
#if DEBUG
            catch (Exception ex)
            {
                Debug.WriteLine($"Error UnRegisterHandler: {ex.Message}");
            }
#else
            catch {}
#endif
            finally
            {
                _regLock.Release();
            }
        }
    }
}
