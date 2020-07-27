using FluentMigrator;
using System.Data.SqlTypes;

namespace Migrations
{
    [Migration(100)]
    public class M100AddDogTable : Migration
    {
        public override void Up()
        {
            Create.Table("Dog")
                .WithColumn("DogId").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("Birthdate").AsDateTime()
                .WithColumn("AdoptedDate").AsDateTime()
                .WithColumn("Gender").AsInt32()
                .WithColumn("Created").AsDateTime()
                .WithColumn("Modified").AsDateTime()
                .WithColumn("Deleted").AsDateTime();
        }

        public override void Down()
        {
            Delete.Table("Dog");
        }
    }
}