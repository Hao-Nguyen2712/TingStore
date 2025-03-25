using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Provider.Migrations
{
    /// <inheritdoc />
    public partial class AUpdateIdentity : Migration
    {  
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "BlackListToken",
                newName: "BlacklistedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryTime",
                table: "BlackListToken",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiryTime",
                table: "BlackListToken");

            migrationBuilder.RenameColumn(
                name: "BlacklistedAt",
                table: "BlackListToken",
                newName: "ExpiryDate");
        }
    }
}
