using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Archangel.Tests.WebEditor.Data.Migrations
{
    public partial class Added_update_on_field_to_news : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateOn",
                table: "News",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateOn",
                table: "News");
        }
    }
}
