using FluentMigrator;
using System.Data.SqlTypes;

namespace Migrations
{
    [Migration(101)]
    public class M101AddHealthTable : Migration
    {
        public override void Up()
        {
            Create.Table("Health")
                .WithColumn("HealthId").AsInt64().PrimaryKey().Identity()
                .WithColumn("DogId").AsInt64().ForeignKey("Dog", "DogId")
                .WithColumn("Weight").AsDecimal()
                .WithColumn("Height").AsDecimal()
                .WithColumn("Length").AsDecimal()
                .WithColumn("Waist").AsDecimal()
                .WithColumn("CreationDateTime").AsDateTime()
                .WithColumn("ModificationDateTime").AsDateTime()
                .WithColumn("DeletionDateTime").AsDateTime();
        }

        public override void Down()
        {
            Delete.Table("Dog");
        }
    }
}