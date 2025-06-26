using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using ToSic.Module.Cre8magicTests.Migrations.EntityBuilders;
using ToSic.Module.Cre8magicTests.Repository;

namespace ToSic.Module.Cre8magicTests.Migrations
{
    [DbContext(typeof(Cre8magicTestsContext))]
    [Migration("ToSic.Module.Cre8magicTests.01.00.00.00")]
    public class InitializeModule : MultiDatabaseMigration
    {
        public InitializeModule(IDatabase database) : base(database)
        {
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var entityBuilder = new Cre8magicTestsEntityBuilder(migrationBuilder, ActiveDatabase);
            entityBuilder.Create();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var entityBuilder = new Cre8magicTestsEntityBuilder(migrationBuilder, ActiveDatabase);
            entityBuilder.Drop();
        }
    }
}
