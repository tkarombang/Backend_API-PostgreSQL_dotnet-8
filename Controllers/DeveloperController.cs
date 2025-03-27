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

		[HttpPost("bulk")]
		public async Task<IActionResult> AddBulkDevelopers([FromBody] List<CreateDeveloperDTO> developerDtos)
		{

			var developers = developerDtos.Select(d => new Developer
			{
				Nama = d.Nama,
				Email = d.Email,
				Role = d.Role,
				Phone = d.Phone,
				TanggalLahir = d.TanggalLahir.HasValue
					? DateTime.SpecifyKind(d.TanggalLahir.Value, DateTimeKind.Utc)
					: null,
				Status = d.Status,
				Gender = (JenisKelamin?)d.Gender,
			}).ToList();
		
			_context.Developers.AddRange(developers);
			await _context.SaveChangesAsync();
			return Ok();
		}


		// POST: api/developers
		[HttpPost]
		public async Task<IActionResult> CreateDeveloper([FromBody] CreateDeveloperDTO dtoBody)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			if (await _context.Developers.AnyAsync(e => e.Email == dtoBody.Email))
			{
				return BadRequest($"Email {dtoBody.Email} Sudah Digunakan Oleh Developer lain");
			}

			var developer = new Developer
			{
				Nama = dtoBody.Nama,
				Email = dtoBody.Email.ToLowerInvariant(),
				Role = dtoBody.Role,
				Phone = dtoBody.Phone,
				TanggalLahir = dtoBody.TanggalLahir.HasValue
					? DateTime.SpecifyKind(dtoBody.TanggalLahir.Value, DateTimeKind.Utc)
					: null,
				Status = dtoBody.Status,
				Gender = dtoBody.Gender.HasValue
					? (JenisKelamin?)dtoBody.Gender
					: null,
			};

			try
			{
				_context.Developers.Add(developer);
				await _context.SaveChangesAsync();

				return CreatedAtAction(nameof(GetDeveloperById), new { id = developer.DeveloperId }, developer);

			}
			catch (DbUpdateException ex) when (ex.InnerException is Npgsql.PostgresException mailUniq && mailUniq.SqlState == "23505")
			{
				return BadRequest($"Email {dtoBody.Email} Sudah Digunakan Oleh Developer lain");
			}

		}


		//PUT: api/developers/{id}
		[HttpPut("{id:int}")]
		public async Task<IActionResult> UpdateDeveloper(int id, [FromBody] UpdateDeveloperDTO dtoUpdateDev)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != dtoUpdateDev.DeveloperId)
			{
				return BadRequest("Developer Id TIDAK ADA");
			}

			var existDev = await _context.Developers.FindAsync(id);
			if (existDev == null)
			{
				return NotFound($"DEVELOPER DENGAN {id} TIDAK DITEMUKAN");
			}

			// update data
			existDev.Nama = dtoUpdateDev.Nama;
			existDev.Email = dtoUpdateDev.Email.ToLowerInvariant();
			existDev.Role = dtoUpdateDev.Role;
			existDev.Phone = dtoUpdateDev.Phone;
			existDev.TanggalLahir = dtoUpdateDev.TanggalLahir.HasValue
				? DateTime.SpecifyKind(dtoUpdateDev.TanggalLahir.Value, DateTimeKind.Utc)
				: null;
			existDev.Status = dtoUpdateDev.Status;
			existDev.Gender = dtoUpdateDev.Gender.HasValue
				? (JenisKelamin?)dtoUpdateDev.Gender
				: null;


			try
			{
				await _context.SaveChangesAsync();
				return Ok(existDev);
			}
			catch (DbUpdateException)
			{
				return StatusCode(500, "Terjadi Kesalahaan Saat Menyimpan Data");
			}
		}


		//DELETE: api/developers/{id}
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteDeveloper(int id)
		{
			var existDev = await _context.Developers.FindAsync(id);
			if (existDev == null)
			{
				return NotFound($"Developer dengan {id} TIDAK DITEMUKAN");
			}

			try
			{
				_context.Developers.Remove(existDev);
				await _context.SaveChangesAsync();
				return NoContent();
			}
			catch (Exception ex)
			{
				// Log detail error ke konsol atau logger
				Console.WriteLine($"Error: {ex.Message}");
				if (ex.InnerException != null)
				{
					Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
				}
				return BadRequest(new
				{
					Message = ex.Message,
					InnerException = ex.InnerException?.Message
				});
			}
		}
	}
}
