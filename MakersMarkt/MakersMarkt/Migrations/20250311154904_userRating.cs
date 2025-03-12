using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MakersMarkt.Migrations
{
    /// <inheritdoc />
    public partial class userRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Users",
                type: "float",
                nullable: false,
                defaultValue: 0f);

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
                columns: new[] { "Password", "Rating" },
                values: new object[] { "$2a$11$Y3LnZPIK36z3uRym7mbVtex4ELB8C4645/tFiBOkLOScoWFq1Gk/e", 4f });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Password", "Rating" },
                values: new object[] { "$2a$11$UT.tHfq3BZViK9B7LTujg.6miCOf5iQ6eaP/gfv2cG3rtU9OAv0U2", 0f });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "PasswordResets",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 11, 10, 41, 21, 720, DateTimeKind.Local).AddTicks(4775));

            migrationBuilder.UpdateData(
                table: "PasswordResets",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 11, 10, 41, 21, 720, DateTimeKind.Local).AddTicks(4777));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$/kpvEHAiTkZzmpwudeiiZujCdz43vCW/s4NOCsWov4.yFoK/J7lXa");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$1ZE7/mIq6j/xIKmRZHolReJu4pPWAG/zBP1DOsrEptGSP18juQHWy");
        }
    }
}
