using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Employees.App
{
    internal class Engine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public void Run()
        {

            while (true)
            {
                string input = Console.ReadLine();

                string[] commandTokens = input.Split(' ');

                string commandName = commandTokens.First();

                string[] commandArgs = commandTokens.Skip(1).ToArray();

                var command = CommandParser.Parse(serviceProvider, commandName);

                var result = command.Execute(commandArgs);

                Console.WriteLine(result);

            }
        }
    }
}
