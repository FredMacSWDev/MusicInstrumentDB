using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace MusicInstrumentDB.Models.ImageModels
{
    public class ImageCreate
    {
        public byte[] ImageByte { get; set; }

        public byte[] ConvertToByteArray(IFormFile imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.CopyTo(ms);
            return ms.ToArray();
        }
    }
}
