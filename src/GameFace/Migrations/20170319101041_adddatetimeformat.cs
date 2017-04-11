using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameFace.Migrations
{
    public partial class adddatetimeformat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rewards",
                columns: table => new
                {
                    idReward = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rewards", x => x.idReward);
                });

            migrationBuilder.CreateTable(
                name: "StatisticType",
                columns: table => new
                {
                    idStatistic = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticType", x => x.idStatistic);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    desirability = table.Column<int>(nullable: false),
                    efort = table.Column<int>(nullable: false),
                    frequency = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true),
                    nickName = table.Column<string>(nullable: true),
                    status = table.Column<bool>(nullable: false),
                    surName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Achieve",
                columns: table => new
                {
                    idAchieve = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    description = table.Column<string>(nullable: true),
                    idTask = table.Column<int>(nullable: false),
                    levelNeeded = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achieve", x => x.idAchieve);
                    table.ForeignKey(
                        name: "ForeignKey_Achievem_tasks",
                        column: x => x.idTask,
                        principalTable: "Tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatisticDatapoint",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    description = table.Column<string>(nullable: true),
                    idStatistic = table.Column<int>(nullable: false),
                    idTask = table.Column<int>(nullable: false),
                    idUser = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticDatapoint", x => x.id);
                    table.ForeignKey(
                        name: "ForeignKey_statisticDP_task",
                        column: x => x.idStatistic,
                        principalTable: "Tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatisticDatapoint_StatisticType_idStatistic",
                        column: x => x.idStatistic,
                        principalTable: "StatisticType",
                        principalColumn: "idStatistic",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_statisticDP_user",
                        column: x => x.idUser,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersRewards",
                columns: table => new
                {
                    idReward = table.Column<int>(nullable: false),
                    idUser = table.Column<int>(nullable: false),
                    hasClaimed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRewards", x => new { x.idReward, x.idUser });
                    table.ForeignKey(
                        name: "ForeignKey_userReward_Reward",
                        column: x => x.idReward,
                        principalTable: "Rewards",
                        principalColumn: "idReward",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_userReward_User",
                        column: x => x.idUser,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "XP",
                columns: table => new
                {
                    idUser = table.Column<int>(nullable: false),
                    idTask = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XP", x => new { x.idUser, x.idTask });
                    table.ForeignKey(
                        name: "ForeignKey_XPCateg",
                        column: x => x.idTask,
                        principalTable: "Tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_XPUser",
                        column: x => x.idUser,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersAchievements",
                columns: table => new
                {
                    idAchievement = table.Column<int>(nullable: false),
                    idUser = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersAchievements", x => new { x.idAchievement, x.idUser });
                    table.ForeignKey(
                        name: "ForeignKey_userAchiev_achiev",
                        column: x => x.idAchievement,
                        principalTable: "Achieve",
                        principalColumn: "idAchieve",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_userAchievm_user",
                        column: x => x.idUser,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achieve_idTask",
                table: "Achieve",
                column: "idTask");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticDatapoint_idStatistic",
                table: "StatisticDatapoint",
                column: "idStatistic");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticDatapoint_idUser",
                table: "StatisticDatapoint",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_Users_nickName",
                table: "Users",
                column: "nickName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersAchievements_idUser",
                table: "UsersAchievements",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRewards_idUser",
                table: "UsersRewards",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_XP_idTask",
                table: "XP",
                column: "idTask");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatisticDatapoint");

            migrationBuilder.DropTable(
                name: "UsersAchievements");

            migrationBuilder.DropTable(
                name: "UsersRewards");

            migrationBuilder.DropTable(
                name: "XP");

            migrationBuilder.DropTable(
                name: "StatisticType");

            migrationBuilder.DropTable(
                name: "Achieve");

            migrationBuilder.DropTable(
                name: "Rewards");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
