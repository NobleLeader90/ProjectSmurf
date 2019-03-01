using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationSmurf.Migrations.Student
{
    public partial class StudentPeriodAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Period1",
                table: "Student",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Period2",
                table: "Student",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Period3",
                table: "Student",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Period4",
                table: "Student",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Period5",
                table: "Student",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Period6",
                table: "Student",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period1",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Period2",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Period3",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Period4",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Period5",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Period6",
                table: "Student");
        }
    }
}
