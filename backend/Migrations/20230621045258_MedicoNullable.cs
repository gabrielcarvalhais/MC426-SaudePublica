using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MC426_Backend.Migrations
{
    /// <inheritdoc />
    public partial class MedicoNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Funcionario_MedicoId",
                table: "Agendamento");

            migrationBuilder.AlterColumn<int>(
                name: "MedicoId",
                table: "Agendamento",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93c1d0a4-2a15-4b80-b062-be9bac57c979",
                column: "ConcurrencyStamp",
                value: "75cadff4-b659-45b3-8b75-558cb9f34edd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4344bed-9d81-4c83-8fb5-b4653894ff90",
                column: "ConcurrencyStamp",
                value: "0d76eb47-d26d-4585-a3a7-89028b1fac76");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d02b1923-6c99-4fe1-a1fa-4477ae031242",
                column: "ConcurrencyStamp",
                value: "6edbbf6a-930a-47e3-b431-9cdd89e9c93a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6bc92124-2866-4d0b-82c3-e629139c2c03",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4fd2f846-9abd-4ff2-9f09-5ff6aa95bca8", "AQAAAAEAACcQAAAAEAcwXFKiYSbTklflALvodIdj6N8eFb7a+XPv52SUBY7M7Qlo54vwhfo3LF8qokHpxg==", "4e0d3029-dedd-457a-b0f0-2dc6cc2353db" });

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Funcionario_MedicoId",
                table: "Agendamento",
                column: "MedicoId",
                principalTable: "Funcionario",
                principalColumn: "FuncionarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Funcionario_MedicoId",
                table: "Agendamento");

            migrationBuilder.AlterColumn<int>(
                name: "MedicoId",
                table: "Agendamento",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Funcionario_MedicoId",
                table: "Agendamento",
                column: "MedicoId",
                principalTable: "Funcionario",
                principalColumn: "FuncionarioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
