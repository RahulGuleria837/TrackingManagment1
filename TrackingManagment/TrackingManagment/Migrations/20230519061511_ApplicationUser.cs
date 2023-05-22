using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingManagment.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "realStates",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_realStates_ApplicationUserId",
                table: "realStates",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_realStates_AspNetUsers_ApplicationUserId",
                table: "realStates",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_realStates_AspNetUsers_ApplicationUserId",
                table: "realStates");

            migrationBuilder.DropIndex(
                name: "IX_realStates_ApplicationUserId",
                table: "realStates");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "realStates");
        }
    }
}
