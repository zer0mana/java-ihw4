﻿using FluentMigrator;

namespace OrderHandlerMicroservice.Repositories.Migrations;

[Migration(005, TransactionBehavior.None)]
public class AddOrderDishType : Migration {
    public override void Up()
    {
        const string sql = @"
DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'calculations_v1') THEN
            CREATE TYPE calculations_v1 as
            (
                  id           bigint
                , user_id      bigint
                , good_ids     bigint[]
                , total_volume double precision
                , total_weight double precision
                , price        numeric(19, 5)
                , at           timestamp with time zone
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
        DROP TYPE IF EXISTS calculations_v1;
    END
$$;";

        Execute.Sql(sql);
    }
}