using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationSmurf.Migrations
{
    public partial class AddEventCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventCode",
                table: "Event",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventCode",
                table: "Event");
        }
    }
}
