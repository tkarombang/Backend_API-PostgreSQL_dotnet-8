using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using System.Threading.Tasks;

namespace MyApp.DeveloperControllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class DevelopersController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DevelopersController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/developers
        [HttpGet]
        public async Task<IActionResult> GetAllDevelopers()
        {
            var developers = await _context.Developers.ToListAsync();
            return Ok(developers);
        }

        // GET: api/developers/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDeveloperById(int id)
        {
            var developer = await _context.Developers.FirstOrDefaultAsync(d => d.developer_id == id);

            if (developer == null)
            {
                return NotFound($"Developer with ID {id} not found.");
            }

            return Ok(developer);
        }

        // GET: api/developers/name/{name}
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetDeveloperByName(string name)
        {
            var developers = await _context.Developers
                .Where(d => EF.Functions.ILike(d.nama, $"%{name}%"))
                .ToListAsync();

            if (developers == null || developers.Count == 0)
            {
                return NotFound($"No developers found with name containing '{name}'.");
            }

            return Ok(developers);
        }
    }
}
