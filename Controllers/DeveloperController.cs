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
			var devByID = await _context.Developers.FirstOrDefaultAsync(d => d.DeveloperId == id);

			if (devByID == null)
			{
				return NotFound($"Developer with ID {id} not found.");
			}

			return Ok(devByID);
		}

		// GET: api/developers/name/{name}
		[HttpGet("name/{name}")]
		public async Task<IActionResult> GetDeveloperByName(string name)
		{
			var devNama = await _context.Developers
					.Where(d => EF.Functions.ILike(d.Nama, $"%{name}%"))
					.ToListAsync();

			if (devNama == null || devNama.Count == 0)
			{
				return NotFound($"No developers found with name containing '{name}'.");
			}

			return Ok(devNama);
		}


		// POST: api/developers
		[HttpPost]
		public async Task<IActionResult> CreateDeveloper([FromBody] Developer devBody)
		{
			if(!ModelState.IsValid){
				return BadRequest(ModelState);
			}

			// KONVERSI TANGGAL LAHIR KE UTC
			if(devBody.TanggalLahir.HasValue){
				devBody.TanggalLahir = DateTime.SpecifyKind(devBody.TanggalLahir.Value, DateTimeKind.Utc);
			}

			_context.Developers.Add(devBody);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetDeveloperById), new { id = devBody.DeveloperId}, devBody);
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

			var existDev = await _context.Developers.FindAsync(id);
			if(existDev == null)	{
				return NotFound($"DEVELOPER DENGAN {id} TIDAK DITEMUKAN");
			}

			// update data
			existDev.Nama = developerUpdate.Nama;
			existDev.Email = developerUpdate.Email;
			existDev.Role = developerUpdate.Role;
			existDev.Phone = developerUpdate.Phone;


			try{
				await _context.SaveChangesAsync();
				return Ok(existDev);
			}catch(DbUpdateException){
				return StatusCode(500, "Terjadi Kesalahaan Saat Menyimpan Data");
			}
		}


		//DELETE: api/developers/{id}
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteDeveloper(int id)
		{
			var existDev = await _context.Developers.FindAsync(id);
			if(existDev == null){
				return NotFound($"Developer dengan {id} TIDAK DITEMUKAN");
			}

			try{
				_context.Developers.Remove(existDev);
				await _context.SaveChangesAsync();
				return NoContent();
			}catch(Exception ex){
				return BadRequest(ex.Message);
			}
		}
	}
}
