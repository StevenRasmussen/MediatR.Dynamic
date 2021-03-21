using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MediatR.Dynamic
{
    public interface IDynamicFilteredNotification : INotification
    {
        Dictionary<string, string> Params { get; set; }
    }
}
