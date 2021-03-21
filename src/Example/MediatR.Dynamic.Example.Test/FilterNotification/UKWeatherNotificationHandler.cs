using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Dynamic.Example.Test.FilterNotification
{
    public class UKWeatherNotificationHandler : IDynamicFilteredNotificationHandler<WeatherForcastFilterNot>
    {
        public Dictionary<string, string> Params { get; set; } = new Dictionary<string, string>(
            new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("CTRY","UK")
            });
 

        public async Task Handle(WeatherForcastFilterNot notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"{notification.Summary}");
        }
    }

    public class UKLDNWeatherNotificationHandler : IDynamicFilteredNotificationHandler<WeatherForcastFilterNot>
    {
        public Dictionary<string, string> Params { get; set; } = new Dictionary<string, string>(
            new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("CTRY","UK"),
                new KeyValuePair<string, string>("State","London")
            });


        public async Task Handle(WeatherForcastFilterNot notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"{notification.Summary}");
        }
    }

    public class UKYorkWeatherNotificationHandler : IDynamicFilteredNotificationHandler<WeatherForcastFilterNot>
    {
        public Dictionary<string, string> Params { get; set; } = new Dictionary<string, string>(
            new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("CTRY","UK"),
                new KeyValuePair<string, string>("State","York")
            });


        public async Task Handle(WeatherForcastFilterNot notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"{notification.Summary}");
        }
    }
}
