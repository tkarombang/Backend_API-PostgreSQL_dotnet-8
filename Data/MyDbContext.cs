using Microsoft.EntityFrameworkCore;
using MyApp.Model;

namespace MyApp.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options) { }

        public DbSet<MyEntity> MyEntities { get; set; } // Contoh DbSet
        public DbSet<ProjectDeveloper> ProjectDevelopers { get; set; } // Contoh DbSet


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectDeveloper>()
                .HasKey(pd => pd.developer_id); // Menentukan primary key dengan Fluent API
        }


    }
}

public class MyEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}
