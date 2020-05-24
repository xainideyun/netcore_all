using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HT.Future.Entities.Migrations
{
    public partial class ddd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginTime",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Role",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "IsActive", "IsAdmin", "LockoutEnabled", "PhoneNumberConfirmed", "TwoFactorEnabled" },
                values: new object[] { "7128e87f-59dc-472f-a7ec-ee913f046882", false, true, true, false, false, false });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "IsActive", "IsAdmin", "LockoutEnabled", "PhoneNumberConfirmed", "TwoFactorEnabled" },
                values: new object[] { "722a1056-d3c5-4c25-8148-10ea761876a1", false, true, true, false, false, false });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "IsActive", "IsAdmin", "LockoutEnabled", "PhoneNumberConfirmed", "TwoFactorEnabled" },
                values: new object[] { "b327bf05-398a-4836-8fab-3aed74a2d240", false, true, false, false, false, false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "User");

            migrationBuilder.DropColumn(
                name: "LastLoginTime",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Role");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "IsActive", "IsAdmin", "LockoutEnabled", "PhoneNumberConfirmed", "TwoFactorEnabled" },
                values: new object[] { "37719c9e-6c23-49be-b9e9-67d8d300a9db", (short)0, (short)1, (short)1, (short)0, (short)0, (short)0 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "IsActive", "IsAdmin", "LockoutEnabled", "PhoneNumberConfirmed", "TwoFactorEnabled" },
                values: new object[] { "3d584083-3b70-4c33-924f-8e7fbcbd659f", (short)0, (short)1, (short)1, (short)0, (short)0, (short)0 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "IsActive", "IsAdmin", "LockoutEnabled", "PhoneNumberConfirmed", "TwoFactorEnabled" },
                values: new object[] { "960cae28-5844-4502-9332-05c2d4c696c5", (short)0, (short)1, (short)0, (short)0, (short)0, (short)0 });
        }
    }
}
