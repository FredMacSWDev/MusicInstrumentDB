using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstrumentDB.Models.InstrumentFamilyModels
{
    public class InstrumentFamilyListItem
    {
        public int FamilyId { get; set; }
        public string FamilyName { get; set; }
        public string Description { get; set; }
        public string Classification { get; set; }
        public string Tuning { get; set; }
    }
}
