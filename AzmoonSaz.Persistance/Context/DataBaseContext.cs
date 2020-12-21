using AzmoonSaz.Application.Interfaces;
using AzmoonSaz.Domain.Entities.Classroom;
using AzmoonSaz.Domain.Entities.Test;
using AzmoonSaz.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzmoonSaz.Persistance.Context
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        #region Entities

        public DbSet<User> Users { get; set; }

        public DbSet<UserInClassroom> UserInClassrooms { get; set; }

        public DbSet<Classroom> Classrooms { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<Question> Questions { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.UserName);

            SetRelations(modelBuilder);
        }


        #region Relations
        private void SetRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Classroom>()
                .HasOne(c => c.Creator)
                .WithMany(u => u.Classrooms)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserInClassroom>()
                .HasOne(u => u.Classroom)
                .WithMany(c => c.UserInClassrooms)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserInClassroom>()
                .HasOne(u => u.User)
                .WithMany(u => u.UserInClassrooms)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Test>()
                .HasOne(t => t.Classroom)
                .WithMany(c => c.Tests)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Test)
                .WithMany(t => t.Questions)
                .OnDelete(DeleteBehavior.NoAction);
        }
        #endregion
    }
}
