using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiresAction",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RequiresAction",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "RequiresAction",
                table: "NotificationTypes");

            migrationBuilder.DropColumn(
                name: "RequiresAction",
                table: "NotificationSettings");

            migrationBuilder.DropColumn(
                name: "RequiresAction",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "RequiresAction",
                table: "NotificationCategories");

            migrationBuilder.AddColumn<short>(
                name: "Status",
                table: "Users",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Status",
                table: "Services",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Status",
                table: "NotificationTypes",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Status",
                table: "NotificationSettings",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Status",
                table: "NotificationCategories",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "NotificationTypes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "NotificationSettings");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "NotificationCategories");

            migrationBuilder.AddColumn<bool>(
                name: "RequiresAction",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresAction",
                table: "Services",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresAction",
                table: "NotificationTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresAction",
                table: "NotificationSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresAction",
                table: "Notifications",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresAction",
                table: "NotificationCategories",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
