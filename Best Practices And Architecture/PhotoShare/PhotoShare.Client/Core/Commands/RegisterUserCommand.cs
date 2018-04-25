using System.Linq;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Models;
    using Data;

    public class RegisterUserCommand
    {
        // RegisterUser <username> <password> <repeat-password> <email> <borntown> <currenttown>
        public static string Execute(string[] data)
        {
            if (Session.User != null)
            {
                throw new InvalidOperationException($"Invalid Credentials!");
            }

            string username = data[1];
            string password = data[2];
            string repeatPassword = data[3];
            string email = data[4];

            if (password != repeatPassword)
            {
                throw new ArgumentException("Passwords do not match!");
            }

            using (var db=new PhotoShareContext())
            {
                if (db.Users.Any(u => u.Username == username))
                {
                    throw new InvalidOperationException($"Username {username} is already taken!");
                }
            }



            User user = new User
            {
                Username = username,
                Password = password,
                Email = email,
                IsDeleted = false,
                RegisteredOn = DateTime.Now,
                LastTimeLoggedIn = DateTime.Now
            };

            using (PhotoShareContext context = new PhotoShareContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            return "User " + user.Username + " was registered successfully!";
        }
    }
}
