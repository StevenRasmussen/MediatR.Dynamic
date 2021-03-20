using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Dynamic
{
    public interface IDynamicFilteredNotificationHandler<TFilteredNotification> : INotificationHandler<TFilteredNotification>
        where TFilteredNotification : IDynamicFilteredNotification  
    {
        Dictionary<string, string> Params { get; set; }
        Task Handle(TFilteredNotification notification, CancellationToken cancellationToken);
    }
}
