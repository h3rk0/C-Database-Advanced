using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhotoShare.Data;
using PhotoShare.Models;

namespace PhotoShare.Client.Core.Commands
{
    public class LoginCommand
    {
        public static string Execute(string[] data)
        {
            if (Session.User != null)
            {
                throw new InvalidOperationException("Invalid Credentials!");
            }

            var username = data[1];

            var password = data[2];

            using (var context =new PhotoShareContext())
            {
                var user = context.Users
                    .SingleOrDefault(u => u.Username == username && u.Password == password);

                if (user == null)
                {
                    throw new ArgumentException("Invalid username or password!");
                }

                Session.User = user;

            }

            return $"User {username} successfully logged in!";
        }
    }
}
