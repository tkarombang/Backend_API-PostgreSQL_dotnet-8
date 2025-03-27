


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Model
{
    public class Project
    {
        public int ProjectId { get; set;}
        public required string Nama { get; set;}
        public required string Description { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string? Status{ get; set; }
        // public DateTime SomeDateTimeProperty { get; internal set; }
        public ICollection<ProjectDeveloper> ProjectDevelopers { get; set; } = new List<ProjectDeveloper>();
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
        public ICollection<ReportItem> Reports { get; set; } = new List<ReportItem>();

    }

}