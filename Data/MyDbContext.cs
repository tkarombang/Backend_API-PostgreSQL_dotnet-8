using Microsoft.EntityFrameworkCore;
using MyApp.Model;

namespace MyApp.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options) { }

        public DbSet<MyEntity> MyEntities { get; set; } // Contoh DbSet
        public DbSet<Developer> Developers { get; set; } // Contoh DbSet
        public DbSet<Project> Projects{ get; set; }
        public DbSet<ProjectDeveloper> ProjectDevelopers { get; set; } // Contoh DbSet


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<Developer>(entity =>
            {
                entity.ToTable("developers"); // Pastikan nama tabel sesuai dengan database
                entity.HasKey(d => d.DeveloperId); // Menentukan primary key
            });

            modelBuilder.Entity<Project>(entity => 
            {
                entity.ToTable("project");
                entity.HasKey(proj => proj.ProjectId);
            });

            modelBuilder.Entity<ProjectDeveloper>(entity =>
            {
                entity.ToTable("project_developers"); // Pastikan nama tabel sesuai
                entity.HasKey(pd => pd.developer_id);
            });
        }


    }
}

public class MyEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}
