using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstrumentPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorizedInstruments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authorized",
                columns: table => new
                {
                    DeviceId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authorized", x => x.DeviceId);
                });

            migrationBuilder.InsertData(
                table: "authorized",
                column: "DeviceId",
                values: new object[]
                {
                    "H2-00000",
                    "TC-00000"
                });

            migrationBuilder.AddForeignKey(
                name: "FK_instruments_authorized_device_id",
                table: "instruments",
                column: "device_id",
                principalTable: "authorized",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_instruments_authorized_device_id",
                table: "instruments");

            migrationBuilder.DropTable(
                name: "authorized");
        }
    }
}
