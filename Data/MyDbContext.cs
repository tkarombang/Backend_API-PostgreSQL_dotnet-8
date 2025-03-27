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
    public DbSet<ProjectDeveloper> Project_developer { get; set; } // Contoh DbSet
    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<ReportItem> Reports { get; set; }




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
        entity.Property(d => d.DeveloperId).HasColumnName("developer_id").ValueGeneratedOnAdd();
        entity.Property(d => d.Nama).HasColumnName("nama").HasMaxLength(100);
        entity.Property(d => d.Email).HasColumnName("email").HasMaxLength(100);
        entity.Property(d => d.Role).HasColumnName("role").HasMaxLength(50);
        entity.Property(d => d.Phone).HasColumnName("phone").HasMaxLength(12);
        entity.Property(d => d.TanggalLahir).HasColumnName("tanggal_lahir");
        entity.Property(d => d.Status).HasColumnName("status").HasMaxLength(10);
        entity.Property(d => d.Gender).HasColumnName("jenis_kelamin").HasMaxLength(10);
      });



      modelBuilder.Entity<Project>(entity =>
      {
        entity.ToTable("project");
        entity.HasKey(p => p.ProjectId);
        entity.Property(p => p.ProjectId).HasColumnName("project_id");
        entity.Property(p => p.Nama).HasColumnName("nama").HasMaxLength(100);
        entity.Property(p => p.Description).HasColumnName("description");
        entity.Property(p => p.Start_Date).HasColumnName("start_date")
          .HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        entity.Property(p => p.End_Date).HasColumnName("end_date")
          .HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        entity.Property(p => p.Status).HasColumnName("status").HasMaxLength(20);        
      });


      modelBuilder.Entity<ProjectDeveloper>(entity =>
      {
        entity.ToTable("project_developer"); // Pastikan nama tabel sesuai
        entity.HasKey(pd => new { pd.ProjectId, pd.DeveloperId });
        entity.Property(pd => pd.ProjectId).HasColumnName("project_id");
        entity.Property(pd => pd.DeveloperId).HasColumnName("developer_id");

        // RELASI KE PROJECT
        entity.HasOne(pd => pd.Project)
                  .WithMany(p => p.ProjectDevelopers)
                  .HasForeignKey(p => p.ProjectId)
                  .OnDelete(DeleteBehavior.Cascade);

        // RELASI KE DEVELOPER
        entity.HasOne(pd => pd.Dev)
                  .WithMany(d => d.ProjectDevelopers)
                  .HasForeignKey(d => d.DeveloperId)
                  .OnDelete(DeleteBehavior.Cascade);
      });


      modelBuilder.Entity<TaskItem>(entity =>
      {
        entity.ToTable("task");
        entity.HasKey(t => t.TaskId);
        entity.Property(t => t.TaskId).HasColumnName("task_id");
        entity.Property(t => t.ProjectId).HasColumnName("project_id");
        entity.Property(t => t.DeveloperId).HasColumnName("developer_id");
        entity.Property(t => t.Title).HasColumnName("title");
        entity.Property(t => t.Description).HasColumnName("description");
        entity.Property(t => t.StartDate).HasColumnName("start_date");
        entity.Property(t => t.EndDate).HasColumnName("end_date");
        entity.Property(t => t.Priority).HasColumnName("priority");
        entity.Property(t => t.Status).HasColumnName("status");

        //RELASI Task -> Project
        entity.HasOne(t => t.Project) //TASK MEMILIKI SATU PROJECT
            .WithMany(t => t.Tasks) //PROJECT MEMILIKI BANYAK TASK
            .HasForeignKey(t => t.ProjectId) //FOREIGN KEY ADALAH ProjectId
            .HasConstraintName("task_project_id_fkey")
            .OnDelete(DeleteBehavior.Restrict);

        //RELASI Task -> Developers
        entity.HasOne(t => t.Dev)
            .WithMany(t => t.Tasks)
            .HasForeignKey(t => t.DeveloperId)
            .HasConstraintName("fk_developer_pkey")
            .OnDelete(DeleteBehavior.Cascade);
      });


      modelBuilder.Entity<ReportItem>(entity =>
      {
        entity.ToTable("report");
        entity.HasKey(r => r.ReportId);
        entity.Property(r => r.ReportId).HasColumnName("report_id");
        entity.Property(r => r.ProjectId).HasColumnName("project_id");
        entity.Property(r => r.DeveloperId).HasColumnName("developer_id");
        entity.Property(r => r.TaskId).HasColumnName("task_id");
        entity.Property(r => r.Date).HasColumnName("date");
        entity.Property(r => r.HoursSpent).HasColumnName("hours_spent");
        entity.Property(r => r.Remarks).HasColumnName("remarks");
        
        //RELASI Report -> Project
        entity.HasOne(r => r.Project)
            .WithMany(p => p.Reports).HasForeignKey(r => r.ProjectId)
            .HasForeignKey(r => r.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        //RELASI Report -> Developers
        entity.HasOne(r => r.Dev)
            .WithMany(d => d.Reports).HasForeignKey(r => r.DeveloperId)
            .HasForeignKey(r => r.DeveloperId)
            .OnDelete(DeleteBehavior.Cascade);

        //RELASI Report -> TaskItem
        entity.HasOne(r => r.TaskItem)
            .WithMany(t => t.Reports).HasForeignKey(r => r.TaskId)
            .HasForeignKey(r => r.TaskId)
            .OnDelete(DeleteBehavior.Restrict);
      });

    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=sampledb;Username=postgres;Password=admin")
        .LogTo(Console.WriteLine, LogLevel.Information);
    }


  }
}

public class MyEntity
{
  public int Id { get; set; }
  public required string Name { get; set; }
}
