using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstrumentDB.Models.FamousMusicianModels
{
    public class FamousMusicianCreate
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public int InstrumentId { get; set; }
        [Required]
        public string MusicGenre { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
