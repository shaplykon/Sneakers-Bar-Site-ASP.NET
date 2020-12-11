using System;
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
        public string GetDate() =>  DateTime.Now.ToLocalTime().ToString();
    }

}