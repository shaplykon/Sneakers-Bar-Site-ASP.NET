using System;
using Microsoft.Extensions.DependencyInjection;

namespace Sneaker_Bar
{
    public class DateService
    {        
        public string GetDate() =>  DateTime.Now.ToLocalTime().ToString();
    }
}