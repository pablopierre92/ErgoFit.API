using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ergo.Fit.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSenhasAddApplicationUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Senha",
                table: "UsuariosMaster");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Empresas");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "UsuariosMaster",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Funcionarios",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Empresas",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosMaster_ApplicationUserId",
                table: "UsuariosMaster",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_ApplicationUserId",
                table: "Funcionarios",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_ApplicationUserId",
                table: "Empresas",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_AspNetUsers_ApplicationUserId",
                table: "Empresas",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_AspNetUsers_ApplicationUserId",
                table: "Funcionarios",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosMaster_AspNetUsers_ApplicationUserId",
                table: "UsuariosMaster",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_AspNetUsers_ApplicationUserId",
                table: "Empresas");

            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_AspNetUsers_ApplicationUserId",
                table: "Funcionarios");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosMaster_AspNetUsers_ApplicationUserId",
                table: "UsuariosMaster");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosMaster_ApplicationUserId",
                table: "UsuariosMaster");

            migrationBuilder.DropIndex(
                name: "IX_Funcionarios_ApplicationUserId",
                table: "Funcionarios");

            migrationBuilder.DropIndex(
                name: "IX_Empresas_ApplicationUserId",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UsuariosMaster");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Empresas");

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "UsuariosMaster",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Funcionarios",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Empresas",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
