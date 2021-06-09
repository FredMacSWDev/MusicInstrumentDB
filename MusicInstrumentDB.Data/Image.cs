using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstrumentDB.Data
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }

        [Required]
        public byte[] ImageByte { get; set; }
    }
}
