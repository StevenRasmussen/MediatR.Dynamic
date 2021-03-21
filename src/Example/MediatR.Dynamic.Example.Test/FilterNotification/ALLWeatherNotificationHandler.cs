using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Dynamic.Example.Test.FilterNotification
{
    public class ALLWeatherNotificationHandler : IDynamicFilteredNotificationHandler<WeatherForcastFilterNot>
    {
        public Dictionary<string, string> Params { get; set; } = new Dictionary<string, string>(
            new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("CTRY","UK")
            });
 
        public ALLWeatherNotificationHandler()
        {

        }
        public async Task Handle(WeatherForcastFilterNot notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"{notification.Summary}");
        }
    }
}
