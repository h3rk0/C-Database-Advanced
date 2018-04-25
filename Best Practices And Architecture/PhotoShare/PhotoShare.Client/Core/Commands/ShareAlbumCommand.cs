using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Data;
using PhotoShare.Models;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class ShareAlbumCommand
    {
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public static string Execute(string[] data)
        {
            var albumId = int.Parse(data[1]);

            var username = data[2];

            var role = data[3];

            if (Session.User == null )
            {
                throw new InvalidOperationException($"Invalid Credentials!");
            }

            using (var context=new PhotoShareContext())
            {
                var album = context.Albums
                    .Include(a => a.AlbumRoles)
                    .ThenInclude(ar => ar.User)
                    .FirstOrDefault(a => a.Id == albumId);

                if(album.AlbumRoles.Select(ar => ar.User.Username==username) &&album.AlbumRoles.

                if (album == null)
                {
                    throw new ArgumentException($"Album {albumId} not found!");
                }

                var user = context.Users.FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                Role roleValue;

                if (!Enum.TryParse(role, out roleValue))
                {
                    throw new ArgumentException("Permission must be either “Owner” or “Viewer”!");
                }

                return $"User {username} added to album {album.Name} ({roleValue})";
            }
        }
    }
}
