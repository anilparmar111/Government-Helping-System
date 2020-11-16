using Microsoft.EntityFrameworkCore.Migrations;

namespace Government_Helping_System.Migrations
{
    public partial class employeeidadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_photoModels_queries_QuerieId",
                table: "photoModels");

            migrationBuilder.RenameColumn(
                name: "QuerieId",
                table: "photoModels",
                newName: "querieId");

            migrationBuilder.RenameIndex(
                name: "IX_photoModels_QuerieId",
                table: "photoModels",
                newName: "IX_photoModels_querieId");

            migrationBuilder.AddColumn<string>(
                name: "Main_Emp_Id",
                table: "queries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_photoModels_queries_querieId",
                table: "photoModels",
                column: "querieId",
                principalTable: "queries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_photoModels_queries_querieId",
                table: "photoModels");

            migrationBuilder.DropColumn(
                name: "Main_Emp_Id",
                table: "queries");

            migrationBuilder.RenameColumn(
                name: "querieId",
                table: "photoModels",
                newName: "QuerieId");

            migrationBuilder.RenameIndex(
                name: "IX_photoModels_querieId",
                table: "photoModels",
                newName: "IX_photoModels_QuerieId");

            migrationBuilder.AddForeignKey(
                name: "FK_photoModels_queries_QuerieId",
                table: "photoModels",
                column: "QuerieId",
                principalTable: "queries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
