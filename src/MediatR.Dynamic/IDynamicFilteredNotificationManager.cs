using System;
using System.Collections.Generic;
using System.Text;

namespace MediatR.Dynamic
{
    /// <summary>
    /// use to Manage Dynamic notifications with filters
    /// </summary>
    /// <typeparam name="TFilteredNotification"></typeparam>
    public interface IDynamicFilteredNotificationManager<TFilteredNotification>
            : INotificationHandler<TFilteredNotification>
        where TFilteredNotification : IDynamicFilteredNotification
    {
        void RegisterHandler(IDynamicFilteredNotificationHandler<TFilteredNotification> handler);

        void UnRegisterHandler(IDynamicFilteredNotificationHandler<TFilteredNotification> handler);
    }
}
