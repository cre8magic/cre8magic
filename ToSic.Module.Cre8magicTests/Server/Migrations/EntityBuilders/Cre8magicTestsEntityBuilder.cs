using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace ToSic.Module.Cre8magicTests.Migrations.EntityBuilders
{
    public class Cre8magicTestsEntityBuilder : AuditableBaseEntityBuilder<Cre8magicTestsEntityBuilder>
    {
        private const string _entityTableName = "ToSicCre8magicTests";
        private readonly PrimaryKey<Cre8magicTestsEntityBuilder> _primaryKey = new("PK_ToSicCre8magicTests", x => x.Cre8magicTestsId);
        private readonly ForeignKey<Cre8magicTestsEntityBuilder> _moduleForeignKey = new("FK_ToSicCre8magicTests_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public Cre8magicTestsEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override Cre8magicTestsEntityBuilder BuildTable(ColumnsBuilder table)
        {
            Cre8magicTestsId = AddAutoIncrementColumn(table,"Cre8magicTestsId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Name = AddMaxStringColumn(table,"Name");
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> Cre8magicTestsId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
    }
}
