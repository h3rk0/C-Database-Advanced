using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Employees.App.Command;

namespace Employees.App
{
    internal class CommandParser
    {

        public static ICommand Parse(IServiceProvider serviceProvider, string commandName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var commandTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(ICommand)));

            var commandType = commandTypes.SingleOrDefault(ct => ct.Name == $"{commandName}Command");

            var constructor = commandType.GetConstructors().First();

            var constructorParams = constructor
                .GetParameters()
                .Select(pi => pi.ParameterType);

            var constructorArgs = constructorParams
                .Select(serviceProvider.GetService)
                .ToArray();

            var command =(ICommand) constructor.Invoke(constructorArgs);

            return command;
        }
    }

}
