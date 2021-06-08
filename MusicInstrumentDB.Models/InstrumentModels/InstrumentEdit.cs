using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstrumentDB.Models.InstrumentModels
{
    public class InstrumentEdit
    {
        public int InstrumentId { get; set; }
        public string InstrumentName { get; set; }
        public string Description { get; set; }
        public string Transposition { get; set; }

        public int? FamilyId { get; set; }
    }
}
