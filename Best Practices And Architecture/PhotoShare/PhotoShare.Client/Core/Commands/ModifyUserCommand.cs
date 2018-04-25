using System.Linq;
using PhotoShare.Data;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class ModifyUserCommand
    {
        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public static string Execute(string[] data)
        {
            string username = data[1];

            string property = data[2].ToLower();

            string newValue = data[3];

            if (Session.User == null || Session.User.Username!= username)
            {
                throw new InvalidOperationException($"Invalid Credentials!");
            }

            using (var context=new PhotoShareContext())
            {
                var user = context.Users
                    .FirstOrDefault(u => u.Username == username);

                if (user==null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                switch (property)
                {
                    case "password":
                        if (!newValue.Any(c => Char.IsLower(c))
                            || !newValue.Any(c => Char.IsDigit(c)))
                        {
                            throw new ArgumentException(
                                $"Value {newValue} not valid." + Environment.NewLine + 
                                "Invalid Password!");
                        }
                        user.Password = newValue;
                        break;
                    case "borntown":

                        var bornTown = context.Towns
                            .Where(t => t.Name == newValue)
                            .FirstOrDefault();

                        if (bornTown == null)
                        {
                            throw new ArgumentException($"Value {newValue} not valid." + Environment.NewLine +
                                                        $"Town {bornTown} not found!");
                        }

                        user.BornTown = bornTown;
                        break;
                    case "currenttown":

                        var currentTown = context.Towns
                            .Where(t => t.Name == newValue)
                            .FirstOrDefault();

                        if (currentTown == null)
                        {
                            throw new ArgumentException($"Value {newValue} not valid." + Environment.NewLine +
                                                        $"Town {newValue} not found!");
                        }

                        user.CurrentTown = currentTown;
                        break;
                    default:
                        throw new ArgumentException($"Property {property} not supported!");
                }
                context.SaveChanges();
                return $"User {username} {property} is {newValue}.";
            }
        }
    }
}
