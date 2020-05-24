using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace HT.Future.Entities.Migrations
{
    public partial class aaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SysFunc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysFunc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysFunc_SysFunc_ParentId",
                        column: x => x.ParentId,
                        principalTable: "SysFunc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SysFuncRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SysFuncId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysFuncRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysFuncRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SysFuncRole_SysFunc_SysFuncId",
                        column: x => x.SysFuncId,
                        principalTable: "SysFunc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SysFunc",
                columns: new[] { "Id", "Name", "ParentId", "Title" },
                values: new object[,]
                {
                    { 1, "good", null, "商品管理" },
                    { 2, "settings", null, "系统设置" }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "IsActive", "IsAdmin", "LockoutEnabled", "PhoneNumberConfirmed", "TwoFactorEnabled" },
                values: new object[] { "b524ee9e-101c-4abd-a13b-036a4b7766aa", false, true, true, false, false, false });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "IsActive", "IsAdmin", "LockoutEnabled", "PhoneNumberConfirmed", "TwoFactorEnabled" },
                values: new object[] { "6f48ac90-7588-4a62-a81c-c7c00a1bb1ac", false, true, true, false, false, false });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "IsActive", "IsAdmin", "LockoutEnabled", "PhoneNumberConfirmed", "TwoFactorEnabled" },
                values: new object[] { "03b930b8-2a4b-4a50-a304-d20fa1c5a68d", false, true, false, false, false, false });

            migrationBuilder.InsertData(
                table: "SysFunc",
                columns: new[] { "Id", "Name", "ParentId", "Title" },
                values: new object[,]
                {
                    { 3, "goodList", 1, "商品列表" },
                    { 4, "goodDetail", 1, "商品详情" },
                    { 5, "user", 2, "个人中心" },
                    { 6, "sys", 2, "配置" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SysFunc_ParentId",
                table: "SysFunc",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SysFuncRole_RoleId",
                table: "SysFuncRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SysFuncRole_SysFuncId",
                table: "SysFuncRole",
                column: "SysFuncId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysFuncRole");

            migrationBuilder.DropTable(
                name: "SysFunc");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "IsActive", "IsAdmin", "LockoutEnabled", "PhoneNumberConfirmed", "TwoFactorEnabled" },
                values: new object[] { "7128e87f-59dc-472f-a7ec-ee913f046882", (short)0, (short)1, (short)1, (short)0, (short)0, (short)0 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "IsActive", "IsAdmin", "LockoutEnabled", "PhoneNumberConfirmed", "TwoFactorEnabled" },
                values: new object[] { "722a1056-d3c5-4c25-8148-10ea761876a1", (short)0, (short)1, (short)1, (short)0, (short)0, (short)0 });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "IsActive", "IsAdmin", "LockoutEnabled", "PhoneNumberConfirmed", "TwoFactorEnabled" },
                values: new object[] { "b327bf05-398a-4836-8fab-3aed74a2d240", (short)0, (short)1, (short)0, (short)0, (short)0, (short)0 });
        }
    }
}
