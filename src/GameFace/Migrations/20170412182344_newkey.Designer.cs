using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GameFace.Controllers;

namespace GameFace.Migrations
{
    [DbContext(typeof(GameFaceContext))]
    [Migration("20170412182344_newkey")]
    partial class newkey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("GameFace.Models.Achieve", b =>
                {
                    b.Property<int>("idAchieve")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("description");

                    b.Property<int>("idTask");

                    b.Property<int>("levelNeeded");

                    b.HasKey("idAchieve");

                    b.HasIndex("idTask");

                    b.ToTable("Achieve");
                });

            modelBuilder.Entity("GameFace.Models.Rewards", b =>
                {
                    b.Property<int>("idReward")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("description");

                    b.HasKey("idReward");

                    b.ToTable("Rewards");
                });

            modelBuilder.Entity("GameFace.Models.StatisticDatapoint", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("description");

                    b.Property<int>("idStatistic");

                    b.Property<int>("idTask");

                    b.Property<int>("idUser");

                    b.HasKey("id");

                    b.HasIndex("idStatistic");

                    b.HasIndex("idUser");

                    b.ToTable("StatisticDatapoint");
                });

            modelBuilder.Entity("GameFace.Models.StatisticType", b =>
                {
                    b.Property<int>("idStatistic")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("description");

                    b.HasKey("idStatistic");

                    b.ToTable("StatisticType");
                });

            modelBuilder.Entity("GameFace.Models.Tasks", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("desirability");

                    b.Property<int>("efort");

                    b.Property<int>("frequency");

                    b.Property<string>("name");

                    b.HasKey("id");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("GameFace.Models.Users", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("name");

                    b.Property<string>("nickName");

                    b.Property<bool>("status");

                    b.Property<string>("surName");

                    b.HasKey("id");

                    b.HasIndex("nickName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GameFace.Models.UsersAchievements", b =>
                {
                    b.Property<int>("idAchievement");

                    b.Property<int>("idUser");

                    b.Property<DateTime>("date");

                    b.HasKey("idAchievement", "idUser");

                    b.HasIndex("idUser");

                    b.ToTable("UsersAchievements");
                });

            modelBuilder.Entity("GameFace.Models.UsersRewards", b =>
                {
                    b.Property<int>("idReward");

                    b.Property<int>("idUser");

                    b.Property<bool>("hasClaimed");

                    b.HasKey("idReward", "idUser");

                    b.HasIndex("idUser");

                    b.ToTable("UsersRewards");
                });

            modelBuilder.Entity("GameFace.Models.XP", b =>
                {
                    b.Property<int>("idUser");

                    b.Property<int>("idTask");

                    b.Property<DateTime>("date");

                    b.HasKey("idUser", "idTask", "date");

                    b.HasIndex("idTask");

                    b.ToTable("XP");
                });

            modelBuilder.Entity("GameFace.Models.Achieve", b =>
                {
                    b.HasOne("GameFace.Models.Tasks", "tasks")
                        .WithMany("achievement")
                        .HasForeignKey("idTask")
                        .HasConstraintName("ForeignKey_Achievem_tasks")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameFace.Models.StatisticDatapoint", b =>
                {
                    b.HasOne("GameFace.Models.Tasks", "tasks")
                        .WithMany("statisticDatapoint")
                        .HasForeignKey("idStatistic")
                        .HasConstraintName("ForeignKey_statisticDP_task")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameFace.Models.StatisticType", "statistics")
                        .WithMany("statisticDatapoint")
                        .HasForeignKey("idStatistic")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameFace.Models.Users", "users")
                        .WithMany("statisticDatapoint")
                        .HasForeignKey("idUser")
                        .HasConstraintName("ForeignKey_statisticDP_user")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameFace.Models.UsersAchievements", b =>
                {
                    b.HasOne("GameFace.Models.Achieve", "achievement")
                        .WithMany("usersAchievement")
                        .HasForeignKey("idAchievement")
                        .HasConstraintName("ForeignKey_userAchiev_achiev")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameFace.Models.Users", "users")
                        .WithMany("userAchievements")
                        .HasForeignKey("idUser")
                        .HasConstraintName("ForeignKey_userAchievm_user")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameFace.Models.UsersRewards", b =>
                {
                    b.HasOne("GameFace.Models.Rewards", "reward")
                        .WithMany("userRewards")
                        .HasForeignKey("idReward")
                        .HasConstraintName("ForeignKey_userReward_Reward")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameFace.Models.Users", "users")
                        .WithMany("userRewards")
                        .HasForeignKey("idUser")
                        .HasConstraintName("ForeignKey_userReward_User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameFace.Models.XP", b =>
                {
                    b.HasOne("GameFace.Models.Tasks", "tasks")
                        .WithMany("xP")
                        .HasForeignKey("idTask")
                        .HasConstraintName("ForeignKey_XPCateg")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameFace.Models.Users", "User")
                        .WithMany("xP")
                        .HasForeignKey("idUser")
                        .HasConstraintName("ForeignKey_XPUser")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
