using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstrumentPlatform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_instruments_authorized_id",
                table: "instruments");

            migrationBuilder.AddForeignKey(
                name: "FK_instruments_authorized_id",
                table: "instruments",
                column: "id",
                principalTable: "authorized",
                principalColumn: "instrument_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_instruments_authorized_id",
                table: "instruments");

            migrationBuilder.AddForeignKey(
                name: "FK_instruments_authorized_id",
                table: "instruments",
                column: "id",
                principalTable: "authorized",
                principalColumn: "instrument_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
