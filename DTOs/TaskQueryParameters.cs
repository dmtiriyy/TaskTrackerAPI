namespace TaskTrackerApi.DTOs;

public class TaskQueryParameters
{
    public bool? IsCompleted { get; set; }
    public string? Search {get; set;}
}