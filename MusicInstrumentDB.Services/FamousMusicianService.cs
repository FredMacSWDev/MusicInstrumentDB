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

        public IEnumerable<FamousMusicianListItem> GetMusicians()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                  ctx
                      .FamousMusicians
                      .Where(e => e.OwnderId == _userId)
                      .Select(
                      e =>
                         new FamousMusicianListItem
                         {
                             FamousMusicianId = e.FamousMusicianId,
                             FullName = e.FullName,
                             InstrumentId = e.InstrumentId,
                             MusicGenre = e.MusicGenre
                         }
                      );
                return query.ToArray();
            }
        }

        public FamousMusicianDetail GetMusicianById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                     ctx
                         .FamousMusicians
                         .Single(e => e.FamousMusicianId == id && e.OwnderId == _userId);
                return
                    new FamousMusicianDetail
                    {
                        FamousMusicianId = entity.FamousMusicianId,
                        FullName = entity.FullName,
                        InstrumentId = entity.InstrumentId,
                        MusicGenre = entity.MusicGenre,
                        Description = entity.Description
                    };
            }
        }

        public bool UpdateMusician(FamousMusicianEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .FamousMusicians
                        .Single(e => e.FamousMusicianId ==  model.FamousMusicianId && e.OwnderId == _userId);

                entity.FullName = model.FullName;
                entity.Description = model.Description;
                entity.InstrumentId = model.InstrumentId;
                entity.MusicGenre = model.MusicGenre;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteMusician(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .FamousMusicians
                        .Single(e => e.FamousMusicianId == id && e.OwnderId == _userId);
                ctx.FamousMusicians.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}