using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resources.Infrastructure.DAL.Migrations;

/// <inheritdoc />
public partial class Init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ResourceBlockades",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                BlockadeOwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                BlockadeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                BlockadeDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                ReleasedOnPurpose = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ResourceBlockades", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Resources",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CreatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Canceled = table.Column<bool>(type: "bit", nullable: false),
                CancelerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CancelDate = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Resources", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ResourceBlockades");

        migrationBuilder.DropTable(
            name: "Resources");
    }
}
