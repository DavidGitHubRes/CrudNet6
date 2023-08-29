using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudNet6.Migrations
{
    public partial class migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaClienteId",
                table: "clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoriasCliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Detalle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasCliente", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_clientes_CategoriaClienteId",
                table: "clientes",
                column: "CategoriaClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_clientes_CategoriasCliente_CategoriaClienteId",
                table: "clientes",
                column: "CategoriaClienteId",
                principalTable: "CategoriasCliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clientes_CategoriasCliente_CategoriaClienteId",
                table: "clientes");

            migrationBuilder.DropTable(
                name: "CategoriasCliente");

            migrationBuilder.DropIndex(
                name: "IX_clientes_CategoriaClienteId",
                table: "clientes");

            migrationBuilder.DropColumn(
                name: "CategoriaClienteId",
                table: "clientes");
        }
    }
}
