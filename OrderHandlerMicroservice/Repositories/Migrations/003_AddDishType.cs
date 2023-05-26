using FluentMigrator;

namespace OrderHandlerMicroservice.Repositories.Migrations;

[Migration(003, TransactionBehavior.None)]
public class AddDishType : Migration {
    public override void Up()
    {
        const string sql = @"
DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'dish_v1') THEN
            CREATE TYPE order_v1 as
            (
                  id           int
                , name         varchar
                , decription   text
                , price        numeric(19, 5)
                , quantity     int
            );
        END IF;
    END
$$;";
        
        Execute.Sql(sql);
    }

    public override void Down()
    {
        const string sql = @"
DO $$
    BEGIN
        DROP TYPE IF EXISTS dish_v1;
    END
$$;";

        Execute.Sql(sql);
    }
}