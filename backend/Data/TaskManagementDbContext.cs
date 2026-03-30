using Microsoft.EntityFrameworkCore;
using TaskManagement.Api.Models;

namespace TaskManagement.Api.Data;

public class TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options) : DbContext(options)
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.ToTable("Tasks");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("Id");
            entity.Property(t => t.Title).HasColumnName("Title").IsRequired().HasMaxLength(200);
            entity.Property(t => t.Description).HasColumnName("Description").HasMaxLength(1000);
            entity.Property(t => t.IsCompleted).HasColumnName("IsCompleted").IsRequired();
            entity.Property(t => t.CreatedAt).HasColumnName("CreatedAt").IsRequired();
        });
    }
}
