using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataGap.CmsKit.Pro.Migrations
{
    public partial class AddedPoll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CmsPollingAnswerOptions");

            migrationBuilder.DropTable(
                name: "CmsPollingUserVotes");

            migrationBuilder.DropTable(
                name: "CmsPollings");

            migrationBuilder.CreateTable(
                name: "CmsPolls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowMultipleAnswer = table.Column<bool>(type: "bit", nullable: false),
                    VoteCount = table.Column<int>(type: "int", nullable: false),
                    ShowVoteCount = table.Column<bool>(type: "bit", nullable: false),
                    ShowResultWithoutGivingAnswer = table.Column<bool>(type: "bit", nullable: false),
                    ShowHoursLeft = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResultShowingEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResultShowingTime = table.Column<byte>(type: "tinyint", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsPolls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CmsPollAnswerOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PollId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    PerAnswerVoteCount = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "CmsPollUserVotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PollId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnswerOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsPollUserVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmsPollUserVotes_CmsPolls_PollId",
                        column: x => x.PollId,
                        principalTable: "CmsPolls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CmsPollAnswerOptions_PollId",
                table: "CmsPollAnswerOptions",
                column: "PollId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsPollUserVotes_PollId",
                table: "CmsPollUserVotes",
                column: "PollId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CmsPollAnswerOptions");

            migrationBuilder.DropTable(
                name: "CmsPollUserVotes");

            migrationBuilder.DropTable(
                name: "CmsPolls");

            migrationBuilder.CreateTable(
                name: "CmsPollings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllowMultipleAnswer = table.Column<bool>(type: "bit", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultShowingEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResultShowingTime = table.Column<byte>(type: "tinyint", nullable: false),
                    ShowHoursLeft = table.Column<bool>(type: "bit", nullable: false),
                    ShowResultWithoutGivingAnswer = table.Column<bool>(type: "bit", nullable: false),
                    ShowVoteCount = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VoteCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsPollings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CmsPollingAnswerOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerAnswerVoteCount = table.Column<int>(type: "int", nullable: false),
                    PollingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsPollingAnswerOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmsPollingAnswerOptions_CmsPollings_PollingId",
                        column: x => x.PollingId,
                        principalTable: "CmsPollings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CmsPollingUserVotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnswerOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PollingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsPollingUserVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CmsPollingUserVotes_CmsPollings_PollingId",
                        column: x => x.PollingId,
                        principalTable: "CmsPollings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CmsPollingAnswerOptions_PollingId",
                table: "CmsPollingAnswerOptions",
                column: "PollingId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsPollingUserVotes_PollingId",
                table: "CmsPollingUserVotes",
                column: "PollingId");
        }
    }
}
