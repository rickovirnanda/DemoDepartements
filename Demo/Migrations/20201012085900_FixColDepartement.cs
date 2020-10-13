using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoService.migrations
{
    public partial class FixColDepartement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employees_departements_departement_id",
                table: "employees");

            migrationBuilder.DropPrimaryKey(
                name: "pk_departements",
                table: "departements");

            migrationBuilder.DropColumn(
                name: "departemen_id",
                table: "employees");

            migrationBuilder.RenameTable(
                name: "departements",
                newName: "departments");

            migrationBuilder.AlterColumn<long>(
                name: "departement_id",
                table: "employees",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<long>(
                name: "departement_id",
                table: "employees",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "departemen_id",
                table: "employees",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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
                onDelete: ReferentialAction.Restrict);
        }
    }
}
