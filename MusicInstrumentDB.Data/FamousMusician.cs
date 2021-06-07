using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstrumentDB.Data
{
    public class FamousMusician
    {
        [Key]
        public int FamousMusicianId { get; set; }

        [Required]
        public int InstrumentId { get; set; }
        [ForeignKey(nameof(InstrumentId))]
        public virtual List<Instrument> Instruments { get; set; } = new List<Instrument>();

        [Required]
        public int MusicGenre { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Guid OwnderId { get; set; }
    }
}
