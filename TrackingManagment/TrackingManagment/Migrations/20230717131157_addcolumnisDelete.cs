using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackingManagment.Migrations
{
    /// <inheritdoc />
    public partial class addcolumnisDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "realStates",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "realStates");
        }
    }
}
