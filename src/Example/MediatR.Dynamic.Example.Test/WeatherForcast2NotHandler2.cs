using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Dynamic.Example.Test
{
    public class WeatherForcast2NotHandler2 : IDynamicNotificationHandler<WeatherForecastNotification2>
    {

        private readonly IDynamicNotificationManager<WeatherForecastNotification2> _registrar;
        public WeatherForcast2NotHandler2(IDynamicNotificationManager<WeatherForecastNotification2> registrar)
        {

            _registrar = registrar;

            // Dynamically add this class as a handler for the notification type 'YourNotificationTypeHere'
            _registrar.RegisterHandler(this);
        }

        public async Task Handle(WeatherForecastNotification2 notification, CancellationToken cancellationToken)
        {
            await Task.Delay(10);
        }

        ~WeatherForcast2NotHandler2()
        {
            // Un-register this class as an event handler for the notification type
            _registrar.UnRegisterHandler(this);
        }
    }
}