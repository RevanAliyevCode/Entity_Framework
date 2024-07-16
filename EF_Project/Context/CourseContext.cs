using EF_Project.Concrets;
using EF_Project.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Project.Context
{
    public class CourseContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.Default);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().HasQueryFilter(x => x.IsDelete == false);
            modelBuilder.Entity<Teacher>().HasQueryFilter(x => x.IsDelete == false);
            modelBuilder.Entity<Student>().HasQueryFilter(x => x.IsDelete == false);

            modelBuilder.Entity<Group>().Property(x => x.BeginDate).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Group>().Property(x => x.EndDate).HasDefaultValue(DateTime.Now.AddMonths(6));
        }
    }
}
