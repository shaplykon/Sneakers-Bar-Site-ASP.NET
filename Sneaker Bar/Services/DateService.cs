using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Sneaker_Bar
{
    public static class ServiceProviderExtensions
    {
        public static void AddDateService(this IServiceCollection services)
        {
            services.AddTransient<DateService>();
        }
    }

    public class DateService
    {
        public string date { get; set; }
        public DateService() => date = DateTime.Now.ToString();
    }

}