using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Model;
using System.Threading.Tasks;

namespace MyApp.DeveloperControllers
{
	[ApiController]

	[Route("api/[controller]")]
	public class DevelopersController : ControllerBase
	{
		private readonly MyDbContext _context;
		private readonly ILogger<DevelopersController> _logger;

		public DevelopersController(MyDbContext context, ILogger<DevelopersController> logger)
		{
			_context = context;
			_logger = logger;
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
			var developer = await _context.Developers.FirstOrDefaultAsync(d => d.DeveloperId == id);

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
					.Where(d => EF.Functions.ILike(d.Nama, $"%{name}%"))
					.ToListAsync();

			if (developers == null || developers.Count == 0)
			{
				return NotFound($"No developers found with name containing '{name}'.");
			}

			return Ok(developers);
		}


		// POST: api/developers
		[HttpPost]
		public async Task<IActionResult> CreateDeveloper([FromBody] Developer developer)
		{
			if(!ModelState.IsValid){
				return BadRequest(ModelState);
			}

			// KONVERSI TANGGAL LAHIR KE UTC
			if(developer.TanggalLahir.HasValue){
				developer.TanggalLahir = DateTime.SpecifyKind(developer.TanggalLahir.Value, DateTimeKind.Utc);
			}

			_context.Developers.Add(developer);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetDeveloperById), new { id = developer.DeveloperId}, developer);
		}


		//PUT: api/developers/{id}
		[HttpPut("{id:int}")]
		public async Task<IActionResult> UpdateDeveloper(int id, [FromBody] Developer developerUpdate)
		{
			if(!ModelState.IsValid){
				return BadRequest(ModelState);
			}

			if (id != developerUpdate.DeveloperId){
				return BadRequest("Developer Id TIDAK ADA");
			}

			var developer = await _context.Developers.FindAsync(id);
			if(developer == null)	{
				return NotFound($"DEVELOPER DENGAN {id} TIDAK DITEMUKAN");
			}

			// update data
			developer.Nama = developerUpdate.Nama;
			developer.Email = developerUpdate.Email;
			developer.Role = developerUpdate.Role;
			developer.Phone = developerUpdate.Phone;


			try{
				await _context.SaveChangesAsync();
				return Ok(developer);
			}catch(DbUpdateException){
				return StatusCode(500, "Terjadi Kesalahaan Saat Menyimpan Data");
			}


		}
	}
}
