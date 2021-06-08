using MusicInstrumentDB.Data;
using MusicInstrumentDB.Models.InstrumentFamilyModels;
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
                    FamilyName = model.FamilyName                  
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
                                    FamilyName = e.FamilyName
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
                        Instruments = entity.Instruments
                        .Select(e => new InstrumentListItem()
                        {
                            InstrumentId = e.InstrumentId,
                            FullName = e.FullName,
                            FamilyName = e.FamilyName

                        }).ToList()
                    };
            }
        }

        public bool UpdateInstrumentFamily(InstrumentFamilyEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .InstrumentFamilies
                        .Single(e => e.FamilyId == model.FamilyId && e.OwnerId == _userId);

                entity.FamilyName = model.FamilyName;

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

                ctx.InstrumentFamilies.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

