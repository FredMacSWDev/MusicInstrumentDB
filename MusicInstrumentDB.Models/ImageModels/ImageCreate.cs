using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace MusicInstrumentDB.Models.ImageModels
{
    public class ImageCreate
    {
        public byte[] ImageByte { get; set; }

        public byte[] ConvertToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, imageIn.RawFormat);
            return ms.ToArray();
        }
    }
}
