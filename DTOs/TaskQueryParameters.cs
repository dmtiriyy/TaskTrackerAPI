namespace TaskTrackerApi.DTOs;

public class TaskQueryParameters
{
    public bool? IsCompleted { get; set; }
    public string? Search {get; set;}
    public string? SortBy {get; set;}
    public string? SortDirection {get; set;}
}