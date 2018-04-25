namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using Data;

    public class DeleteUser
    {
        // DeleteUser <username>
        public static string Execute(string[] data)
        {
            string username = data[1];

            if (Session.User == null || Session.User.Username!=username)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }
           
            using (PhotoShareContext context = new PhotoShareContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    throw new InvalidOperationException($"User with {username} was not found!");
                }

                if (user.IsDeleted.Value)
                {
                    throw new InvalidOperationException($"User {username} is already deleted!");
                }

                user.IsDeleted = true;

                // TODO: Delete User by username (only mark him as inactive)
                context.SaveChanges();

                return $"User {username} was deleted from the database!";
            }
        }
    }
}
