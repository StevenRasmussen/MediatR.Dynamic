using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Dynamic
{
    public abstract class BaseDynamicFilteredNotificationHandler<TFilteredNotification> 
        : IDynamicFilteredNotificationHandler<TFilteredNotification>, IDisposable
                where TFilteredNotification : IDynamicFilteredNotification
    {
        public abstract Dictionary<string, string> Params { get; set; }
        private readonly IDynamicFilteredNotificationManager<TFilteredNotification> _manager;
        public BaseDynamicFilteredNotificationHandler(IDynamicFilteredNotificationManager<TFilteredNotification> manager)
        {
            _manager = manager; 
            _manager.RegisterHandler(this);
        }
        ~BaseDynamicFilteredNotificationHandler()
        {
            _manager.UnRegisterHandler(this);
        }
        public void Dispose()
        {
            _manager.UnRegisterHandler(this);
        }

        public abstract Task Handle(TFilteredNotification notification, CancellationToken cancellationToken);
    }
}
