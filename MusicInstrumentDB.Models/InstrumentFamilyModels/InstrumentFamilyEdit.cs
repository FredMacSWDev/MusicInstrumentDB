using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstrumentDB.Models.InstrumentFamilyModels
{
    public class InstrumentFamilyEdit
    {
        public int FamilyId { get; set; }
        public string FamilyName { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public string Classification { get; set; }

        [Required]
        public string Tuning { get; set; }
    }
}
