using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OperationSmurf.Migrations.Section
{
    public partial class correctlyremoveRosterFromSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    Period1 = table.Column<int>(nullable: false),
                    Period2 = table.Column<int>(nullable: false),
                    Period3 = table.Column<int>(nullable: false),
                    Period4 = table.Column<int>(nullable: false),
                    Period5 = table.Column<int>(nullable: false),
                    Period6 = table.Column<int>(nullable: false),
                    SectionId = table.Column<int>(nullable: true),
                    StudentId = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_SectionId",
                table: "Student",
                column: "SectionId");
        }
    }
}
