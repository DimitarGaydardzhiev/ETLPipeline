using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETL.Data.Migrations
{
    /// <inheritdoc />
    public partial class TransactionStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TYPE TransactionType AS TABLE
                (
                    Id UNIQUEIDENTIFIER,
                    CustomerID INT,
                    Amount DECIMAL(18, 2),
                    TransactionDate DATETIME
                );
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE usp_InsertTransactions
                    @Transactions TransactionType READONLY
                AS
                BEGIN
                    SET NOCOUNT ON;
                    INSERT INTO Transactions (Id, CustomerID, Amount, TransactionDate)
                    SELECT Id, CustomerID, Amount, TransactionDate
                    FROM @Transactions;
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS usp_InsertTransactions;");
            migrationBuilder.Sql("DROP TYPE IF EXISTS TransactionType;");
        }
    }
}
