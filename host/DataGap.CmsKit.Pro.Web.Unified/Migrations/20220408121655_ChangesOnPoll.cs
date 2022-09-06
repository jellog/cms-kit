using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataGap.CmsKit.Pro.Migrations
{
    public partial class ChangesOnPoll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CmsPollUserVotes_CmsPolls_PollId",
                table: "CmsPollUserVotes");

            migrationBuilder.DropTable(
                name: "CmsPollAnswerOptions");

            migrationBuilder.DropIndex(
                name: "IX_CmsPollUserVotes_PollId",
                table: "CmsPollUserVotes");

            migrationBuilder.DropColumn(
                name: "ResultShowingTime",
                table: "CmsPolls");

            migrationBuilder.RenameColumn(
                name: "AnswerOptionId",
                table: "CmsPollUserVotes",
                newName: "PollOptionId");

            migrationBuilder.RenameColumn(
                name: "ShowResultWithoutGivingAnswer",
                table: "CmsPolls",
                newName: "ShowResultWithoutGivingVote");

            migrationBuilder.RenameColumn(
                name: "AllowMultipleAnswer",
                table: "CmsPolls",
                newName: "AllowMultipleVote");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "CmsPollUserVotes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ResultShowingEndDate",
                table: "CmsPolls",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "CmsPolls",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "CmsPolls",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CmsPolls",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "CmsPolls",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "CmsPolls",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CmsPollOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PollId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    VoteCount = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsPollOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmsPollOptions_CmsPolls_PollId",
                        column: x => x.PollId,
                        principalTable: "CmsPolls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CmsPollOptions_PollId",
                table: "CmsPollOptions",
                column: "PollId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CmsPollOptions");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "CmsPollUserVotes");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "CmsPolls");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "CmsPolls");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CmsPolls");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "CmsPolls");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "CmsPolls");

            migrationBuilder.RenameColumn(
                name: "PollOptionId",
                table: "CmsPollUserVotes",
                newName: "AnswerOptionId");

            migrationBuilder.RenameColumn(
                name: "ShowResultWithoutGivingVote",
                table: "CmsPolls",
                newName: "ShowResultWithoutGivingAnswer");

            migrationBuilder.RenameColumn(
                name: "AllowMultipleVote",
                table: "CmsPolls",
                newName: "AllowMultipleAnswer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ResultShowingEndDate",
                table: "CmsPolls",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "ResultShowingTime",
                table: "CmsPolls",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "CmsPollAnswerOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    PerAnswerVoteCount = table.Column<int>(type: "int", nullable: false),
                    PollId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsPollAnswerOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmsPollAnswerOptions_CmsPolls_PollId",
                        column: x => x.PollId,
                        principalTable: "CmsPolls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CmsPollUserVotes_PollId",
                table: "CmsPollUserVotes",
                column: "PollId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsPollAnswerOptions_PollId",
                table: "CmsPollAnswerOptions",
                column: "PollId");

            migrationBuilder.AddForeignKey(
                name: "FK_CmsPollUserVotes_CmsPolls_PollId",
                table: "CmsPollUserVotes",
                column: "PollId",
                principalTable: "CmsPolls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
