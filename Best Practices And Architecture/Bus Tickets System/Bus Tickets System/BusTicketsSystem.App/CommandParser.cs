using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using BusTicketsSystem.App.Commands.Contracts;

namespace BusTicketsSystem.App
{
    public class CommandParser
    {
        public static ICommand ParseCommand(IServiceProvider serviceProvider,string commandName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var commandTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(ICommand)))
                .ToArray();
            
            var commandType = commandTypes.SingleOrDefault(t => t.Name == $"{commandName}");
            
            if (commandType == null)
            {
                throw new InvalidOperationException("Invalid Command!");
            }

            var constructor = commandType.GetConstructors().First();

            var constructorParameters = constructor.GetParameters()
                .Select(pi => pi.ParameterType)
                .ToArray();

            var services = constructorParameters
                .Select(serviceProvider.GetService)
                .ToArray();

            var command =(ICommand) constructor.Invoke(services);

            return command;
        }
    }
}
