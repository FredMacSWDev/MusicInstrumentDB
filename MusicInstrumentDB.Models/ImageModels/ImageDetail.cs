using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace MusicInstrumentDB.Models.ImageModels
{
    public class ImageDetail
    {
        public int ImageId { get; set; }
        public Image ImageConverted { get; set; }

        public Image ConvertFromByteArray(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            return Image.FromStream(ms);
        }
    }
}
