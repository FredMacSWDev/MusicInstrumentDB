using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstrumentDB.Models.InstrumentModels
{
    public class InstrumentListItem
    {
        public int InstrumentId { get; set; }
        public string InstrumentName { get; set; }

        public int? FamilyId { get; set; }
        public string InstrumentFamilyName { get; set; }
    }
}
