using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Base.Samples.Infra.Data.Sql.Migrations
{
    /// <inheritdoc />
    public partial class SetStore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "People",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<long>(
                name: "StoreId",
                table: "People",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_StoreId",
                table: "People",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Store_StoreId",
                table: "People",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Store_StoreId",
                table: "People");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropIndex(
                name: "IX_People_StoreId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "People");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "People",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
