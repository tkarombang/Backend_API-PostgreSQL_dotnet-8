using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyApp.Model;

public class ReportItem
{
  public int ReportId { get; set;}
  public int ProjectId { get; set; }
  public int DeveloperId { get; set; }
  public int TaskId { get; set; }
  public Project? Project { get; set; }
  public Developer? Dev { get; set; }
  public TaskItem? TaskItem { get; set; }
  public required DateTime Date { get; set;}
  public int HoursSpent { get; set;}
  public string? Remarks { get; set;}
}