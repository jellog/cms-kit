using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataGap.CmsKit.Pro.Migrations
{
    public partial class RemovedAggregateRoot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "CmsPollUserVotes");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "CmsPollUserVotes");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "CmsPollUserVotes");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "CmsPollUserVotes");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "CmsPollAnswerOptions");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "CmsPollAnswerOptions");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "CmsPollAnswerOptions");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "CmsPollAnswerOptions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "CmsPollUserVotes",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "CmsPollUserVotes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "CmsPollUserVotes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "CmsPollUserVotes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "CmsPollAnswerOptions",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "CmsPollAnswerOptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "CmsPollAnswerOptions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "CmsPollAnswerOptions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
