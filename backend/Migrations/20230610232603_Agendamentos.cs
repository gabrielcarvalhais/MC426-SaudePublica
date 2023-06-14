using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MC426_Backend.Migrations
{
    /// <inheritdoc />
    public partial class Agendamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agendamento",
                columns: table => new
                {
                    AgendamentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chave = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusAgendamento = table.Column<int>(type: "int", nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    MedicoId = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraFinal = table.Column<TimeSpan>(type: "time", nullable: false),
                    Frequencia = table.Column<int>(type: "int", nullable: false),
                    DataFinal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValorHora = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Excluido = table.Column<bool>(type: "bit", nullable: false),
                    VinculoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamento", x => x.AgendamentoId);
                    table.ForeignKey(
                        name: "FK_Agendamento_Agendamento_VinculoId",
                        column: x => x.VinculoId,
                        principalTable: "Agendamento",
                        principalColumn: "AgendamentoId");
                    table.ForeignKey(
                        name: "FK_Agendamento_Funcionario_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Funcionario",
                        principalColumn: "FuncionarioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agendamento_Paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Paciente",
                        principalColumn: "PacienteId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_MedicoId",
                table: "Agendamento",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_PacienteId",
                table: "Agendamento",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_VinculoId",
                table: "Agendamento",
                column: "VinculoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agendamento");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93c1d0a4-2a15-4b80-b062-be9bac57c979",
                column: "ConcurrencyStamp",
                value: "ea74a2f2-1028-47d6-8365-4f09b38c413c");

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
        }
    }
}
