using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;

namespace MyApp.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjDevController : ControllerBase{
        private readonly MyDbContext _context;
        public ProjDevController(MyDbContext context){
            _context = context;
        }


        // GET: API/ProjectDeveloper
        [HttpGet]
        public async Task<IActionResult> GetAllProjectDevelopers()
        {
            var projectDevelopers = await _context.ProjectDevelopers
                .Include(pd => pd.Project)
                .Include(pd => pd.Developer)
                .ToListAsync();

                return Ok(projectDevelopers);
        }

        //POST: API/ProjectDeveloper

    }
}