using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusTicketsSystem.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BusTicketsSystem.App
{
    public class Engine
    {
        private IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public void Run()
        {
            
            var databaseInitializerService = serviceProvider.GetService<IDatabaseInitializerService>();
            databaseInitializerService.InitializeDatabase();

            while (true)
            {
                Console.Write("Enter command: ");
                var input = Console.ReadLine();

                var commandTokens = input.Split(' ').ToArray();

                var commandName = commandTokens[0];

                var commandArgs = commandTokens.Skip(1).ToArray();

                var command = CommandParser.ParseCommand(serviceProvider,commandName);

                var result = command.Execute(commandArgs);

                Console.WriteLine(result);
            }
        }
    }
}
