using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Api.DTOs;

public class TaskCreateDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Required]
    public bool IsCompleted { get; set; }
}
