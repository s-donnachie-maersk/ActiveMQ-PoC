using ActiveMQ_PoC.ConsumerA.Data.Entities;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ActiveMQ_PoC.ConsumerA.Migrations
{
    public partial class InitialCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransportOrderDocs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransportOrder = table.Column<TransportOrder>(type: "jsonb", nullable: false),
                    OriginalEvent = table.Column<TransportOrderAmendedEvent>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportOrderDocs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportOrderDocs_TransportOrder",
                table: "TransportOrderDocs",
                column: "TransportOrder")
                .Annotation("Npgsql:IndexMethod", "gin");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransportOrderDocs");
        }
    }
}
