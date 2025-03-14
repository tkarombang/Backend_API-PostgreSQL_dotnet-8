public class CreateReportDTO
{
  public int ReportId { get; set; }
  public int? DeveloperId { get; set; }
  public int? ProjectId { get; set; }
  public int? TaskId { get; set; }
  public DateTime Date { get; set; }
  public int HoursSpent { get; set; }
  public required string Remarks { get; set; }
}