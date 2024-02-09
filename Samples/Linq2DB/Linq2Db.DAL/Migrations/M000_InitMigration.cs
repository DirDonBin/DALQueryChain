using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2Db.DAL.Migrations
{
    [Migration(20240209084700)]
    public class M000_InitMigration : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Category")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable();

            Create.Table("Products")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("Price").AsDecimal().Nullable()
                .WithColumn("Count").AsInt32().Nullable()
                .WithColumn("Categoryid").AsInt32().Nullable().ForeignKey("Category", "Id")
                .WithColumn("Raiting").AsDouble().Nullable();

            Create.Table("ArchiveProducts")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("Price").AsDecimal().Nullable()
                .WithColumn("Count").AsInt32().Nullable()
                .WithColumn("Categoryid").AsInt32().Nullable().ForeignKey("Category", "Id")
                .WithColumn("Raiting").AsDouble().Nullable();
        }
    }
}
