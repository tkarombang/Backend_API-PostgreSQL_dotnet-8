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
      var report = await _context.ReportsItems
        .Include(r => r.Developers)
        .Include(r => r.Project)
        .Include(r => r.TaskItem)
        .ToListAsync();

      return Ok(report);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetReportsById(int id)
    {
      var report = await _context.ReportsItems
        .Include(r => r.Developers)
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
        return BadRequest(new { message = "data Report tidak bole NULL" });
      }

      var CreateReportDto = new ReportItem
      {
        ReportId = rprt.ReportId,
        DeveloperId = rprt.DeveloperId,
        ProjectId = rprt.ProjectId,
        TaskId = rprt.TaskId,
        Date = rprt.Date,
        HoursSpent = rprt.HoursSpent,
        Remarks = rprt.Remarks
      };

      _context.ReportsItems.Add(CreateReportDto);
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

      var existReport = await _context.ReportsItems.FindAsync(id);
      if (existReport == null)
      {
        return NotFound($"Report dengan {id} TIDAK DITEMUKAN");
      }

      existReport.DeveloperId = reportUpdate.DeveloperId;
      existReport.ProjectId = reportUpdate.ProjectId;
      existReport.TaskId = reportUpdate.TaskId;
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
      var existReport = await _context.ReportsItems.FindAsync(id);

      if (existReport == null)
      {
        return NotFound($"Report dengan {id} TIDAK DITEMUKAN");
      }

      try
      {
        _context.ReportsItems.Remove(existReport);
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