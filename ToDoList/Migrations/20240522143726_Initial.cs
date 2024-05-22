using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocalDB_Categories_CategoryId",
                table: "LocalDB");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalDB_Statuses_StatusId",
                table: "LocalDB");

            migrationBuilder.DropIndex(
                name: "IX_LocalDB_CategoryId",
                table: "LocalDB");

            migrationBuilder.DropIndex(
                name: "IX_LocalDB_StatusId",
                table: "LocalDB");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "LocalDB",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "LocalDB",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CategoryId1",
                table: "LocalDB",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StatusId1",
                table: "LocalDB",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocalDB_CategoryId1",
                table: "LocalDB",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_LocalDB_StatusId1",
                table: "LocalDB",
                column: "StatusId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalDB_Categories_CategoryId1",
                table: "LocalDB",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocalDB_Statuses_StatusId1",
                table: "LocalDB",
                column: "StatusId1",
                principalTable: "Statuses",
                principalColumn: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocalDB_Categories_CategoryId1",
                table: "LocalDB");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalDB_Statuses_StatusId1",
                table: "LocalDB");

            migrationBuilder.DropIndex(
                name: "IX_LocalDB_CategoryId1",
                table: "LocalDB");

            migrationBuilder.DropIndex(
                name: "IX_LocalDB_StatusId1",
                table: "LocalDB");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "LocalDB");

            migrationBuilder.DropColumn(
                name: "StatusId1",
                table: "LocalDB");

            migrationBuilder.AlterColumn<string>(
                name: "StatusId",
                table: "LocalDB",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryId",
                table: "LocalDB",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_LocalDB_CategoryId",
                table: "LocalDB",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalDB_StatusId",
                table: "LocalDB",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalDB_Categories_CategoryId",
                table: "LocalDB",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocalDB_Statuses_StatusId",
                table: "LocalDB",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
