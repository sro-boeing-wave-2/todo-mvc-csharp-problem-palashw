using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoogleKeepDB.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Keep",
                columns: table => new
                {
                    keepID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(nullable: false),
                    plainText = table.Column<string>(nullable: true),
                    label = table.Column<string>(nullable: true),
                    isPinned = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keep", x => x.keepID);
                });

            migrationBuilder.CreateTable(
                name: "Checklist",
                columns: table => new
                {
                    checklistID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    item = table.Column<string>(nullable: true),
                    keepID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checklist", x => x.checklistID);
                    table.ForeignKey(
                        name: "FK_Checklist_Keep_keepID",
                        column: x => x.keepID,
                        principalTable: "Keep",
                        principalColumn: "keepID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Checklist_keepID",
                table: "Checklist",
                column: "keepID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Checklist");

            migrationBuilder.DropTable(
                name: "Keep");
        }
    }
}
