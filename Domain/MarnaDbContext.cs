using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class MarnaDbContext : DbContext
    {
        public MarnaDbContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseSqlServer(
                "Server=localhost;Database=VblogMarnaDb;Integrated Security=True;TrustServerCertificate=True;"
            );
        }
        public MarnaDbContext(DbContextOptions<MarnaDbContext> options) : base(options) { }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
