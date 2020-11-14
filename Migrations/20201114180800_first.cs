using Microsoft.EntityFrameworkCore.Migrations;

namespace Government_Helping_System.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoModel_queries_querieId",
                table: "PhotoModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhotoModel",
                table: "PhotoModel");

            migrationBuilder.DropColumn(
                name: "QueriesId",
                table: "PhotoModel");

            migrationBuilder.RenameTable(
                name: "PhotoModel",
                newName: "photoModels");

            migrationBuilder.RenameColumn(
                name: "querieId",
                table: "photoModels",
                newName: "QuerieId");

            migrationBuilder.RenameIndex(
                name: "IX_PhotoModel_querieId",
                table: "photoModels",
                newName: "IX_photoModels_QuerieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_photoModels",
                table: "photoModels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_photoModels_queries_QuerieId",
                table: "photoModels",
                column: "QuerieId",
                principalTable: "queries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_photoModels_queries_QuerieId",
                table: "photoModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_photoModels",
                table: "photoModels");

            migrationBuilder.RenameTable(
                name: "photoModels",
                newName: "PhotoModel");

            migrationBuilder.RenameColumn(
                name: "QuerieId",
                table: "PhotoModel",
                newName: "querieId");

            migrationBuilder.RenameIndex(
                name: "IX_photoModels_QuerieId",
                table: "PhotoModel",
                newName: "IX_PhotoModel_querieId");

            migrationBuilder.AddColumn<string>(
                name: "QueriesId",
                table: "PhotoModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhotoModel",
                table: "PhotoModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoModel_queries_querieId",
                table: "PhotoModel",
                column: "querieId",
                principalTable: "queries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
