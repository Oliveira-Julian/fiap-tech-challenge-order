using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddPagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PEDIDO_PAGAMENTO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    ID_PEDIDO = table.Column<Guid>(type: "uuid", nullable: true),
                    STATUS = table.Column<int>(type: "integer", nullable: false),
                    QrCode = table.Column<string>(type: "text", nullable: true),
                    DATA_CRIACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEDIDO_PAGAMENTO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PEDIDO_PAGAMENTO_PEDIDO_ID_PEDIDO",
                        column: x => x.ID_PEDIDO,
                        principalTable: "PEDIDO",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PEDIDO_PAGAMENTO_ID_PEDIDO",
                table: "PEDIDO_PAGAMENTO",
                column: "ID_PEDIDO",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PEDIDO_PAGAMENTO");
        }
    }
}
