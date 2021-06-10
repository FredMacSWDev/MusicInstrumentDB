using Microsoft.AspNetCore.Http;
using MusicInstrumentDB.Data;
using MusicInstrumentDB.Models.ImageModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstrumentDB.Services
{
    public class ImageService
    {
        public readonly Guid _userId;

        public ImageService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateImage(IFormFile imageIn)
        {
            ImageCreate model = new ImageCreate();
            //will call converter
            var entity =
                new ImageAsByte()
                {
                    ImageByte = model.ConvertToByteArray(imageIn)
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.ByteArrayImages.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public ImageDetail GetImageById(int id)
        {
            //will call converter
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ByteArrayImages
                        .Single(e => e.ImageId == id && e.OwnerId == _userId);

                byte[] byteArray = entity.ImageByte;
                ImageDetail finalImage = new ImageDetail();
                finalImage.ImageConverted = finalImage.ConvertFromByteArray(byteArray);
                finalImage.ImageId = entity.ImageId;
                return finalImage;
            }
        }
    }
}
