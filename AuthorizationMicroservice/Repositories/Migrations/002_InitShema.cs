using FluentMigrator;

namespace AuthorizationMicroservice.Repositories.Migrations;

[Migration(002, TransactionBehavior.None)]
public class InitShema : Migration {
    public override void Up()
    {
        Create.Table("user")
            .WithColumn("id").AsInt32().PrimaryKey("dish_pk").Identity()
            .WithColumn("username").AsString().NotNullable().Unique()
            .WithColumn("email").AsString().NotNullable().Unique()
            .WithColumn("password_hash").AsString().NotNullable()
            .WithColumn("role").AsString().NotNullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable()
            .WithColumn("updated_at").AsDateTimeOffset().NotNullable();

        Create.Table("session")
            .WithColumn("id").AsInt32().PrimaryKey("order_pk").Identity()
            .WithColumn("user_id").AsInt32().ForeignKey().NotNullable()
            .WithColumn("session_token").AsString().NotNullable()
            .WithColumn("expires_at").AsDateTimeOffset().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("user");
        Delete.Table("session");
    }
}