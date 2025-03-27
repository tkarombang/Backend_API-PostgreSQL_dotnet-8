using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyApp.Model;

public class TaskItem
{
    public int TaskId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public required string Priority { get; set; }
    public required string Status { get; set; }
    public int ProjectId { get; set; }
    public int DeveloperId { get; set; }    
    public Project? Project { get; set; }
    public Developer? Dev { get; set; }
    public ICollection<ReportItem> Reports { get; set; } = new List<ReportItem>();

}
