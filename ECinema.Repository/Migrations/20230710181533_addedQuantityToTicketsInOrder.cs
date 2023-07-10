﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ECinema.Repository.Migrations
{
    public partial class addedQuantityToTicketsInOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "TicketsInOrder",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "TicketsInOrder");
        }
    }
}
