using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo.Migrations
{
    public partial class FixForeignKeyDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "departemen_id",
                table: "employees",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "departemen_id",
                table: "employees");
        }
    }
}
