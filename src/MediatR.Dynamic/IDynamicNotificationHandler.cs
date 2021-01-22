using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Dynamic
{
    interface IDynamicNotificationHandler<in TNotification> 
        where TNotification : INotification
    {
        Task Handle(TNotification notification, CancellationToken cancellationToken);
    }
}
