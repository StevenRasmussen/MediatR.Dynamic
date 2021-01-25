namespace MediatR.Dynamic
{
    public interface IDynamicNotificationRegistrar<TNotification> : INotificationHandler<TNotification>
        where TNotification : INotification
    {
        void RegisterHandler(IDynamicNotificationHandler<TNotification> handler);

        void UnRegisterHandler(IDynamicNotificationHandler<TNotification> handler);
    }
}
