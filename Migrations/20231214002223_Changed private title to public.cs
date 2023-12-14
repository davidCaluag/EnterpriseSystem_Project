﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_EnterpriseSystem.Migrations
{
    /// <inheritdoc />
    public partial class Changedprivatetitletopublic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Users_userId",
                table: "Playlists");

            migrationBuilder.AlterColumn<Guid>(
                name: "userId",
                table: "Playlists",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Users_userId",
                table: "Playlists",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Users_userId",
                table: "Playlists");

            migrationBuilder.AlterColumn<Guid>(
                name: "userId",
                table: "Playlists",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Users_userId",
                table: "Playlists",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
