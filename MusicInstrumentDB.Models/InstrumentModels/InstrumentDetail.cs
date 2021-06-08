using MusicInstrumentDB.Data;
using MusicInstrumentDB.Models.FamousMusicianModels;
using MusicInstrumentDB.Models.InstrumentFamilyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstrumentDB.Models.InstrumentModels
{
    public class InstrumentDetail
    {
        public int InstrumentId { get; set; }
        public string InstrumentName { get; set; }
        public string Description { get; set; }
        public string Transposition { get; set; }

        public string InstrumentFamilyName {get; set;}

        public virtual List<FamousMusicianListItem> FamousMusicians { get; set; }
    }
}
