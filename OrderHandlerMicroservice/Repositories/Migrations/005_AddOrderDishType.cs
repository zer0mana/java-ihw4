using FluentMigrator;

namespace OrderHandlerMicroservice.Repositories.Migrations;

[Migration(005, TransactionBehavior.None)]
public class AddOrderDishType : Migration {
    public override void Up()
    {
        const string sql = @"
DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'dish_order_v1') THEN
            CREATE TYPE calculations_v1 as
            (
                  id           int
                , order_id     int
                , dish_id      int
                , quantity     int
                , price        numeric(19, 5)
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
        DROP TYPE IF EXISTS dish_order_v1;
    END
$$;";

        Execute.Sql(sql);
    }
}