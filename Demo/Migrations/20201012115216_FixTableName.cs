using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoService.migrations
{
    public partial class FixTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employees_departments_departement_id",
                table: "employees");

            migrationBuilder.DropPrimaryKey(
                name: "pk_departments",
                table: "departments");

            migrationBuilder.RenameTable(
                name: "departments",
                newName: "departements");

            migrationBuilder.AddPrimaryKey(
                name: "pk_departements",
                table: "departements",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_employees_departements_departement_id",
                table: "employees",
                column: "departement_id",
                principalTable: "departements",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employees_departements_departement_id",
                table: "employees");

            migrationBuilder.DropPrimaryKey(
                name: "pk_departements",
                table: "departements");

            migrationBuilder.RenameTable(
                name: "departements",
                newName: "departments");

            migrationBuilder.AddPrimaryKey(
                name: "pk_departments",
                table: "departments",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_employees_departments_departement_id",
                table: "employees",
                column: "departement_id",
                principalTable: "departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
