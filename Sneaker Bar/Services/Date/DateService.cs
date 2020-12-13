using System;
using Microsoft.Extensions.DependencyInjection;
using Sneaker_Bar.Services.Date;

namespace Sneaker_Bar
{
    public class DateService:IDateService
    {        
        public string GetDate() =>  DateTime.Now.ToLocalTime().ToString();
    }
}