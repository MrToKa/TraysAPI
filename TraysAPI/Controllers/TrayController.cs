using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraysAPI.Data;
using TraysAPI.Data.Models;

namespace CableManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrayController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TrayController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{trayName}")]
        public async Task<IActionResult> GetTrayInfo(string trayName)
        {
            var cables = await _context.Cables
                .Include(c => c.CableType)
                .ToListAsync();  // Fetch all cables into memory

            // Apply the filter with StringComparison after fetching data
            var filteredCables = cables
                .Where(c => c.Routing.Split('/')
                    .Any(segment => string.Equals(segment, trayName, StringComparison.OrdinalIgnoreCase)))  // Case-insensitive comparison
                .ToList();

            var tray = await _context.Trays
                .Where(t => t.Name == trayName)
                .Select(t => new
                {
                    t.Width,
                    Height = t.Height - 15,
                    Cables = filteredCables
                        .Select(c => new
                        {
                            CableInfo = $"{c.CableName} => {c.CableType.Type} => {c.CableType.Diameter} => {c.CableType.Weight} => {c.CableInformation}"
                        })
                        .ToList()
                    //Cables = _context.Cables
                    //    .Include(c => c.CableType)
                    //    .Where(c => c.Routing.Contains(trayName))
                    //    .Select(c => new
                    //    {
                    //        CableInfo = $"{c.CableName} => {c.CableType.Type} => {c.CableType.Diameter} => {c.CableType.Weight} => {c.CableInformation}"
                    //    })
                    //    .ToList()
                })
                .FirstOrDefaultAsync();

            if (tray == null)
                return NotFound();

            return Ok(tray);
        }

        [HttpGet]
        [Route("names")]
        public async Task<IActionResult> GetTrays()
        {
            var trays = await _context.Trays
                .Select(t => new
                {
                    t.Name
                })
                .ToListAsync();

            return Ok(trays);
        }
    }
}
