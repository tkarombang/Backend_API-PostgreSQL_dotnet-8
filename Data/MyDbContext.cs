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
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectDeveloper> ProjectDevelopers { get; set; } // Contoh DbSet
        public DbSet<TaskItem> TaskItems { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<Project>()
                .Property(proj => proj.Start_Date)
                .HasConversion(
                    v => v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                );

            modelBuilder.Entity<Project>()
                .Property(proj => proj.End_Date)
                .HasConversion(
                    v => v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                );

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
                entity.ToTable("project_developer"); // Pastikan nama tabel sesuai
                entity.HasKey(pd => new { pd.ProjectId, pd.DeveloperId });

                entity.Property(pd => pd.ProjectId)
                    .HasColumnName("project_id");

                entity.Property(pd => pd.DeveloperId)
                    .HasColumnName("developer_id");

                // RELASI KE PROJECT
                entity.HasOne(pd => pd.Project)
                    .WithMany(p => p.ProjectDeveloper)
                    .HasForeignKey(p => p.ProjectId);

                // RELASI KE DEVELOPER
                entity.HasOne(pd => pd.Developers)
                    .WithMany(d => d.ProjectDevelopers)
                    .HasForeignKey(d => d.DeveloperId);
            });


            modelBuilder.Entity<TaskItem>(entity => {
                entity.ToTable("task");
            
                //KONFIGURASI RELASI Task -> Project
                entity.HasOne(t => t.Project) //TASK MEMILIKI SATU PROJECT
                    .WithMany(p => p.TaskItem) //PROJECT MEMILIKI BANYAK TASK
                    .HasForeignKey(t => t.ProjectId); //FOREIGN KEY ADALAH ProjectId
    
                //KONFIGURASI RELASI Task -> Developers
                entity.HasOne(t => t.Developers)
                    .WithMany(d => d.TaskItem)
                    .HasForeignKey(t => t.DeveloperId);
            });
        
        }


    }
}

public class MyEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
