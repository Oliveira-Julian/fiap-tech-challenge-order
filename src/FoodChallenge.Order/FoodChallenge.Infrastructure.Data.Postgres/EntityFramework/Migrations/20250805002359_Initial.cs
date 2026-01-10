using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodChallenge.Infrastructure.Data.Postgres.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CLIENTE",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    CPF = table.Column<string>(type: "text", nullable: true),
                    NOME = table.Column<string>(type: "text", nullable: true),
                    EMAIL = table.Column<string>(type: "text", nullable: true),
                    DATA_CRIACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ATIVO = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DATA_EXCLUSAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PRODUTO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    CATEGORIA = table.Column<int>(type: "integer", nullable: false),
                    NOME = table.Column<string>(type: "text", nullable: true),
                    DESCRICAO = table.Column<string>(type: "text", nullable: true),
                    PRECO = table.Column<decimal>(type: "numeric", nullable: false),
                    DATA_CRIACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ATIVO = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DATA_EXCLUSAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PEDIDO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    ID_CLIENTE = table.Column<Guid>(type: "uuid", nullable: false),
                    ID_PAGAMENTO = table.Column<Guid>(type: "uuid", nullable: true),
                    CODIGO = table.Column<string>(type: "text", nullable: false),
                    VALOR_TOTAL = table.Column<decimal>(type: "numeric", nullable: false),
                    STATUS = table.Column<int>(type: "integer", nullable: false),
                    DATA_CRIACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ATIVO = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DATA_EXCLUSAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEDIDO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PEDIDO_CLIENTE_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "CLIENTE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PRODUTO_IMAGEM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    ID_PRODUTO = table.Column<Guid>(type: "uuid", nullable: true),
                    NOME = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    TIPO = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TAMANHO = table.Column<decimal>(type: "numeric", nullable: false),
                    CONTEUDO = table.Column<byte[]>(type: "bytea", nullable: false),
                    DATA_CRIACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ATIVO = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DATA_EXCLUSAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO_IMAGEM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUTO_IMAGEM_PRODUTO",
                        column: x => x.ID_PRODUTO,
                        principalTable: "PRODUTO",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ORDEM_PEDIDO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    ID_PEDIDO = table.Column<Guid>(type: "uuid", nullable: false),
                    STATUS = table.Column<int>(type: "integer", nullable: false),
                    DATA_INICIO_PREPARACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DATA_FIM_PREPARACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DATA_CRIACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDEM_PEDIDO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ORDEM_PEDIDO_PEDIDO_ID_PEDIDO",
                        column: x => x.ID_PEDIDO,
                        principalTable: "PEDIDO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PEDIDO_ITEM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    ID_PRODUTO = table.Column<Guid>(type: "uuid", nullable: false),
                    ID_PEDIDO = table.Column<Guid>(type: "uuid", nullable: false),
                    CODIGO = table.Column<string>(type: "text", nullable: false),
                    VALOR = table.Column<decimal>(type: "numeric", nullable: false),
                    QUANTIDADE = table.Column<int>(type: "integer", nullable: false),
                    DATA_CRIACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DATA_ATUALIZACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ATIVO = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DATA_EXCLUSAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PEDIDO_ITEM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PEDIDO_ITEM_PEDIDO_ID_PEDIDO",
                        column: x => x.ID_PEDIDO,
                        principalTable: "PEDIDO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PEDIDO_ITEM_PRODUTO_ID_PRODUTO",
                        column: x => x.ID_PRODUTO,
                        principalTable: "PRODUTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PEDIDO_PAGAMENTO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    ID_PEDIDO = table.Column<Guid>(type: "uuid", nullable: true),
                    CHAVE_MERCADO_PAGO_ORDEM = table.Column<Guid>(type: "uuid", nullable: true),
                    ID_MERCADO_PAGO_ORDEM = table.Column<string>(type: "text", nullable: true),
                    ID_MERCADO_PAGO_PAGAMENTO = table.Column<string>(type: "text", nullable: true),
                    STATUS = table.Column<int>(type: "integer", nullable: false),
                    VALOR = table.Column<decimal>(type: "numeric", nullable: false),
                    METODO = table.Column<int>(type: "integer", nullable: false),
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
                name: "IX_ORDEM_PEDIDO_ID_PEDIDO",
                table: "ORDEM_PEDIDO",
                column: "ID_PEDIDO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PEDIDO_ID_CLIENTE",
                table: "PEDIDO",
                column: "ID_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_PEDIDO_ITEM_ID_PEDIDO",
                table: "PEDIDO_ITEM",
                column: "ID_PEDIDO");

            migrationBuilder.CreateIndex(
                name: "IX_PEDIDO_ITEM_ID_PRODUTO",
                table: "PEDIDO_ITEM",
                column: "ID_PRODUTO");

            migrationBuilder.CreateIndex(
                name: "IX_PEDIDO_PAGAMENTO_ID_PEDIDO",
                table: "PEDIDO_PAGAMENTO",
                column: "ID_PEDIDO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_IMAGEM_ID_PRODUTO",
                table: "PRODUTO_IMAGEM",
                column: "ID_PRODUTO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ORDEM_PEDIDO");

            migrationBuilder.DropTable(
                name: "PEDIDO_ITEM");

            migrationBuilder.DropTable(
                name: "PEDIDO_PAGAMENTO");

            migrationBuilder.DropTable(
                name: "PRODUTO_IMAGEM");

            migrationBuilder.DropTable(
                name: "PEDIDO");

            migrationBuilder.DropTable(
                name: "PRODUTO");

            migrationBuilder.DropTable(
                name: "CLIENTE");
        }
    }
}
