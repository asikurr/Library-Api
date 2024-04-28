using Library.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Library.Api
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions appDbContext) : base(appDbContext)
        {

        }
        public AppDbContext() { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authors>()
                        .HasKey(a => a.AuthorID)
                        .HasName("PK_Authors");

            modelBuilder.Entity<Members>()
                        .HasKey(a => a.MemberID)
                        .HasName("PK_Members");

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Members> Members { get; set; }
    }
}
