using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusTicketsSystem.Data;
using BusTicketsSystem.Models;
using BusTicketsSystem.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BusTicketsSystem.Services
{
    public class BusStationService : IBusStationService
    {
        private readonly BusTicketsSystemContext context;

        public BusStationService(BusTicketsSystemContext context)
        {
            this.context = context;
        }
        public BusStation ById(int id)
        {

            var busStation = context.BusStations.Find(id);

            return busStation;
        }
    }
}
