using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MakersMarkt.Migrations
{
    /// <inheritdoc />
    public partial class UserLoginAttempts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoginAttempts",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "PasswordResets",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 12, 11, 17, 18, 589, DateTimeKind.Local).AddTicks(2709));

            migrationBuilder.UpdateData(
                table: "PasswordResets",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 12, 11, 17, 18, 589, DateTimeKind.Local).AddTicks(2731));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LoginAttempts", "Password" },
                values: new object[] { 0, "$2a$11$gGqrCtyOQbdGfBwVj7Rp1OtB0XHz0yJf2RtAOa5di1mUzN0dx.JAS" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "LoginAttempts", "Password" },
                values: new object[] { 0, "$2a$11$muwxKssX3lqxCgu.H4hvvO0V1snvdVrfF6MQJ4TuyURWvoSAKgHY2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginAttempts",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "PasswordResets",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 11, 16, 49, 4, 506, DateTimeKind.Local).AddTicks(2937));

            migrationBuilder.UpdateData(
                table: "PasswordResets",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 11, 16, 49, 4, 506, DateTimeKind.Local).AddTicks(2939));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$Y3LnZPIK36z3uRym7mbVtex4ELB8C4645/tFiBOkLOScoWFq1Gk/e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$UT.tHfq3BZViK9B7LTujg.6miCOf5iQ6eaP/gfv2cG3rtU9OAv0U2");
        }
    }
}
