using System;
using System.Linq;

namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Data;

    public class AddTownCommand
    {
        // AddTown <townName> <countryName>
        public static string Execute(string[] data)
        {
            if (Session.User == null)
            {
                throw new InvalidOperationException($"Invalid Credentials!");
            }

            string townName = data[1];

            string country = data[0];

            if (townName == country)
            {
                throw new InvalidOperationException("Invalid Credentials!");
            }

            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (context.Towns.Any(t => t.Name == townName))
                {
                    throw new ArgumentException("Town already exists");
                }

                Town town = new Town
                {
                    Name = townName,
                    Country = country
                };

                context.Towns.Add(town);
                context.SaveChanges();

                return townName + " was added to database!";
            }
        }
    }
}
