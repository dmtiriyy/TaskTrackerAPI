using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTrackerApi.Data;
using TaskTrackerApi.DTOs;
using TaskTrackerApi.Services;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
   private readonly ITaskService _taskService;
    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
   public async Task<ActionResult<List<TaskResponse>>> GetTasks()
   {
      var taskResponse = await _taskService.GetAllAsync();
      
      return Ok(taskResponse);
   }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskResponse>> GetTask([FromRoute] int id)
    {
        var taskResponse = await _taskService.GetByIdAsync(id);
        if (taskResponse == null)
        {
            return NotFound();
        }
        return Ok(taskResponse);
    }
    [HttpPost]
    public async Task<ActionResult<TaskResponse>> AddTask([FromBody] CreateTaskRequest request)
    {
        var taskResponse = await _taskService.CreateAsync(request);
        return Ok(taskResponse);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TaskResponse>> UpdateTask([FromRoute] int id, [FromBody] UpdateTaskRequest request)
    {
        var taskResponse = await _taskService.UpdateAsync(id, request);
        if (taskResponse == null)
        {
            return NotFound();
        }

        return Ok(taskResponse);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTask([FromRoute] int id)
    {
        var taskResponse = await _taskService.DeleteAsync(id);
        if (taskResponse == false)
        {
            return NotFound();
        }
        return NoContent();
        
    }

    [HttpPatch("{id}/complete")]
    public async Task<ActionResult<TaskResponse>> CompleteTask([FromRoute] int id)
    {
        var taskResponse = await _taskService.CompleteTaskAsync(id);
        if (taskResponse == null)
        {
            return NotFound();
        }
        return Ok(taskResponse);
    }
    
}