using FluentMigrator;

namespace AuthorizationMicroservice.Repositories.Migrations;

[Migration(003, TransactionBehavior.None)]
public class AddUserType : Migration {
    public override void Up()
    {
        const string sql = @"
DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'user_v1') THEN
            CREATE TYPE user_v1 as
            (
                  id                int
                , username          varchar
                , email             varchar
                , password_hash     varchar
                , role              varchar
                , created_at        timestamp with time zone
                , updated_at        timestamp with time zone
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
        DROP TYPE IF EXISTS user_v1;
    END
$$;";

        Execute.Sql(sql);
    }
}