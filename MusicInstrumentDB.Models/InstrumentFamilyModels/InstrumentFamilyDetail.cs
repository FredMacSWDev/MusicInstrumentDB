using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstrumentDB.Models.InstrumentFamilyModels
{
    public class InstrumentFamilyDetail
    {
        public int FamilyId { get; set; }
        public string FamilyName { get; set; }
        public virtual List<InstrumentListItem> Instruments { get; set; }
    }
}
