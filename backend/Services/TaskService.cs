using TaskManagement.Api.DTOs;
using TaskManagement.Api.Models;
using TaskManagement.Api.Repositories;

namespace TaskManagement.Api.Services;

public class TaskService(ITaskRepository taskRepository) : ITaskService
{
    public async Task<List<TaskReadDto>> GetAllTasksAsync()
    {
        var tasks = await taskRepository.GetAllAsync();
        return tasks.Select(MapToReadDto).ToList();
    }

    public async Task<TaskReadDto?> GetTaskByIdAsync(int id)
    {
        var task = await taskRepository.GetByIdAsync(id);
        return task is null ? null : MapToReadDto(task);
    }

    public async Task<TaskReadDto> CreateTaskAsync(TaskCreateDto dto)
    {
        var newTask = new TaskItem
        {
            Title = dto.Title.Trim(),
            Description = string.IsNullOrWhiteSpace(dto.Description) ? null : dto.Description.Trim(),
            IsCompleted = dto.IsCompleted,
            CreatedAt = DateTime.UtcNow
        };

        var createdTask = await taskRepository.CreateAsync(newTask);
        return MapToReadDto(createdTask);
    }

    public async Task<bool> UpdateTaskAsync(int id, TaskUpdateDto dto)
    {
        var task = new TaskItem
        {
            Id = id,
            Title = dto.Title.Trim(),
            Description = string.IsNullOrWhiteSpace(dto.Description) ? null : dto.Description.Trim(),
            IsCompleted = dto.IsCompleted
        };

        return await taskRepository.UpdateAsync(task);
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        return await taskRepository.DeleteAsync(id);
    }

    private static TaskReadDto MapToReadDto(TaskItem task)
    {
        return new TaskReadDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            IsCompleted = task.IsCompleted,
            CreatedAt = task.CreatedAt
        };
    }
}
