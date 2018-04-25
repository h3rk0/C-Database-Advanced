using System;
using System.Collections.Generic;
using System.Text;
using BusTicketsSystem.App.Commands.Contracts;
using BusTicketsSystem.Data;
using BusTicketsSystem.Services.Contracts;

namespace BusTicketsSystem.App.Commands
{
    public class Print_Info : ICommand
    {
        private readonly IBusStationService busStationService;

        public Print_Info(IBusStationService busStationService)
        {
            this.busStationService = busStationService;
        }

        public string Execute(params string[] arguments)
        {
            var id =int.Parse(arguments[0]);

            var busStation = busStationService.ById(id);

            if (busStation == null)
            {
                throw new ArgumentException("No such BusStation!");
            }

            var result = new StringBuilder();

            result.AppendLine($"{busStation.Name}, {busStation.Town}");
            result.AppendLine("Arrivals:");
            foreach (var arriving in busStation.ArriveTrips)
            {
                result.AppendLine($"From: {arriving.OriginBusStation.Name} | Arrive at: {arriving.ArrivalTime} | Status: {arriving.Status}");
            }

            result.AppendLine("Departures: ");
            foreach (var departing in busStation.departTrips)
            {
                result.AppendLine(
                    $"To: {departing.DestinationBusStation.Name} | Depart at: {departing.DepartureTime} | Status : {departing.Status}");
            }

            return result.ToString();
        }
    }
}
