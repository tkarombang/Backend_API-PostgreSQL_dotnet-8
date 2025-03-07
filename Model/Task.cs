using MyApp.Model;

public class Task{
    public int  TaskId { get; set; }
    public int ProjectId { get; set; } 
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; } 
    public string Priority { get; set; }
    public string Status { get; set; }
    public int? DeveloperId { get; set; }


    public Project Project { get; set; }
    public Developer Developer { get; set;}
}
