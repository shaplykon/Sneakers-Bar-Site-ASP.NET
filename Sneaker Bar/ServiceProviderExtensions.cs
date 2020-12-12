using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sneaker_Bar.Model;
using Sneaker_Bar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sneaker_Bar
{
    public static class ServiceProviderExtensions
    {
        public static void AddDateService(this IServiceCollection services)
        {
            services.AddTransient<DateService>();
        }


        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<SneakersRepository>();
            services.AddScoped<ArticleRepository>();
            services.AddScoped<CommentRepository>();
            services.AddScoped<PurchaseRepository>();
        }
    }
}
