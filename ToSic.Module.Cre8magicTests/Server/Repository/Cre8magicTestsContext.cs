using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace ToSic.Module.Cre8magicTests.Repository
{
    public class Cre8magicTestsContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.Cre8magicTests> Cre8magicTests { get; set; }

        public Cre8magicTestsContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.Cre8magicTests>().ToTable(ActiveDatabase.RewriteName("ToSicCre8magicTests"));
        }
    }
}
