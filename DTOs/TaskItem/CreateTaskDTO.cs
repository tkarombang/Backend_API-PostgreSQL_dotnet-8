using System.ComponentModel.DataAnnotations;

public class CreateTaskDTO
{

  public required string Title { get; set; }

  public required string Description { get; set; }

  public DateTime? StartDate { get; set; }
  public DateTime? EndDate { get; set; }

  public required string Priority { get; set; }

  public required string Status { get; set; }

  public int? DeveloperId { get; set; } //OPTIONAL
  public required int ProjectId { get; set; } //MANDATORY

}