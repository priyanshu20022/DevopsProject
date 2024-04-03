using Jenkins.API.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace Jenkins.API.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Project> Projects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    Id = 1,
                    Name = "Project 1",
                    LastModified = DateTime.Now,
                    CreatedOn = DateTime.Now,
                    OwnerId = 1,
                    Shared = false
                },
                new Project
                {
                    Id = 2,
                    Name = "Project 3",
                    LastModified = DateTime.Now,
                    CreatedOn = DateTime.Now,
                    OwnerId = 2,
                    Shared = true
                }
            );
        }
    }
}
