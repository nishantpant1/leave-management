using Microsoft.EntityFrameworkCore.Migrations;

namespace leave_management.Data.Migrations
{
    public partial class NameCorrectionDefaultDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DefualtDays",
                table: "LeaveTypes",
                newName: "DefaultDays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DefaultDays",
                table: "LeaveTypes",
                newName: "DefualtDays");
        }
    }
}
