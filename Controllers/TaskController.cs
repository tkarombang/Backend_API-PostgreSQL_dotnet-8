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

        if(task == null){
          return NotFound($"Task Dengan {id} Tidak ditemukan");
        }

        return Ok(task); 
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] TaskItem taskItem)
    {
      if(!ModelState.IsValid){
        return BadRequest(ModelState);
      }

      _context.TaskItems.Add(taskItem);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetTaskById), new  {id = taskItem.TaskId}, taskItem);
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskItem taskItem)
    {
      _logger.LogInformation("Fetching All Task");
      if(id != taskItem.TaskId){
        return BadRequest("Task ID Tidak Cocok");
      }

      if(string.IsNullOrWhiteSpace(taskItem.Title) || string.IsNullOrWhiteSpace(taskItem.Description)){
        return BadRequest("Title dan Description tidak boleh kosong.");
      }

      var existingTask = await _context.TaskItems.FindAsync(id);
      if(existingTask == null){
        return NotFound($"Task dengan {id} Tidak Ditemukan");
      } 

      existingTask.Title = taskItem.Title;
      existingTask.Description = taskItem.Description;
      existingTask.ProjectId = taskItem.ProjectId;
      existingTask.DeveloperId = taskItem.DeveloperId;

      await _context.SaveChangesAsync();
      return NoContent();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
      var task = await _context.TaskItems.FindAsync(id);
      if(task == null){
        return NotFound($"Task with {id} Tidak Ditemukan");
      }

      try{
      _context.TaskItems.Remove(task);
      await _context.SaveChangesAsync();
      return NoContent();
      
      }catch(Exception ex){
      _logger.LogError(ex, $"ERROR SAAT MENGHAPUS TASK {id}", id);
      return StatusCode(500, "TERJADI KESALAHAN SERVER");

      }

    } 

  }
}