using FluentMigrator;

namespace OrderHandlerMicroservice.Repositories.Migrations;

[Migration(004, TransactionBehavior.None)]
public class AddDihsType : Migration {
    public override void Up()
    {
        const string sql = @"
DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'order_v1') THEN
            CREATE TYPE calculations_v1 as
            (
                  id               int
                , user_id          int
                , status           varchar
                , special_requests text
                , created_at       timestamp with time zone
                , updated_at       timestamp with time zone
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
        DROP TYPE IF EXISTS order_v1;
    END
$$;";

        Execute.Sql(sql);
    }
}