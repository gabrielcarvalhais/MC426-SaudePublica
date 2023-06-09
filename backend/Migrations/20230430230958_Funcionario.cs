using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MC426_Backend.Migrations
{
    /// <inheritdoc />
    public partial class Funcionario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    FuncionarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chave = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(70)", maxLength: 70, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cpf = table.Column<string>(type: "VARCHAR(11)", maxLength: 11, nullable: true),
                    Telefone = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Excluido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.FuncionarioId);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93c1d0a4-2a15-4b80-b062-be9bac57c979",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ea74a2f2-1028-47d6-8365-4f09b38c413c", "Funcionário", "Funcionário" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4344bed-9d81-4c83-8fb5-b4653894ff90",
                column: "ConcurrencyStamp",
                value: "9749cb42-ea70-4f10-a0ed-d4a7d95b65d1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d02b1923-6c99-4fe1-a1fa-4477ae031242",
                column: "ConcurrencyStamp",
                value: "2bd6c704-9e06-4c5c-924c-7ba09b8f3077");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6bc92124-2866-4d0b-82c3-e629139c2c03",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9bf50309-97cb-48cd-b774-268701138106", "AQAAAAEAACcQAAAAEN9whBjbKTn/qF7G6OgvtfVK95Bt4Uoh1I27TfRzdC46IsjpgHSZyP1LdBWY87Ow1Q==", "892beebc-aa3f-482c-9b6a-4a743ab80e72" });

            migrationBuilder.CreateIndex(
                name: "IX_Funcionario_Chave",
                table: "Funcionario",
                column: "Chave");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Funcionario");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93c1d0a4-2a15-4b80-b062-be9bac57c979",
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cbbadd47-d648-43ca-8631-26fd78565e5c", "Profissional", "Profissional" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4344bed-9d81-4c83-8fb5-b4653894ff90",
                column: "ConcurrencyStamp",
                value: "ba0df159-a5a7-4d6b-8b9c-8cb2fea80612");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d02b1923-6c99-4fe1-a1fa-4477ae031242",
                column: "ConcurrencyStamp",
                value: "c2ca1124-fbd2-449c-80fa-4ca55000050d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6bc92124-2866-4d0b-82c3-e629139c2c03",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b2292245-fe12-4bf4-97db-d3fa0e03cb9f", "AQAAAAEAACcQAAAAEKnBJYjt8Q/9LIBq6sqHbNgus9oOhXHdr0WMlR6jfW/fo+lEIIoEpgC3KjDjb0yIZw==", "3c343113-fb4a-4fb3-843e-40a8853c0cf5" });
        }
    }
}
