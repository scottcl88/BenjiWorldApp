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
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Birthdate").AsDateTime().Nullable()
                .WithColumn("AdoptedDate").AsDateTime().Nullable()
                .WithColumn("Gender").AsInt32().NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("Modified").AsDateTime().NotNullable()
                .WithColumn("Deleted").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table("Dog");
        }
    }
}