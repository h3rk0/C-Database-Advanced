using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Data;
using PhotoShare.Models;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class AddTagToCommand 
    {
        // AddTagTo <albumName> <tag>
        public static string Execute(string[] data)
        {
            var albumName = data[1];

            var tagName = data[2];

            using (var context= new PhotoShareContext())
            {
                var album = context.Albums
                    .Include(a => a.AlbumTags)
                    .ThenInclude(at => at.Tag)
                    .FirstOrDefault(a => a.Name == albumName);

                var tag = context.Tags.FirstOrDefault(t => t.Name == "#" + tagName);

                if (album == null|| tag == null)
                {
                    throw new ArgumentException("Either tag or album do not exist!");
                }

                AlbumTag albumTag =new AlbumTag()
                {
                    TagId = tag.Id,
                    Tag = context.Tags.FirstOrDefault(t => t.Name==tagName)
                };

              album.AlbumTags.Add(albumTag);

                context.SaveChanges();
            }

            return $"Tag #{tagName} added to {albumName}!";
        }
    }
}
