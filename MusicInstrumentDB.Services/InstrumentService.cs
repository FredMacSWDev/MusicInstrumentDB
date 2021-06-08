﻿using MusicInstrumentDB.Data;
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
                                    InstrumentFamilyName = e.InstrumentFamily.FamilyName
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
                    .Single(e => e.InstrumentId == id && e.OwnerId == _userId);
                return
                    new InstrumentDetail
                    {
                        InstrumentId = entity.InstrumentId,
                        InstrumentName = entity.InstrumentName,
                        Description = entity.Description,
                        Transposition = entity.Transposition,
                        FamilyId = entity.FamilyId,
                        InstrumentFamily = new InstrumentFamilyListItem() { CategoryId = entity.InstrumentFamily.FamilyId, FamilyName = entity.InstrumentFamily.FamilyName }
                        .Select(e => new FamousMusicianListItem()
                        {
                            FamousMusicianId = e.FamousMusicianId,
                            FamousMusicianName = e.FamousMusicianName,
                            InstrumentId = e.Instrument.InstrumentName
                        }).ToList()
                    };
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
                    .Single(e => e.InstrumentId == instrumentId && e.OwnerId == _userId);

                ctx.Instruments.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
