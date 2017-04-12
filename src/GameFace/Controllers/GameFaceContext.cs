using Microsoft.EntityFrameworkCore;
using GameFace.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace GameFace.Controllers
{
    public class GameFaceContext : DbContext
    {


        public DbSet<Users> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Rewards> Rewards { get; set; }
        public DbSet<UsersRewards> UsersRewards { get; set; }
        public DbSet<Achieve> Achieve { get; set; }
        public DbSet<UsersAchievements> UsersAchievements { get; set; }
        public DbSet<StatisticDatapoint> StatisticDatapoint { get; set; }
        public DbSet<StatisticType> StatisticType { get; set; }
        public DbSet<XP> XP { get; set; }

  
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./GameFace.db")
                .ConfigureWarnings(warnings => warnings.Throw(CoreEventId.IncludeIgnoredWarning));      
       
        }
    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rewards>().HasKey(c => c.idReward);


            modelBuilder.Entity<UsersRewards>()
            .HasKey(c => new { c.idReward, c.idUser });
            modelBuilder.Entity<UsersRewards>()
            .HasOne(s => s.reward)
            .WithMany(c => c.userRewards)
            .HasForeignKey(s => s.idReward)
            .HasConstraintName("ForeignKey_userReward_Reward");
            modelBuilder.Entity<UsersRewards>()
            .HasOne(s => s.users)
            .WithMany(c => c.userRewards)
            .HasForeignKey(s => s.idUser)
            .HasConstraintName("ForeignKey_userReward_User");

            modelBuilder.Entity<Achieve>().HasKey(c => c.idAchieve);
            modelBuilder.Entity<Achieve>()
                .HasOne(s => s.tasks)
            .WithMany(c => c.achievement)
            .HasForeignKey(s => s.idTask)
            .HasConstraintName("ForeignKey_Achievem_tasks");


            modelBuilder.Entity<UsersAchievements>()
            .HasOne(s => s.achievement)
            .WithMany(c => c.usersAchievement)
            .HasForeignKey(s => s.idAchievement)
            .HasConstraintName("ForeignKey_UserAchievem_achiev");


            modelBuilder.Entity<UsersAchievements>()
           .HasKey(c => new { c.idAchievement, c.idUser });
            modelBuilder.Entity<UsersAchievements>()
            .HasOne(s => s.users)
            .WithMany(c => c.userAchievements)
            .HasForeignKey(s => s.idUser)
            .HasConstraintName("ForeignKey_userAchievm_user");
            modelBuilder.Entity<UsersAchievements>()
            .HasOne(s => s.achievement)
            .WithMany(c => c.usersAchievement)
            .HasForeignKey(s => s.idAchievement)
            .HasConstraintName("ForeignKey_userAchiev_achiev");


           
            modelBuilder.Entity<StatisticDatapoint>()
            .HasOne(s => s.users)
            .WithMany(c => c.statisticDatapoint)
            .HasForeignKey(s => s.idUser)
            .HasConstraintName("ForeignKey_statisticDP_user");
            modelBuilder.Entity<StatisticDatapoint>()
            .HasOne(s => s.statistics)
            .WithMany(c => c.statisticDatapoint)
            .HasForeignKey(s => s.idStatistic)
            .HasConstraintName("ForeignKey_statisticDP_achiev");
            modelBuilder.Entity<StatisticDatapoint>()
            .HasOne(s => s.tasks)
            .WithMany(c => c.statisticDatapoint)
            .HasForeignKey(s => s.idStatistic)
            .HasConstraintName("ForeignKey_statisticDP_task");
                              
            modelBuilder.Entity<StatisticType>().HasKey(c => c.idStatistic);

            modelBuilder.Entity<XP>().HasKey(c => new { c.idUser, c.idTask ,c.date});
            modelBuilder.Entity<XP>()
            .HasOne(s => s.tasks)
            .WithMany(c => c.xP)
            .HasForeignKey(s => s.idTask)
            .HasConstraintName("ForeignKey_XPCateg");
            modelBuilder.Entity<XP>()
            .HasOne(s => s.User)
            .WithMany(c => c.xP)
            .HasForeignKey(s => s.idUser)
            .HasConstraintName("ForeignKey_XPUser");


            modelBuilder.Entity<Users>().HasIndex(b => b.nickName).IsUnique();

            modelBuilder.Entity<Tasks>().ToTable("Tasks");
                       
        }
    }


}

