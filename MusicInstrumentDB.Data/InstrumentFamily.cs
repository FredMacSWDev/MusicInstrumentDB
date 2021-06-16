using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstrumentDB.Data
{
    public class InstrumentFamily
    {
        [Key]
        public int FamilyId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string FamilyName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Classification { get; set; }

        [Required]
        public string Tuning { get; set; }

        public virtual List<Instrument> Instruments { get; set; } = new List<Instrument>();

    }
}
