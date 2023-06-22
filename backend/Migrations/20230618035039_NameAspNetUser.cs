using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MC426_Backend.Migrations
{
    /// <inheritdoc />
    public partial class NameAspNetUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

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
                columns: new[] { "ConcurrencyStamp", "Name", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e57dad70-146d-4552-a815-cd3341c86b54", null, "AQAAAAEAACcQAAAAEK4u1R2M1bv5XPIx+kEd8r+gCyEASwvzuFSXwp2MImkfE14gJmUwpBRDca7J3Dmu0w==", "4a68ebc5-4dbe-40c3-beed-ce5b4278d6e6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93c1d0a4-2a15-4b80-b062-be9bac57c979",
                column: "ConcurrencyStamp",
                value: "f0bed074-14da-415e-9941-983b40ad5015");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4344bed-9d81-4c83-8fb5-b4653894ff90",
                column: "ConcurrencyStamp",
                value: "dd40aed1-a158-4e56-8084-cf4705a37243");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d02b1923-6c99-4fe1-a1fa-4477ae031242",
                column: "ConcurrencyStamp",
                value: "446abc10-5e7f-4b81-bce4-6ad3d52dbcf6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6bc92124-2866-4d0b-82c3-e629139c2c03",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a28683b6-fda5-4987-8791-9df3abbbba23", "AQAAAAEAACcQAAAAENzo92p6ibg3a902VgrcIiANmfJ9yTYvjhP+x/JdV/6EgqxfRVKEA69xmmf/tAEsZQ==", "b6d0749d-a522-4de7-96f9-cc4b39411e3b" });
        }
    }
}
