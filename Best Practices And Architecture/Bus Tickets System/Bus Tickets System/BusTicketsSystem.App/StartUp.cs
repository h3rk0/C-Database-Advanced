using System;
using BusTicketsSystem.Data;
using Microsoft.Extensions.DependencyInjection;
using BusTicketsSystem.Services;
using BusTicketsSystem.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsSystem.App
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();

            var engine =new Engine(serviceProvider);

            engine.Run();

        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<BusTicketsSystemContext>();

            serviceCollection.AddTransient<IBusStationService, BusStationService>();

            serviceCollection.AddTransient<IDatabaseInitializerService, DatabaseInitializerService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
        
    }
}
