using ActiveMQ_PoC.ConsumerA.Data.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActiveMQ_PoC.ConsumerA.Migrations
{
    public partial class ChangedOriginalEventType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TransportOrderAmendedEvent>(
                name: "OriginalEvent",
                table: "TransportOrderDocs",
                type: "json",
                nullable: false,
                oldClrType: typeof(TransportOrderAmendedEvent),
                oldType: "jsonb");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TransportOrderAmendedEvent>(
                name: "OriginalEvent",
                table: "TransportOrderDocs",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(TransportOrderAmendedEvent),
                oldType: "json");
        }
    }
}
