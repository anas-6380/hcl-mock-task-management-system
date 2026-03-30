using TaskManagement.Api.DTOs;

namespace TaskManagement.Api.Services;

public interface ITaskService
{
    Task<List<TaskReadDto>> GetAllTasksAsync();
    Task<TaskReadDto?> GetTaskByIdAsync(int id);
    Task<TaskReadDto> CreateTaskAsync(TaskCreateDto dto);
    Task<bool> UpdateTaskAsync(int id, TaskUpdateDto dto);
    Task<bool> DeleteTaskAsync(int id);
}
