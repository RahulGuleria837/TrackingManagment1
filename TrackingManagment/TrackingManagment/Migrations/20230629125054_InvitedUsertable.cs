using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingManagment.Migrations
{
    /// <inheritdoc />
    public partial class InvitedUsertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvitationReciverName",
                table: "invitedUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InvitationSenderName",
                table: "invitedUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvitationReciverName",
                table: "invitedUsers");

            migrationBuilder.DropColumn(
                name: "InvitationSenderName",
                table: "invitedUsers");
        }
    }
}
