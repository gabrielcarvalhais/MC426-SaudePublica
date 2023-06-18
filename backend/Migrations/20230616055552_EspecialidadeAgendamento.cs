using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MC426_Backend.Migrations
{
    /// <inheritdoc />
    public partial class EspecialidadeAgendamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Especialidade",
                table: "Agendamento",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Especialidade",
                table: "Agendamento");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93c1d0a4-2a15-4b80-b062-be9bac57c979",
                column: "ConcurrencyStamp",
                value: "1ccd1bda-4bfd-41ff-8b02-54a04e3ea749");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4344bed-9d81-4c83-8fb5-b4653894ff90",
                column: "ConcurrencyStamp",
                value: "a4911b08-c226-4de2-ac62-e609d41ea376");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d02b1923-6c99-4fe1-a1fa-4477ae031242",
                column: "ConcurrencyStamp",
                value: "aa583a65-eebc-47a1-a2cb-03fd17a007c1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6bc92124-2866-4d0b-82c3-e629139c2c03",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "690b4c6f-47d7-46f5-a154-02153d0efcf9", "AQAAAAEAACcQAAAAEPwDrd97hOIPX6eEuI1Nc486b4tYwDkK0eB8sGi9tJgsuWhFleAi5gcxd+lFBMQ1bA==", "5f40687f-5576-42d0-a165-a1d48821fd74" });
        }
    }
}
