using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class UpdateUserDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Birthdate",
                table: "Users",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCode",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeTypeId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeTypeCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "DepartmentCode", "Description", "TenantId" },
                values: new object[] { new Guid("43db034a-98cc-42ee-8fff-c57016484fdd"), "A000", "Testing", 1 });

            migrationBuilder.InsertData(
                table: "EmployeeTypes",
                columns: new[] { "Id", "Description", "EmployeeTypeCode", "TenantId" },
                values: new object[] { new Guid("43db034a-98cc-42ee-8fff-c5701648dddd"), "Testing", "A0000", 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6e5d8fa8-fa96-419f-9c07-3e05b96b087e"),
                columns: new[] { "Address", "Birthdate", "EmployeeCode", "PasswordHash", "PasswordSalt", "PhoneNumber", "TenantId" },
                values: new object[] { "123 Admin Street", new DateOnly(1900, 1, 1), "admin", new byte[] { 106, 254, 11, 233, 26, 46, 143, 143, 101, 232, 232, 236, 170, 74, 243, 128, 118, 137, 9, 222, 161, 151, 255, 193, 139, 132, 114, 186, 64, 54, 253, 192, 69, 217, 62, 8, 172, 244, 72, 161, 237, 1, 142, 226, 17, 36, 46, 38, 162, 72, 205, 49, 86, 24, 117, 200, 104, 122, 59, 194, 6, 203, 87, 49 }, new byte[] { 207, 137, 195, 62, 96, 110, 152, 18, 40, 95, 31, 110, 159, 179, 150, 188, 248, 235, 178, 145, 69, 102, 77, 216, 199, 9, 212, 169, 40, 228, 41, 224, 242, 210, 39, 185, 32, 246, 43, 55, 229, 152, 134, 83, 114, 164, 190, 18, 159, 90, 130, 180, 164, 64, 247, 248, 108, 167, 7, 35, 6, 6, 254, 235, 21, 67, 102, 114, 188, 199, 207, 141, 170, 75, 192, 172, 251, 49, 175, 237, 98, 131, 160, 69, 130, 122, 207, 202, 212, 196, 207, 122, 135, 102, 2, 202, 123, 220, 158, 231, 178, 22, 187, 89, 52, 161, 190, 123, 91, 35, 184, 128, 194, 237, 186, 214, 97, 248, 148, 55, 247, 35, 77, 18, 93, 42, 2, 206 }, "012345678", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeTypeId",
                table: "Users",
                column: "EmployeeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_EmployeeTypes_EmployeeTypeId",
                table: "Users",
                column: "EmployeeTypeId",
                principalTable: "EmployeeTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_EmployeeTypes_EmployeeTypeId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "EmployeeTypes");

            migrationBuilder.DropIndex(
                name: "IX_Users_DepartmentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EmployeeTypeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Birthdate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmployeeCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmployeeTypeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6e5d8fa8-fa96-419f-9c07-3e05b96b087e"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 30, 26, 213, 8, 103, 48, 182, 118, 120, 144, 112, 216, 242, 40, 40, 207, 239, 96, 119, 49, 33, 171, 55, 121, 126, 118, 156, 120, 77, 215, 156, 72, 179, 105, 183, 56, 0, 241, 155, 101, 195, 120, 71, 19, 202, 24, 89, 175, 55, 34, 225, 108, 10, 165, 18, 204, 237, 134, 207, 84, 99, 154, 47, 187 }, new byte[] { 249, 241, 17, 68, 23, 112, 83, 99, 27, 23, 64, 13, 227, 155, 63, 133, 99, 58, 201, 76, 70, 85, 152, 9, 70, 66, 108, 245, 248, 18, 81, 251, 62, 224, 104, 49, 200, 148, 2, 170, 114, 145, 100, 43, 6, 123, 10, 84, 187, 121, 24, 246, 140, 224, 220, 192, 149, 138, 206, 19, 121, 210, 255, 199, 215, 59, 144, 106, 249, 124, 89, 200, 250, 38, 182, 73, 82, 22, 204, 110, 185, 148, 190, 61, 240, 51, 253, 98, 3, 178, 132, 83, 200, 211, 185, 10, 10, 240, 232, 227, 222, 171, 218, 46, 2, 185, 11, 241, 191, 195, 67, 159, 124, 120, 65, 168, 254, 123, 30, 84, 153, 105, 64, 114, 183, 158, 125, 252 } });
        }
    }
}
