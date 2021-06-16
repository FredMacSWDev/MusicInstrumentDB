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
                             MusicGenre = e.MusicGenre,
                             InstrumentName = e.Instrument.InstrumentName
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
                         .SingleOrDefault(e => e.FamousMusicianId == id && e.OwnderId == _userId);

                if (entity != null)
                {
                    return
                        new FamousMusicianDetail
                        {
                            FamousMusicianId = entity.FamousMusicianId,
                            FullName = entity.FullName,
                            InstrumentId = entity.InstrumentId,
                            MusicGenre = entity.MusicGenre,
                            Description = entity.Description,
                            InstrumentName = entity.Instrument.InstrumentName
                        };
                }
                return null;
            }
        }

        public bool UpdateMusician(FamousMusicianEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .FamousMusicians
                        .SingleOrDefault(e => e.FamousMusicianId == model.FamousMusicianId && e.OwnderId == _userId);

                if (entity != null)
                {
                    entity.FullName = model.FullName;
                    entity.Description = model.Description;
                    entity.InstrumentId = model.InstrumentId;
                    entity.MusicGenre = model.MusicGenre;

                    return ctx.SaveChanges() == 1;
                }
                return false;
            }
        }

        public bool DeleteMusician(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .FamousMusicians
                        .SingleOrDefault(e => e.FamousMusicianId == id && e.OwnderId == _userId);

                if (entity != null)
                {
                    ctx.FamousMusicians.Remove(entity);
                    return ctx.SaveChanges() == 1;
                }
                return false;
            }
        }
    }
}