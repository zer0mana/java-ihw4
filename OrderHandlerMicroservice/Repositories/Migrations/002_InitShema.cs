using FluentMigrator;

namespace OrderHandlerMicroservice.Repositories.Migrations;

[Migration(002, TransactionBehavior.None)]
public class InitShema : Migration {
    public override void Up()
    {
        Create.Table("dish")
            .WithColumn("id").AsInt32().PrimaryKey("dish_pk").Identity()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("description").AsString().NotNullable()
            .WithColumn("price").AsDecimal().NotNullable()
            .WithColumn("quantity").AsInt32().NotNullable();

        Create.Table("order")
            .WithColumn("id").AsInt32().PrimaryKey("order_pk").Identity()
            .WithColumn("user_id").AsInt32().ForeignKey().NotNullable()
            .WithColumn("status").AsString().NotNullable()
            .WithColumn("special_requests").AsString().NotNullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("updated_at").AsDateTimeOffset().NotNullable();

        Create.Table("order_dish")
            .WithColumn("id").AsInt32().PrimaryKey("order_dish_pk").Identity()
            .WithColumn("order_id").AsInt32().ForeignKey().NotNullable()
            .WithColumn("dish_id").AsInt32().ForeignKey().NotNullable()
            .WithColumn("quantity").AsInt32().NotNullable()
            .WithColumn("price").AsDecimal().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("dish");
        Delete.Table("order");
        Delete.Table("order_dish");
    }
}