using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.App.Command
{
    class ExitCommand : ICommand
    {
        public string Execute(params string[] args)
        {
            Console.WriteLine("Bye-bye!");

            Environment.Exit(0);

            return String.Empty;
        }
    }
}
