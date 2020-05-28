using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HT.Future.Entities.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessAuthority_Role_RoleId",
                table: "AccessAuthority");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreateTime", "EmailConfirmed", "IsActive", "IsAdmin", "LockoutEnabled", "PhoneNumberConfirmed", "TwoFactorEnabled" },
                values: new object[] { "7b7cca3f-e8e2-49c4-abf4-ac8b4a530028", new DateTime(2020, 5, 28, 18, 1, 55, 679, DateTimeKind.Local).AddTicks(201), false, true, true, false, false, false });

            migrationBuilder.AddForeignKey(
                name: "FK_AccessAuthority_Role_RoleId",
                table: "AccessAuthority",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessAuthority_Role_RoleId",
                table: "AccessAuthority");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreateTime", "EmailConfirmed", "IsActive", "IsAdmin", "LockoutEnabled", "PhoneNumberConfirmed", "TwoFactorEnabled" },
                values: new object[] { "06ec1a38-e410-4705-9443-8f2e504775a0", new DateTime(2020, 5, 28, 15, 16, 0, 524, DateTimeKind.Local).AddTicks(3241), (short)0, (short)1, (short)1, (short)0, (short)0, (short)0 });

            migrationBuilder.AddForeignKey(
                name: "FK_AccessAuthority_Role_RoleId",
                table: "AccessAuthority",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
