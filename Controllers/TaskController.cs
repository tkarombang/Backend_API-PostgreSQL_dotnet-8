using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;

namespace MyApp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TaskController : ControllerBase
  {
    private readonly MyDbContext _context;
    private readonly ILogger<TaskController> _logger;

    public TaskController(MyDbContext context, ILogger<TaskController> logger)
    {
      _context = context;
      _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {

      _logger.LogInformation("Fetching All Task");
      var task = await _context.TaskItems
        .Include(t => t.Project) //RELASI KE PROJECT
        .Include(t => t.Developers) //RELASI KE DEVELOPER
        .ToListAsync();

      return Ok(task);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
      _logger.LogInformation("Fetching All Task");
      var task = await _context.TaskItems
        .Include(t => t.Project)
        .Include(t => t.Developers)
        .FirstOrDefaultAsync(t => t.TaskId == id);

      if (task == null)
      {
        return NotFound($"Task Dengan {id} Tidak ditemukan");
      }

      var getTaskDTO = new GetTaskDTO
      {
        TaskId = task.TaskId,
        Title = task.Title,
        Description = task.Description,
        StartDate = task.StartDate,
        EndDate = task.EndDate,
        Priority = task.Priority,
        Status = task.Status,
        DeveloperId = task.DeveloperId,
        ProjectId = task.ProjectId,
      };

      return Ok(getTaskDTO);
    }



    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDTO dtoTask)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var createTaskDto = new TaskItem
      {
        Title = dtoTask.Title,
        Description = dtoTask.Description,
        StartDate = dtoTask.StartDate,
        EndDate = dtoTask.EndDate,
        Priority = dtoTask.Priority,
        Status = dtoTask.Status,
        DeveloperId = dtoTask.DeveloperId,
        ProjectId = dtoTask.ProjectId
      };

      _context.TaskItems.Add(createTaskDto);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetTaskById), new { id = createTaskDto.TaskId }, createTaskDto);
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDTO taskItemDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var existingTask = await _context.TaskItems.FindAsync(id);
      if (existingTask == null)
      {
        return NotFound($"Task dengan {id} Tidak Ditemukan");
      }


      if (string.IsNullOrWhiteSpace(taskItemDto.Title) || string.IsNullOrWhiteSpace(taskItemDto.Description))
      {
        return BadRequest("Title dan Description tidak boleh kosong.");
      }


      existingTask.Title = taskItemDto.Title;
      existingTask.Description = taskItemDto.Description;
      existingTask.StartDate = taskItemDto.StartDate;
      existingTask.EndDate = taskItemDto.EndDate;
      existingTask.Priority = taskItemDto.Priority;
      existingTask.Status = taskItemDto.Status;
      existingTask.DeveloperId = taskItemDto.DeveloperId;
      existingTask.ProjectId = taskItemDto.ProjectId;

      try
      {
        await _context.SaveChangesAsync();
        return Ok(existingTask);
      }
      catch (DbUpdateException)
      {
        return StatusCode(500, "Terjadi Kesalahan saat menyimpan Data");
      }
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
      var task = await _context.TaskItems.FindAsync(id);
      if (task == null)
      {
        return NotFound($"Task with {id} Tidak Ditemukan");
      }

      try
      {
        _context.TaskItems.Remove(task);
        await _context.SaveChangesAsync();
        return NoContent();

      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"ERROR SAAT MENGHAPUS TASK {id}", id);
        return StatusCode(500, "TERJADI KESALAHAN SERVER");

      }

    }

  }
}