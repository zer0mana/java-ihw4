using FluentMigrator;

namespace AuthorizationMicroservice.Repositories.Migrations;

[Migration(004, TransactionBehavior.None)]
public class AddSessionType : Migration {
    public override void Up()
    {
        const string sql = @"
DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'session_v1') THEN
            CREATE TYPE session_v1 as
            (
                  id               int
                , user_id          int
                , session_token    varchar
                , expires_at       timestamp with time zone
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
        DROP TYPE IF EXISTS session_v1;
    END
$$;";

        Execute.Sql(sql);
    }
}