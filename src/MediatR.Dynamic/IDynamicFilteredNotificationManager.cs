using System;
using System.Collections.Generic;
using System.Text;

namespace MediatR.Dynamic
{
    /// <summary>
    /// use to Manage Dynamic notifications with filters
    /// </summary>
    /// <typeparam name="TNotification"></typeparam>
    public interface IDynamicFilteredNotificationManager<TNotification>
            : IDynamicFilteredNotificationHandler<TNotification>
        where TNotification : IDynamicFilteredNotification
    {
        void RegisterHandler(IDynamicFilteredNotificationHandler<TNotification> handler);

        void UnRegisterHandler(IDynamicFilteredNotificationHandler<TNotification> handler);
    }
}
