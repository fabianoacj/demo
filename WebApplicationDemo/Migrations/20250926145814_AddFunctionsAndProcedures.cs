using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationDemo.Migrations
{
    public partial class AddFunctionsAndProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE FUNCTION dbo.GetProductsJson()
                RETURNS NVARCHAR(MAX)
                AS
                BEGIN
                    DECLARE @json NVARCHAR(MAX);
                    SELECT @json = (
                        SELECT Id, Name, Price, StoreId
                        FROM Products
                        FOR JSON AUTO
                    );
                    RETURN @json;
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE dbo.InsertProduct
                    @Id UNIQUEIDENTIFIER,
                    @Name NVARCHAR(100),
                    @Price DECIMAL(18,2),
                    @StoreId UNIQUEIDENTIFIER
                AS
                BEGIN
                    INSERT INTO Products (Id, Name, Price, StoreId)
                    VALUES (@Id, @Name, @Price, @StoreId);
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS dbo.GetProductsJson;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.InsertProduct;");
        }
    }
}
