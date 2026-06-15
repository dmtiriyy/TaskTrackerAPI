using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTrackerApi.Data;
using TaskTrackerApi.DTOs;
using TaskTrackerApi.Entities;

namespace TaskTrackerApi.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _context;
    public TaskService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<TaskResponse>> GetAllAsync( TaskQueryParameters query)
    {
        var queryable = _context.TaskItems.AsQueryable();
        if (query.IsCompleted != null)
        {
            queryable = queryable.Where(task => task.IsCompleted == query.IsCompleted.Value);
        }

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            queryable = queryable.Where(task => task.Title.Contains(query.Search));
        }

        if (query.SortBy == "title")
        {
            queryable = query.SortDirection == "desc" ? queryable.OrderByDescending(task => task.Title) : queryable.OrderBy(task => task.Title);
        }

        if (query.SortBy == "createdAt")
        {
            queryable = query.SortDirection == "desc" ? queryable.OrderByDescending(task => task.CreatedAt ) : queryable.OrderBy(task => task.CreatedAt);
        }

        if (query.SortBy == "isCompleted")
        {
            queryable = query.SortDirection == "desc" ? queryable.OrderByDescending(task => task.IsCompleted) : queryable.OrderBy(task => task.IsCompleted);
        }
        queryable = queryable
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize);
        var tasks = await queryable.ToListAsync();
        var response = new List<TaskResponse>();

        for (int i = 0; i < tasks.Count; i++)
        {
            response.Add(MapToResponse(tasks[i]));
        }

        return response;
    }

    public async Task<TaskResponse?> GetByIdAsync(int id)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task == null)
        {
            return null;
        }

        var response = MapToResponse(task);
        return response;
    }

    public async Task<TaskResponse> CreateAsync(CreateTaskRequest request)
    {
        var taskItem = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            IsCompleted = false,
            CreatedAt = DateTime.Now,
        };

        _context.TaskItems.Add(taskItem);
        await _context.SaveChangesAsync();
        var response = MapToResponse(taskItem);
        return response;
    }

    public async Task<TaskResponse?> UpdateAsync(int id, UpdateTaskRequest request)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task == null)
        {
            return null;
        }
        task.Title = request.Title;
        task.Description = request.Description;
        task.IsCompleted = request.IsCompleted;
        await _context.SaveChangesAsync();
        var response = MapToResponse(task);
        return response;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task == null)
        {
            return false;
        }

        _context.TaskItems.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<TaskResponse?> CompleteTaskAsync(int id)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task == null)
        {
            return null;
        }
        task.IsCompleted = true;
        await _context.SaveChangesAsync();
        return MapToResponse(task);
    }
    private TaskResponse MapToResponse(TaskItem task)
    {
        return new TaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            IsCompleted = task.IsCompleted,
            CreatedAt = task.CreatedAt,
        };
    }
}