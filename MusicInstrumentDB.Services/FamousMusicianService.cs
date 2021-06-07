using MusicInstrumentDB.Data;
using MusicInstrumentDB.Models.FamousMusicianModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstrumentDB.Services
{
    public class FamousMusicianService
    {
        private readonly Guid _userId;

        public FamousMusicianService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateMusician(FamousMusicianCreate model)
        {
            var entity =
                new FamousMusician
                {
                    OwnderId = _userId,
                    Description = model.Description,
                    FullName = model.FullName,
                    InstrumentId = model.InstrumentId,
                    MusicGenre = model.MusicGenre
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.FamousMusicians.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
