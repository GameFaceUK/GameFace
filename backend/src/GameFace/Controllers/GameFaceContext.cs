using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GameFace.Modules;


namespace GameFace.Controllers
{
    public class GameFaceContext : DbContext
    {


        public DbSet<Users> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<CompletingTasks> CompletingTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./GameFace.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasIndex(b => b.nickName).IsUnique();
            modelBuilder.Entity<Tasks>().ToTable("Tasks");

            modelBuilder.Entity<CompletingTasks>()
            .HasOne(s => s.Tasks)
            .WithMany(c => c.taskComplition)
            .HasForeignKey(s => s.idTask)
            .HasConstraintName("ForeignKey_CompleteT_Tasks");

            modelBuilder.Entity<CompletingTasks>()
            .HasOne(u => u.Users)
            .WithMany(c => c.taskComplition)
            .HasForeignKey(u => u.idUser)
            .HasConstraintName("ForeignKey_CompleteT_User");
        }
    }


}

