using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resources.Endpoint.Migrations.AvailabilityDb;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "Availability");

        migrationBuilder.CreateTable(
            name: "ResourceBlockades",
            schema: "Availability",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                BlockadeOwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                BlockadeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                BlockadeDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                ReleasedOnPurpose = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ResourceBlockades", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ResourceBlockades",
            schema: "Availability");
    }
}
