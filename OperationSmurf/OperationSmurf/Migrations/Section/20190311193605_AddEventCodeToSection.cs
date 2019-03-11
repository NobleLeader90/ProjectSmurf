using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationSmurf.Migrations.Section
{
    public partial class AddEventCodeToSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventCode",
                table: "Section",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventCode",
                table: "Section");
        }
    }
}
