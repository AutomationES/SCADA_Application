using Microsoft.AspNetCore.Mvc;
using SCADA.Common.Interfaces;
using SCADA.Common.Models;

namespace SCADA.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PLCsController : ControllerBase
{
    private readonly IPLCRepository _plcRepository;

    public PLCsController(IPLCRepository plcRepository)
    {
        _plcRepository = plcRepository;
    }

    // GET: api/PLCs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PLC>>> GetPLCs(bool includeTags = false)
    {
        return Ok(await _plcRepository.GetAllPLCsAsync(includeTags));
    }

    // GET: api/PLCs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PLC>> GetPLC(int id, bool includeTags = false)
    {
        var plc = await _plcRepository.GetPLCByIdAsync(id, includeTags);
        return plc == null ? NotFound() : Ok(plc);
    }

    // POST: api/PLCs
    [HttpPost]
    public async Task<ActionResult<PLC>> PostPLC(PLC plc)
    {
        if (await _plcRepository.IpAddressExistsAsync(plc.IPAddress))
            return Conflict("IP address already in use");

        await _plcRepository.AddPLCAsync(plc);
        return CreatedAtAction(nameof(GetPLC), new { id = plc.Id }, plc);
    }

    // DELETE: api/PLCs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePLC(int id)
    {
        await _plcRepository.DeletePLCAsync(id);
        return NoContent();
    }
}