using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Api.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Tenants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Properties",
                type: "NVARCHAR(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Owners",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Owners");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Properties",
                type: "NVARCHAR",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(255)",
                oldMaxLength: 255);
        }
    }
}
