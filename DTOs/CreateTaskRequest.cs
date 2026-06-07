using System.ComponentModel.DataAnnotations;
namespace TaskTrackerApi.DTOs;

public class CreateTaskRequest
{
    [Required]
    [MaxLength(100)]
    public  string Title   { get; set; } = ""  ; 
    [MaxLength(500)]
    public string Description { get; set; } = "";
}