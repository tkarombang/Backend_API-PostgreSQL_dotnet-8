using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;

namespace MyApp.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ReportController : ControllerBase
  {
    private readonly MyDbContext _context;

    public ReportController(MyDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReports()
    {
      var report = await _context.Reports
        .Include(r => r.Dev)
        .Include(r => r.Project)
        .Include(r => r.TaskItem)
        .ToListAsync();

      return Ok(report);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetReportsById(int id)
    {
      var report = await _context.Reports
        .Include(r => r.Dev)
        .Include(r => r.Project)
        .Include(r => r.TaskItem)
        .FirstOrDefaultAsync(r => r.ReportId == id);

      if (report == null)
      {
        return NotFound($"Report dengan {id} TIDAK DITEMUKAN");
      }

      return Ok(report);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReport([FromBody] CreateReportDTO rprt)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (rprt == null)
      {
        return BadRequest(new { message = "data Report tidak boleh NULL" });
      }

      var CreateReportDto = new ReportItem
      {
        ReportId = rprt.ReportId,
        DeveloperId = rprt.DeveloperId ?? 0,
        ProjectId = rprt.ProjectId ?? 0,
        TaskId = rprt.TaskId ?? 0,
        Date = rprt.Date,
        HoursSpent = rprt.HoursSpent,
        Remarks = rprt.Remarks
      };

      _context.Reports.Add(CreateReportDto);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetReportsById), new { id = CreateReportDto.ReportId }, CreateReportDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateReport(int id, [FromBody] UpdateReportDto reportUpdate)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var existReport = await _context.Reports.FindAsync(id);
      if (existReport == null)
      {
        return NotFound($"Report dengan {id} TIDAK DITEMUKAN");
      }

      existReport.DeveloperId = reportUpdate.DeveloperId ?? 0;
      existReport.ProjectId = reportUpdate.ProjectId ?? 0;
      existReport.TaskId = reportUpdate.TaskId ?? 0;
      existReport.Date = reportUpdate.Date;
      existReport.HoursSpent = reportUpdate.HoursSpent;
      existReport.Remarks = reportUpdate.Remarks;

      try
      {
        await _context.SaveChangesAsync();
        return Ok(existReport);
      }
      catch (DbUpdateException)
      {
        return StatusCode(500, "Terjadi Kesalahan saat menyimpan Data");
      }
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteReport(int id)
    {
      var existReport = await _context.Reports.FindAsync(id);

      if (existReport == null)
      {
        return NotFound($"Report dengan {id} TIDAK DITEMUKAN");
      }

      try
      {
        _context.Reports.Remove(existReport);
        await _context.SaveChangesAsync();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }


  }
}