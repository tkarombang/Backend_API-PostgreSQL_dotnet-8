using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyApp.Model;

public class ReportItem
{
  [Key]
  [Column("report_id")]
  public int ReportId { get; set;}

  [ForeignKey("DeveloperId")]
  [Column("developer_id")]
  public int? DeveloperId { get; set; }
  public Developer? Developers { get; set; }

  [ForeignKey("ProjectId")]
  [Column("project_id")]
  public int? ProjectId { get; set; }
  public Project? Project { get; set; }

  [ForeignKey("TaskId")]
  [Column("task_id")]
  public int? TaskId { get; set; }
  public TaskItem? TaskItem { get; set; }

  [Column("date")]
  public required DateTime Date { get; set;}

  [Column("hours_spent")]
  public int HoursSpent { get; set;}

  [Column("remarks")]
  public required string Remarks { get; set;}
}