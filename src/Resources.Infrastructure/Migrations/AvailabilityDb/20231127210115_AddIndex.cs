using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resources.Endpoint.Migrations.AvailabilityDb;

/// <inheritdoc />
public partial class AddIndex : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            name: "IX_ResourceBlockades_ResourceId",
            schema: "Availability",
            table: "ResourceBlockades",
            column: "ResourceId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_ResourceBlockades_ResourceId",
            schema: "Availability",
            table: "ResourceBlockades");
    }
}
