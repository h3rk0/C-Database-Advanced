using System.Linq;
using PhotoShare.Data;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class UploadPictureCommand
    {
        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public string Execute(string[] data)
        {
            var albumName = data[1];

            var pictureTitle = data[2];

            //var pictureFilePath = data[3]; ??

            using (var context=new PhotoShareContext())
            {
                var album = context.Albums.SingleOrDefault(a => a.Name == albumName);

                if (album == null)
                {
                    throw new ArgumentException($"Album {albumName} not found!");
                }


            }

            return $"Picture {pictureTitle} added to {albumName}!";
        }
    }
}
