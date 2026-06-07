using TaskTrackerApi.DTOs;

namespace TaskTrackerApi.Services;

public interface ITaskService
{
    Task<List<TaskResponse>> GetAllAsync(TaskQueryParameters query);
    Task<TaskResponse?> GetByIdAsync(int id);
    Task<TaskResponse> CreateAsync(CreateTaskRequest request);
    Task<TaskResponse?> UpdateAsync(int id, UpdateTaskRequest request);
    Task<bool> DeleteAsync(int id);
    Task<TaskResponse?> CompleteTaskAsync(int id);
}