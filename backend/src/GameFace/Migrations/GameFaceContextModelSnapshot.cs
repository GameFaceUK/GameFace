using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GameFace.Controllers;

namespace GameFace.Migrations
{
    [DbContext(typeof(GameFaceContext))]
    partial class GameFaceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("GameFace.Modules.CompletingTasks", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("date");

                    b.Property<int>("idTask");

                    b.Property<int>("idUser");

                    b.HasKey("id");

                    b.HasIndex("idTask");

                    b.HasIndex("idUser");

                    b.ToTable("CompletingTasks");
                });

            modelBuilder.Entity("GameFace.Modules.Tasks", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("description");

                    b.Property<string>("name");

                    b.Property<int>("value");

                    b.HasKey("id");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("GameFace.Modules.Users", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("active");

                    b.Property<string>("name");

                    b.Property<string>("nickName");

                    b.Property<string>("surName");

                    b.HasKey("id");

                    b.HasIndex("nickName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GameFace.Modules.CompletingTasks", b =>
                {
                    b.HasOne("GameFace.Modules.Tasks", "Tasks")
                        .WithMany("taskComplition")
                        .HasForeignKey("idTask")
                        .HasConstraintName("ForeignKey_CompleteT_Tasks")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameFace.Modules.Users", "Users")
                        .WithMany("taskComplition")
                        .HasForeignKey("idUser")
                        .HasConstraintName("ForeignKey_CompleteT_User")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
