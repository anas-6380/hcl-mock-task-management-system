using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.Services;

namespace TaskManagement.Api.Controllers;

public class TaskPageController(ITaskService taskService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var tasks = await taskService.GetAllTasksAsync();
        return View(tasks);
    }
}
