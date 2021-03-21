using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Dynamic
{
    /// <summary>
    /// interface for dynamic handlers that have the ability to filter the payload before sending to all the listeners
    /// </summary>
    /// <typeparam name="TFilteredNotification"></typeparam>
    public interface IDynamicFilteredNotificationHandler<TFilteredNotification> 
        where TFilteredNotification : IDynamicFilteredNotification  
    {
        /// <summary>
        /// set the paramaters that this handler will listen for.
        /// use the Keywork ALL for a handler within the filter that listens to all the notifications of this type
        /// </summary>
        Dictionary<string, string> Params { get; set; }
        Task Handle(TFilteredNotification notification, CancellationToken cancellationToken);
    }
}