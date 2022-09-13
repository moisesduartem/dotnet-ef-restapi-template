using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestApi.Identity.Migrations
{
    public partial class AddAdminUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "38eaed59-24bb-4b3a-91bf-13cc7cc83d42", "39cbe72e-b53b-4117-8d06-4aa1f053788e", "Admin", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38eaed59-24bb-4b3a-91bf-13cc7cc83d42");
        }
    }
}
