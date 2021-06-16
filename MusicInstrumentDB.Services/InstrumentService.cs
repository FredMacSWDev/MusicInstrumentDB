using MusicInstrumentDB.Data;
using MusicInstrumentDB.Models.FamousMusicianModels;
using MusicInstrumentDB.Models.InstrumentFamilyModels;
using MusicInstrumentDB.Models.InstrumentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstrumentDB.Services
{
    public class InstrumentService
    {
        public readonly Guid _userId;

        public InstrumentService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateInstrument(InstrumentCreate model)
        {
            var entity =
                new Instrument()
                {
                    OwnerId = _userId,
                    InstrumentName = model.InstrumentName,
                    Description = model.Description,
                    Transposition = model.Transposition,
                    FamilyId = model.FamilyId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Instruments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<InstrumentListItem> GetInstruments()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Instruments
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new InstrumentListItem
                                {
                                    InstrumentId = e.InstrumentId,
                                    InstrumentName = e.InstrumentName,
                                    FamilyId = e.FamilyId,
                                    FamilyName = e.InstrumentFamily.FamilyName
                                }
                        );
                return query.ToArray();
            }
        }

        public InstrumentDetail GetInstrumentById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Instruments
                    .Where(e => e.InstrumentId == id && e.OwnerId == _userId);
                if (entity.Any())
                {
                    var populatedEntity =
                    ctx
                    .Instruments
                    .Single(e => e.InstrumentId == id && e.OwnerId == _userId);
                    if (populatedEntity.FamilyId != null)
                    {
                        return
                            new InstrumentDetail
                            {
                                InstrumentId = populatedEntity.InstrumentId,
                                InstrumentName = populatedEntity.InstrumentName,
                                Description = populatedEntity.Description,
                                Transposition = populatedEntity.Transposition,
                                FamilyId = populatedEntity.FamilyId,
                                InstrumentFamilyName = populatedEntity.InstrumentFamily.FamilyName,

                                FamousMusicians = populatedEntity.FamousMusicians
                                .Select(e => new FamousMusicianListItem()
                                {
                                    FamousMusicianId = e.FamousMusicianId,
                                    FullName = e.FullName,
                                    InstrumentId = e.InstrumentId,
                                    InstrumentName = e.Instrument.InstrumentName,
                                    MusicGenre = e.MusicGenre
                                }).ToList()
                            };
                    }
                    else
                    {
                        return
                         new InstrumentDetail
                         {
                             InstrumentId = populatedEntity.InstrumentId,
                             InstrumentName = populatedEntity.InstrumentName,
                             Description = populatedEntity.Description,
                             Transposition = populatedEntity.Transposition,
                             InstrumentFamilyName = "family does not exist",

                             FamousMusicians = populatedEntity.FamousMusicians
                            .Select(e => new FamousMusicianListItem()
                            {
                                FamousMusicianId = e.FamousMusicianId,
                                FullName = e.FullName,
                                InstrumentId = e.InstrumentId,
                                InstrumentName = e.Instrument.InstrumentName,
                                MusicGenre = e.MusicGenre
                            }).ToList()
                         };
                    }
                }
                return null;
            }
        }

        public bool UpdateInstrument(InstrumentEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Instruments
                    .Single(e => e.InstrumentId == model.InstrumentId && e.OwnerId == _userId);

                entity.InstrumentName = model.InstrumentName;
                entity.Description = model.Description;
                entity.Transposition = model.Transposition;
                entity.FamilyId = model.FamilyId;
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteInstrument(int instrumentId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Instruments
                    .Where(e => e.InstrumentId == instrumentId && e.OwnerId == _userId);
                if (entity.Any())
                {
                    var populatedEntity =
                    ctx
                    .Instruments
                    .Single(e => e.InstrumentId == instrumentId && e.OwnerId == _userId);

                    var entity2 =
                    ctx
                    .FamousMusicians
                    .Where(e => e.InstrumentId == instrumentId);

                if (entity2 != null)
                    return false;

                ctx.Instruments.Remove(populatedEntity);
                return ctx.SaveChanges() == 1;
                }
                return false;
            }
        }

        public IEnumerable<InstrumentDetail> GetInstrumentByName(string instrumentName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx.Instruments
                    .Where(e => e.InstrumentName.ToLower().Contains(instrumentName.ToLower()) && e.OwnerId == _userId)
                    .Select(
                        e =>
                            new InstrumentDetail
                            {
                                InstrumentId = e.InstrumentId,
                                InstrumentName = e.InstrumentName,
                                FamilyId = e.FamilyId,
                                InstrumentFamilyName = e.InstrumentFamily.FamilyName,
                                Description = e.Description,
                                Transposition = e.Transposition
                            }
                        );
                return entity.ToArray();
            }
        }
    }
}
