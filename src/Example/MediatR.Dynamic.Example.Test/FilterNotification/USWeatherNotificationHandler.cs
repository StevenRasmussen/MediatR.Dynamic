using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Dynamic.Example.Test.FilterNotification
{
    public class USWeatherNotificationHandler : IDynamicFilteredNotificationHandler<WeatherForcastFilterNot>
    {
        public Dictionary<string, string> Params { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Task Handle(WeatherForcastFilterNot notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
