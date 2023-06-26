using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context.EntityFramework
{
    public class SimpleContextDb : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-2TP3190;Initial Catalog=AlparslanDb;Integrated Security=True;TrustServerCertificate=True;");


        }

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
