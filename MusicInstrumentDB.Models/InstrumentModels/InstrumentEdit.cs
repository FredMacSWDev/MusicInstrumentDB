using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstrumentDB.Models.InstrumentModels
{
    public class InstrumentEdit
    {
        [Required]
        public int InstrumentId { get; set; }
        [Required]
        public string InstrumentName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Transposition { get; set; }

        public int? FamilyId { get; set; }
    }
}
