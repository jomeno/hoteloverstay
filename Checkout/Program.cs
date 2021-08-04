using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hotel.Core.Repos;
using Hotel.Infrastructure.Repos;
using Hotel.Core.Managers;

namespace Checkout
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // add repositaries
                    services.AddSingleton<IReservation, Reservation>();
                    services.AddSingleton<ICheckoutManager, CheckoutManager>();

                    // add hosted services
                    services.AddHostedService<Worker>();
                });
    }
}
