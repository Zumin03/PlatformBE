using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstrumentPlatform.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_instruments_authorized_device_id",
                table: "instruments");

            migrationBuilder.DropForeignKey(
                name: "FK_measurements_instruments_deviceId",
                table: "measurements");

            migrationBuilder.RenameColumn(
                name: "measuredAt",
                table: "measurements",
                newName: "measured_at");

            migrationBuilder.RenameColumn(
                name: "deviceId",
                table: "measurements",
                newName: "instrument_id");

            migrationBuilder.RenameIndex(
                name: "IX_measurements_deviceId",
                table: "measurements",
                newName: "IX_measurements_instrument_id");

            migrationBuilder.RenameColumn(
                name: "comport",
                table: "instruments",
                newName: "port");

            migrationBuilder.RenameColumn(
                name: "device_name",
                table: "instruments",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "device_id",
                table: "instruments",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DeviceId",
                table: "authorized",
                newName: "instrument_id");

            migrationBuilder.AddForeignKey(
                name: "FK_instruments_authorized_id",
                table: "instruments",
                column: "id",
                principalTable: "authorized",
                principalColumn: "instrument_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_measurements_instruments_instrument_id",
                table: "measurements",
                column: "instrument_id",
                principalTable: "instruments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_instruments_authorized_id",
                table: "instruments");

            migrationBuilder.DropForeignKey(
                name: "FK_measurements_instruments_instrument_id",
                table: "measurements");

            migrationBuilder.RenameColumn(
                name: "measured_at",
                table: "measurements",
                newName: "measuredAt");

            migrationBuilder.RenameColumn(
                name: "instrument_id",
                table: "measurements",
                newName: "deviceId");

            migrationBuilder.RenameIndex(
                name: "IX_measurements_instrument_id",
                table: "measurements",
                newName: "IX_measurements_deviceId");

            migrationBuilder.RenameColumn(
                name: "port",
                table: "instruments",
                newName: "comport");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "instruments",
                newName: "device_name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "instruments",
                newName: "device_id");

            migrationBuilder.RenameColumn(
                name: "instrument_id",
                table: "authorized",
                newName: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_instruments_authorized_device_id",
                table: "instruments",
                column: "device_id",
                principalTable: "authorized",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_measurements_instruments_deviceId",
                table: "measurements",
                column: "deviceId",
                principalTable: "instruments",
                principalColumn: "device_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
