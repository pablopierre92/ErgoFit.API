using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ergo.Fit.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriasExerciciosSessoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExercicioModel_CategoriaModel_IdCategoria",
                table: "ExercicioModel");

            migrationBuilder.DropForeignKey(
                name: "FK_SessaoModel_ExercicioModel_IdExercicio",
                table: "SessaoModel");

            migrationBuilder.DropForeignKey(
                name: "FK_SessaoModel_Funcionarios_IdFuncionario",
                table: "SessaoModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessaoModel",
                table: "SessaoModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExercicioModel",
                table: "ExercicioModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriaModel",
                table: "CategoriaModel");

            migrationBuilder.RenameTable(
                name: "SessaoModel",
                newName: "Sessoes");

            migrationBuilder.RenameTable(
                name: "ExercicioModel",
                newName: "Exercicios");

            migrationBuilder.RenameTable(
                name: "CategoriaModel",
                newName: "Categorias");

            migrationBuilder.RenameIndex(
                name: "IX_SessaoModel_IdFuncionario",
                table: "Sessoes",
                newName: "IX_Sessoes_IdFuncionario");

            migrationBuilder.RenameIndex(
                name: "IX_SessaoModel_IdExercicio",
                table: "Sessoes",
                newName: "IX_Sessoes_IdExercicio");

            migrationBuilder.RenameIndex(
                name: "IX_ExercicioModel_IdCategoria",
                table: "Exercicios",
                newName: "IX_Exercicios_IdCategoria");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sessoes",
                table: "Sessoes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercicios",
                table: "Exercicios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercicios_Categorias_IdCategoria",
                table: "Exercicios",
                column: "IdCategoria",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Exercicios_IdExercicio",
                table: "Sessoes",
                column: "IdExercicio",
                principalTable: "Exercicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessoes_Funcionarios_IdFuncionario",
                table: "Sessoes",
                column: "IdFuncionario",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercicios_Categorias_IdCategoria",
                table: "Exercicios");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Exercicios_IdExercicio",
                table: "Sessoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessoes_Funcionarios_IdFuncionario",
                table: "Sessoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sessoes",
                table: "Sessoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercicios",
                table: "Exercicios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias");

            migrationBuilder.RenameTable(
                name: "Sessoes",
                newName: "SessaoModel");

            migrationBuilder.RenameTable(
                name: "Exercicios",
                newName: "ExercicioModel");

            migrationBuilder.RenameTable(
                name: "Categorias",
                newName: "CategoriaModel");

            migrationBuilder.RenameIndex(
                name: "IX_Sessoes_IdFuncionario",
                table: "SessaoModel",
                newName: "IX_SessaoModel_IdFuncionario");

            migrationBuilder.RenameIndex(
                name: "IX_Sessoes_IdExercicio",
                table: "SessaoModel",
                newName: "IX_SessaoModel_IdExercicio");

            migrationBuilder.RenameIndex(
                name: "IX_Exercicios_IdCategoria",
                table: "ExercicioModel",
                newName: "IX_ExercicioModel_IdCategoria");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessaoModel",
                table: "SessaoModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExercicioModel",
                table: "ExercicioModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriaModel",
                table: "CategoriaModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExercicioModel_CategoriaModel_IdCategoria",
                table: "ExercicioModel",
                column: "IdCategoria",
                principalTable: "CategoriaModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessaoModel_ExercicioModel_IdExercicio",
                table: "SessaoModel",
                column: "IdExercicio",
                principalTable: "ExercicioModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessaoModel_Funcionarios_IdFuncionario",
                table: "SessaoModel",
                column: "IdFuncionario",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
