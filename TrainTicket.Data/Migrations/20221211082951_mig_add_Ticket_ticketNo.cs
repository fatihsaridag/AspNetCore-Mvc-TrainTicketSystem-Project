using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainTicket.Data.Migrations
{
    public partial class mig_add_Ticket_ticketNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TicketNo",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketNo",
                table: "Tickets");
        }
    }
}
