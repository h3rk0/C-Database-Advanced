using System;
using System.Collections.Generic;
using System.Text;
using BusTicketsSystem.App.Commands.Contracts;

namespace BusTicketsSystem.App.Commands
{
    public class Exit : ICommand
    {
        public string Execute(params string[] arguments)
        {
            Environment.Exit(0);
            
            return string.Empty;
        }
    }
}
