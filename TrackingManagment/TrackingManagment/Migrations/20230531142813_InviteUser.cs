using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingManagment.Migrations
{
    /// <inheritdoc />
    public partial class InviteUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invitedUsers_AspNetUsers_ApplicationUserId",
                table: "invitedUsers");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "invitedUsers",
                newName: "InvitationSenderUserId");

            migrationBuilder.RenameIndex(
                name: "IX_invitedUsers_ApplicationUserId",
                table: "invitedUsers",
                newName: "IX_invitedUsers_InvitationSenderUserId");

            migrationBuilder.AddColumn<int>(
                name: "Action",
                table: "invitedUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "InvitationReceiverUserId",
                table: "invitedUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "invitedUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_invitedUsers_InvitationReceiverUserId",
                table: "invitedUsers",
                column: "InvitationReceiverUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_invitedUsers_AspNetUsers_InvitationReceiverUserId",
                table: "invitedUsers",
                column: "InvitationReceiverUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_invitedUsers_AspNetUsers_InvitationSenderUserId",
                table: "invitedUsers",
                column: "InvitationSenderUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invitedUsers_AspNetUsers_InvitationReceiverUserId",
                table: "invitedUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_invitedUsers_AspNetUsers_InvitationSenderUserId",
                table: "invitedUsers");

            migrationBuilder.DropIndex(
                name: "IX_invitedUsers_InvitationReceiverUserId",
                table: "invitedUsers");

            migrationBuilder.DropColumn(
                name: "Action",
                table: "invitedUsers");

            migrationBuilder.DropColumn(
                name: "InvitationReceiverUserId",
                table: "invitedUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "invitedUsers");

            migrationBuilder.RenameColumn(
                name: "InvitationSenderUserId",
                table: "invitedUsers",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_invitedUsers_InvitationSenderUserId",
                table: "invitedUsers",
                newName: "IX_invitedUsers_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_invitedUsers_AspNetUsers_ApplicationUserId",
                table: "invitedUsers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
