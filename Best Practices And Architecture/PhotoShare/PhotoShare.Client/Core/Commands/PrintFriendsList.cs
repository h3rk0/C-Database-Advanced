using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Data;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class PrintFriendsListCommand 
    {
        // PrintFriendsList <username>
        public static string Execute(string[] data)
        {
            var username = data[1];

            using (var context= new PhotoShareContext())
            {
                var user = context.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(fa => fa.Friend)
                    .FirstOrDefault(u => u.Username==username);
                    

                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }
                
                if (user.FriendsAdded.Count == 0)
                {
                    return $"No friends for this user.:(";
                }
                else
                {
                    StringBuilder builder=new StringBuilder();

                    builder.Append($"Friends:"+Environment.NewLine);

                    foreach (var friendship in user.FriendsAdded.OrderBy(fa => fa.Friend))
                    {
                        builder.Append($"-[{friendship.Friend.Username}]"+Environment.NewLine);
                    }

                    return builder.ToString();
                }
            }
        }
    }
}
