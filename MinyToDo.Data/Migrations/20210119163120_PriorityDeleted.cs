using Microsoft.EntityFrameworkCore.Migrations;

namespace MinyToDo.Data.Migrations
{
    public partial class PriorityDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "UserTasks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Priority",
                table: "UserTasks",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
