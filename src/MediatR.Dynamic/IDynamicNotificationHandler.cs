using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Dynamic
{
    public interface IDynamicNotificationHandler<TNotification>
        where TNotification : INotification
    {
        Task Handle(TNotification notification, CancellationToken cancellationToken);
    }
}
