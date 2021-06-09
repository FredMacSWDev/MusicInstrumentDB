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

        //public byte[] Converter(Image imageIn)
        //{
        //    using (var ms = new MemoryStream())
        //    {
        //        imageIn.Save(ms, imageIn.RawFormat);
        //        return ms.ToArray();
        //    }
        //}


        //public Image byteArrayToImage(byte[] byteArrayIn)
        //{
        //    MemoryStream ms = new MemoryStream(byteArrayIn);
        //    Image returnImage = Image.FromStream(ms);
        //    return returnImage;
        //}

        public bool CreateImage(Image imageIn)//need to refactor to accept raw image
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

        public ImageDetail GetImageById(int id)//refactor to return raw image
        {
            //will call byteArrayToImage
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
