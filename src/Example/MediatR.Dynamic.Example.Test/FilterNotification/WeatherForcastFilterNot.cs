using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatR.Dynamic.Example.Test.FilterNotification
{
    public class WeatherForcastFilterNot : IDynamicFilteredNotification
    {
        public Dictionary<string, string> Params { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public string Summary { get; set; }
    }
}
