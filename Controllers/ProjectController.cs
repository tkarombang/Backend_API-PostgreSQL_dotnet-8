using Microsoft.AspNetCore.Mvc;
// using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Model;


namespace MyApp.ProjectControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(MyDbContext context, ILogger<ProjectController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Get all projects with optional pagination.
        /// </summary>
        /// <param name="page">Page number (default: 1).</param>
        /// <param name="pageSize">Number of items per page (default: 10).</param>
        /// <returns>List of all projects.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllProject([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation("Fetching all projects with pagination. Page: {page}, PageSize: {pageSize}", page, pageSize);

            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Page and pageSize must be greater than zero.");
            }

            var projects = await _context.Projects
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(projects);
        }

        /// <summary>
        /// Get a project by its ID.
        /// </summary>
        /// <param name="id">The ID of the project.</param>
        /// <returns>The project with the specified ID.</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            _logger.LogInformation("Fetching project with ID: {id}", id);

            if (id <= 0)
            {
                return BadRequest("Invalid ID.");
            }

            var project = await _context.Projects.FirstOrDefaultAsync(x => x.ProjectId == id);

            if (project == null)
            {
                return NotFound(new { Message = $"Project with ID {id} not found." });
            }

            return Ok(project);
        }

        /// <summary>
        /// Get projects by name containing a specific keyword.
        /// </summary>
        /// <param name="name">The keyword to search in project names.</param>
        /// <returns>List of projects with names containing the keyword.</returns>
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetProjectByName(string name)
        {
            _logger.LogInformation("Searching for projects with name containing: {name}", name);

            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Name parameter cannot be empty.");
            }

            var projects = await _context.Projects
                .Where(x => EF.Functions.ILike(x.Nama, $"%{name}%"))
                .ToListAsync();

            if (projects == null || projects.Count == 0)
            {
                return NotFound(new { Message = $"No projects found with name containing '{name}'." });
            }

            return Ok(projects);
        }

        /// <summary>
        // POST
        /// <summary>
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating a new project.");

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProjectById), new { id = project.ProjectId }, project);
        }

        // PUT
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] Project updateProject)
        {
            if(!ModelState.IsValid || id != updateProject.ProjectId)
            {
                return BadRequest("Invalid Data.");
            }

            var existingProject = await _context.Projects.FirstOrDefaultAsync(x => x.ProjectId == id);
            if (existingProject == null)
            {
                return NotFound(new { Message = $"Project with ID {id} Not Found." });
            }

            existingProject.Nama = updateProject.Nama;
            existingProject.Description = updateProject.Description;
            existingProject.Start_Date = updateProject.Start_Date;
            existingProject.End_Date = updateProject.End_Date;
            existingProject.Status = updateProject.Status;

            _context.Projects.Update(existingProject);


            // updateProject.SomeDateTimeProperty = DateTime.SpecifyKind(updateProject.SomeDateTimeProperty, DateTimeKind.Utc);


            await _context.SaveChangesAsync();

            return Ok(existingProject);           

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if(project == null){
                return NotFound($"Project dengan {id} Tidak Ditemukan");
            }

            try{
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
                return NoContent();
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

    }
}
