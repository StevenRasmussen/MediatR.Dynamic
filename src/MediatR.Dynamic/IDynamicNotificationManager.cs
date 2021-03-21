namespace MediatR.Dynamic
{
    /// <summary>
    /// used to manage Dynamic notifications
    /// </summary>
    /// <typeparam name="TNotification"></typeparam>
    public interface IDynamicNotificationManager<TNotification> : INotificationHandler<TNotification>
        where TNotification : INotification
    {
        void RegisterHandler(IDynamicNotificationHandler<TNotification> handler);

        void UnRegisterHandler(IDynamicNotificationHandler<TNotification> handler);
    } 
}