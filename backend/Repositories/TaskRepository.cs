using Microsoft.EntityFrameworkCore;
using TaskManagement.Api.Data;
using TaskManagement.Api.Models;

namespace TaskManagement.Api.Repositories;

public class TaskRepository(TaskManagementDbContext dbContext) : ITaskRepository
{
    public async Task<List<TaskItem>> GetAllAsync()
    {
        return await dbContext.Tasks.OrderByDescending(t => t.CreatedAt).ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TaskItem> CreateAsync(TaskItem task)
    {
        dbContext.Tasks.Add(task);
        await dbContext.SaveChangesAsync();
        return task;
    }

    public async Task<bool> UpdateAsync(TaskItem task)
    {
        var existingTask = await dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == task.Id);
        if (existingTask is null)
        {
            return false;
        }

        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.IsCompleted = task.IsCompleted;

        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existingTask = await dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        if (existingTask is null)
        {
            return false;
        }

        dbContext.Tasks.Remove(existingTask);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
