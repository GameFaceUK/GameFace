using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameFace.Migrations
{
    public partial class newkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_XP",
                table: "XP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_XP",
                table: "XP",
                columns: new[] { "idUser", "idTask", "date" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_XP",
                table: "XP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_XP",
                table: "XP",
                columns: new[] { "idUser", "idTask" });
        }
    }
}
