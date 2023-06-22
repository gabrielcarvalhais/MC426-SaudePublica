using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MC426_Backend.Migrations
{
    /// <inheritdoc />
    public partial class EmailUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Paciente",
                type: "VARCHAR(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Funcionario",
                type: "VARCHAR(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93c1d0a4-2a15-4b80-b062-be9bac57c979",
                column: "ConcurrencyStamp",
                value: "f7aa1572-718b-4ae2-a62b-2f40439ad8d8");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4344bed-9d81-4c83-8fb5-b4653894ff90",
                column: "ConcurrencyStamp",
                value: "fc7c067e-5ecf-4b38-a262-7a2abbbd9034");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d02b1923-6c99-4fe1-a1fa-4477ae031242",
                column: "ConcurrencyStamp",
                value: "f02f48e2-6ccf-4ad3-aac7-b8957c1eaea5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6bc92124-2866-4d0b-82c3-e629139c2c03",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f60cc594-1559-4d33-875b-195b3fbf02dd", "AQAAAAEAACcQAAAAEK0ApmJgEmrmFHhzPvDjf3UEKbhhwUZfuoK3mt+xbDJAvO3c1ItTwJiqvc+Ep9Oeag==", "fae417a1-9478-40bc-af11-f804df09feb1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Funcionario");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93c1d0a4-2a15-4b80-b062-be9bac57c979",
                column: "ConcurrencyStamp",
                value: "dcb1eee0-3b1f-4dc8-90b2-44cfd87c9171");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4344bed-9d81-4c83-8fb5-b4653894ff90",
                column: "ConcurrencyStamp",
                value: "f360287d-0e0f-4054-988d-84ffd8a77372");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d02b1923-6c99-4fe1-a1fa-4477ae031242",
                column: "ConcurrencyStamp",
                value: "4d603844-2572-442b-94f4-c78278430492");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6bc92124-2866-4d0b-82c3-e629139c2c03",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e57dad70-146d-4552-a815-cd3341c86b54", "AQAAAAEAACcQAAAAEK4u1R2M1bv5XPIx+kEd8r+gCyEASwvzuFSXwp2MImkfE14gJmUwpBRDca7J3Dmu0w==", "4a68ebc5-4dbe-40c3-beed-ce5b4278d6e6" });
        }
    }
}
