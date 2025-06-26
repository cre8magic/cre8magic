using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;

namespace ToSic.Module.Cre8magicTests.Repository
{
    public class Cre8magicTestsRepository : ICre8magicTestsRepository, ITransientService
    {
        private readonly IDbContextFactory<Cre8magicTestsContext> _factory;

        public Cre8magicTestsRepository(IDbContextFactory<Cre8magicTestsContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.Cre8magicTests> GetCre8magicTestss(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return db.Cre8magicTests.Where(item => item.ModuleId == ModuleId).ToList();
        }

        public Models.Cre8magicTests GetCre8magicTests(int Cre8magicTestsId)
        {
            return GetCre8magicTests(Cre8magicTestsId, true);
        }

        public Models.Cre8magicTests GetCre8magicTests(int Cre8magicTestsId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.Cre8magicTests.Find(Cre8magicTestsId);
            }
            else
            {
                return db.Cre8magicTests.AsNoTracking().FirstOrDefault(item => item.Cre8magicTestsId == Cre8magicTestsId);
            }
        }

        public Models.Cre8magicTests AddCre8magicTests(Models.Cre8magicTests Cre8magicTests)
        {
            using var db = _factory.CreateDbContext();
            db.Cre8magicTests.Add(Cre8magicTests);
            db.SaveChanges();
            return Cre8magicTests;
        }

        public Models.Cre8magicTests UpdateCre8magicTests(Models.Cre8magicTests Cre8magicTests)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(Cre8magicTests).State = EntityState.Modified;
            db.SaveChanges();
            return Cre8magicTests;
        }

        public void DeleteCre8magicTests(int Cre8magicTestsId)
        {
            using var db = _factory.CreateDbContext();
            Models.Cre8magicTests Cre8magicTests = db.Cre8magicTests.Find(Cre8magicTestsId);
            db.Cre8magicTests.Remove(Cre8magicTests);
            db.SaveChanges();
        }
    }
}
