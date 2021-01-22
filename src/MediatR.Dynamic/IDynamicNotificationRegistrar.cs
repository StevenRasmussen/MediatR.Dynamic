namespace MediatR.Dynamic
{
    interface IDynamicNotificationRegistrar<TNotification> : INotificationHandler<TNotification>
        where TNotification : INotification
    {
        void RegisterHandler(IDynamicNotificationHandler<TNotification> handler);

        void UnregisterHandler(IDynamicNotificationHandler<TNotification> handler);
    }
}
