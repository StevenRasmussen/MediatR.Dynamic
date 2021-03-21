using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MediatR.Dynamic.Example.Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastFilterTestController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static readonly Tuple<string, string>[] Location = new[]
        {
            new Tuple<string,string>("US","PA"),
            new Tuple<string,string>("US","NY"),
            new Tuple<string,string>("UK","York"),
            new Tuple<string,string>("UK","London"),
        };

        private IMediator _Mediator { get; set; }
        private IServiceProvider _Provider { get; set; }
        public WeatherForecastFilterTestController(IMediator mediator, IServiceProvider provider)
        {
            this._Provider = provider;
            this._Mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var rng = new Random();
            List<WeatherForecast> _weatherForcast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToList();
            Stopwatch _sp = new Stopwatch();
            _sp.Start();
            _weatherForcast.ForEach( async (w) => {
                await this._Mediator.Publish(new WeatherForecastNotification { Date = w.Date });
                await this._Mediator.Publish(new WeatherForecastNotification2 { Date = w.Date });
            });

            _sp.Stop();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        public static List<WeatherForcast2NotHandler> _handlerTest = new List<WeatherForcast2NotHandler>();
        public static List<WeatherForcast2NotHandler2> _handlerTest2 = new List<WeatherForcast2NotHandler2>();
        [HttpPost]
        public async Task AddMoreHandlers()
        {

            for (int i = 0; i < 10; i++)
            { 
                _handlerTest.Add(new WeatherForcast2NotHandler(
                    (IDynamicNotificationManager<WeatherForecastNotification2>)
                        this._Provider.GetService(typeof(IDynamicNotificationManager<WeatherForecastNotification2>))));

                _handlerTest2.Add(new WeatherForcast2NotHandler2(
                    (IDynamicNotificationManager<WeatherForecastNotification2>)
                        this._Provider.GetService(typeof(IDynamicNotificationManager<WeatherForecastNotification2>)))); 
            }
        }
    }
}
