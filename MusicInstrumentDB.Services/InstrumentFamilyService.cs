using MusicInstrumentDB.Data;
using MusicInstrumentDB.Models.InstrumentFamilyModels;
using MusicInstrumentDB.Models.InstrumentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstrumentDB.Services
{
    public class InstrumentFamilyService
    {
        private readonly Guid _userId;
        public InstrumentFamilyService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateInstrumentFamily(InstrumentFamilyCreate model)
        {
            var entity =
                new InstrumentFamily()
                {
                    OwnerId = _userId,
                    FamilyName = model.FamilyName,
                    Description = model.Description,
                    Classification = model.Classification,
                    Tuning = model.Tuning
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.InstrumentFamilies.Add(entity);
                return ctx.SaveChanges() == 1;

            }
        }
        public IEnumerable<InstrumentFamilyListItem> GetInstrumentFamilies()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .InstrumentFamilies
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new InstrumentFamilyListItem
                                {
                                    FamilyId = e.FamilyId,
                                    FamilyName = e.FamilyName,
                                    Description = e.Description,
                                    Classification = e.Classification,
                                    Tuning = e.Tuning
                                });
                return query.ToArray();
            }
        }
        public InstrumentFamilyDetail GetInstrumentFamilyById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .InstrumentFamilies
                        .Single(e => e.FamilyId == id && e.OwnerId == _userId);
                return
                    new InstrumentFamilyDetail
                    {
                        FamilyId = entity.FamilyId,
                        FamilyName = entity.FamilyName,
                        Description = entity.Description,
                        Classification = entity.Classification,
                        Tuning = entity.Tuning,
                        Instruments = entity.Instruments
                        .Select(e => new InstrumentListItem()
                        {
                            FamilyId = e.FamilyId,
                            FamilyName = e.InstrumentFamily.FamilyName,
                            InstrumentId = e.InstrumentId
                        }).ToList()
                    };
            }
        }

        public bool UpdateInstrumentFamily(InstrumentFamilyEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .InstrumentFamilies
                        .Single(e => e.FamilyId == model.FamilyId && e.OwnerId == _userId);

                entity.FamilyName = model.FamilyName;
                entity.Description = model.Description;
                entity.Classification = model.Classification;
                entity.Tuning = model.Tuning;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteInstrumentFamily(int familyId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .InstrumentFamilies
                        .Single(e => e.FamilyId == familyId && e.OwnerId == _userId);

                var entity2 =
                    ctx
                        .Instruments
                        .Single(e => e.FamilyId == familyId);

                if (entity2.FamilyId == entity.FamilyId)
                {
                    return false;
                }

                ctx.InstrumentFamilies.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

