using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace MyApp.Model
{
    [Table("developers")]
    public class Developer
    {
        public int DeveloperId { get; set; }
        public required string Nama { get; set; }
        public required string Email { get; set; }
        public string? Role { get; set; } 
        public string? Phone { get; set; }
        public DateTime? TanggalLahir { get; set; }
        public string? Status { get; set; }
        public JenisKelamin? Gender { get; set; }
        public string? GenderLabel => Gender.HasValue ? Gender.Value.ToString() : null;

        public ICollection<ProjectDeveloper> ProjectDevelopers { get; set; } = new List<ProjectDeveloper>();
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
        public ICollection<ReportItem> Reports { get; set; } = new List<ReportItem>();
    }

    // Enum untuk jenis kelamin
    public enum JenisKelamin{
        Pria = 1,
        Wanita = 2
    }
}