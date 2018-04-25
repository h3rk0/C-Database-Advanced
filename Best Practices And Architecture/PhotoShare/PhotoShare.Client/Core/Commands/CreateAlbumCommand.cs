using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Client.Utilities;
using PhotoShare.Data;
using PhotoShare.Models;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class CreateAlbumCommand
    {
        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public static string Execute(string[] data)
        {


            var username = data[1];

            var albumTitle = data[2];

            var bgColor = data[3]; //??

            var tags = data.Skip(4).Select(t => t.ValidateOrTransform()).ToList();

            if (Session.User == null || Session.User.Username!=username)
            {
                throw new InvalidOperationException($"Invalid Credentials!");
            }

            using (var context=new PhotoShareContext())
            {

                var user = context.Users
                    .FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                var album = context.Albums.Include(a => a.AlbumTags)
                    .ThenInclude(at => at.Tag)
                    .FirstOrDefault(a => a.Name == albumTitle);

                if (album != null)
                {
                    throw new ArgumentException($"Album {albumTitle} exists!");
                }

                Color enumValue;

                if (!Color.TryParse(bgColor, out enumValue))
                {
                    throw new ArgumentException($"Color {bgColor} not found!");
                }

                // var tags=new List<string>();



                //if (!tags.All(t => context.Tags.Any(ct => ct.Name==t)))
                //{
                //    throw new ArgumentException("Invalid tags!");
                //}

                foreach (var tag in tags)
                {
                    if (!context.Tags.Any(x => x.Name == tag))
                    {
                        throw new ArgumentException("Invalid tags!");
                    }
                }

                var tagsCollection =new List<Tag>();

                foreach (var tag in tags)
                {
                    var newTag = new Tag()
                    {
                         Name = tag
                    };

                tagsCollection.Add(newTag);

                }

                var newAlbum = new Album()
                {
                    Name = albumTitle,
                    BackgroundColor = enumValue,

                    AlbumRoles = new List<AlbumRole>()
                    {
                        new AlbumRole()
                        {
                            User=user,
                            Role = Role.Owner
                        }
                    },

                    AlbumTags = tags.Select(t => new AlbumTag()
                    {
                        Tag=context.Tags.FirstOrDefault(ct => ct.Name==t)
                    }).ToArray()
                };

                context.Albums.Add(newAlbum);

                context.SaveChanges();

                return $"Album {albumTitle} successfully created!";
            }
        }
    }
}
