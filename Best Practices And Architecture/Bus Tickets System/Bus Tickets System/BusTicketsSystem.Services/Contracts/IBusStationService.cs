using System;
using System.Collections.Generic;
using System.Text;
using BusTicketsSystem.Models;

namespace BusTicketsSystem.Services.Contracts
{
    public interface IBusStationService
    {
        BusStation ById(int id);


    }
}
