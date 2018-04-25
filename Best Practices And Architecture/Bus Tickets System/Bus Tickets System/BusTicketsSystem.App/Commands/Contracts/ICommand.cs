using System;
using System.Collections.Generic;
using System.Text;

namespace BusTicketsSystem.App.Commands.Contracts
{
    public interface ICommand
    {
        string Execute(params string[] arguments);
    }
}
