using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyApp.Model;

public class TaskItem
{
    [Key]
    [Column("task_id")]
    public int TaskId { get; set; }

    [Column("title")]
    public required string Title { get; set; }
    
    [Column("description")]
    public required string Description { get; set; }
    
    [Column("start_date")]
    public DateTime? StartDate { get; set; }
    
    [Column("end_date")]
    public DateTime? EndDate { get; set; }
    
    [Column("priority")]
    public required string Priority { get; set; }
    
    [Column("status")]
    public required string Status { get; set; }

    [ForeignKey("DeveloperId")]
    [Column("developer_id")]
    public int? DeveloperId { get; set; }    
    public Developer? Developers { get; set; }

    [ForeignKey("ProjectId")]
    [Column("project_id")]
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
}
