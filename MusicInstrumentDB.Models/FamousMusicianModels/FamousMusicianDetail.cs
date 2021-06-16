using MusicInstrumentDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstrumentDB.Models.FamousMusicianModels
{
    public class FamousMusicianDetail
    {
        public int FamousMusicianId { get; set; }
        public string FullName { get; set; }
        public int? InstrumentId { get; set; }
        public string MusicGenre { get; set; }
        public string Description { get; set; }
        public string InstrumentName { get; set; }
    }
}
