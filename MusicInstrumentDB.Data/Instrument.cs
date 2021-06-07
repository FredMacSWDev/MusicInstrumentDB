using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstrumentDB.Data
{
    public class Instrument
    {
        [Key]
        public int InstrumentId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        public string InstrumentName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Transposition { get; set; }


        [ForeignKey(nameof(InstrumentFamily))]
        public int? FamilyId { get; set; }
        public virtual InstrumentFamily InstrumentFamily { get; set; }

        [Display(Name = "Played By")]
        public virtual List<FamousMusician> FamousMusicians { get; set; } = new List<FamousMusician>();
    }
}
