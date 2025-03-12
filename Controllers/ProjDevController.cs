using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Model;

namespace MyApp.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjDevController : ControllerBase{
        private readonly MyDbContext _context;
        public ProjDevController(MyDbContext context){
            _context = context;
        }


        // GET: API/ProjectDeveloper
        // [HttpGet]
        // public async Task<IActionResult> GetAllProjectDevelopers()
        // {
        //     var projectDevelopers = await _context.ProjectDevelopers
        //         .Include(pd => pd.Project)
        //         .Include(pd => pd.Developers)
        //         .ToListAsync();

        //         return Ok(projectDevelopers);
        // }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProjeDevById(int id)
        {
            if(id <= 0){
                return BadRequest("INVALID ID");
            }

            var projeDev = await _context.ProjectDevelopers.FirstOrDefaultAsync(x => x.ProjectId == id);

            if(projeDev == null){
                return NotFound(new { message = $"{id} TIDAK DITEMUKAN"});
            }

            return Ok(projeDev);
        }


        // CONTROLLER BY DTO (DATA TRANSFERED OBJECT)
        //POST: API/ProjectDeveloper
        [HttpPost]
        public async Task<IActionResult> CreateProjectDevelop([FromBody] CreateProjectDevelopDTO projeDevDto)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var project = await _context.Projects.FindAsync(projeDevDto.ProjectId);
            var developer = await _context.Developers.FindAsync(projeDevDto.DeveloperId); 

            if(project == null || developer == null){
                return NotFound("Project atau Developer TIDAK DITEMUKAN");
            }

            var projectDevelopers = new ProjectDeveloper
            {
                ProjectId = projeDevDto.ProjectId,
                DeveloperId = projeDevDto.DeveloperId
            };

            _context.ProjectDevelopers.Add(projectDevelopers);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProjeDevById), new { id = projeDevDto.ProjectId }, projeDevDto);
        }

    }
}