using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Dynamic.Example.Test.FilterNotification
{
    public class USWeatherNotificationHandler : IDynamicFilteredNotificationHandler<WeatherForcastFilterNot>
    {
        public Dictionary<string, string> Params { get; set; } = new Dictionary<string, string>(
            new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("CTRY","US")
            });
 

        public async Task Handle(WeatherForcastFilterNot notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"{notification.Summary}");
        }
    }

    public class USPAWeatherNotificationHandler : IDynamicFilteredNotificationHandler<WeatherForcastFilterNot>
    {
        public Dictionary<string, string> Params { get; set; } = new Dictionary<string, string>(
            new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("CTRY","US"),
                new KeyValuePair<string, string>("State","PA")
            });


        public async Task Handle(WeatherForcastFilterNot notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"{notification.Summary}");
        }
    }

    public class USNYWeatherNotificationHandler : IDynamicFilteredNotificationHandler<WeatherForcastFilterNot>
    {
        public Dictionary<string, string> Params { get; set; } = new Dictionary<string, string>(
            new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("CTRY","US"),
                new KeyValuePair<string, string>("State","NY")
            });


        public async Task Handle(WeatherForcastFilterNot notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"{notification.Summary}");
        }
    }
}
