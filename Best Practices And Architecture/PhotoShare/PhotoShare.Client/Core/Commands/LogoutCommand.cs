using System;
using System.Collections.Generic;
using System.Text;
using PhotoShare.Models;

namespace PhotoShare.Client.Core.Commands
{
    public static class LogoutCommand
    {
        public static string Execute(string[] data)
        {
            if (Session.User == null)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var logoutUser = Session.User.Username;

            Session.User = null;

            return $"User {logoutUser} successfully logged out!";
        }

    }
}
