using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.SignalR;
using SCADA.Backend.Hubs;
using SCADA.Common.Interfaces;
using SCADA.Common.Models;
using SCADA.Common.ViewModels.Tags;

namespace SCADA.Backend.Controllers;

[Authorize]
[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[ApiVersion("1.0")]  // Optional versioning
public class TagsController : ControllerBase
{
    private readonly ITagRepository _tagRepository;
    private readonly IPLCRepository _plcRepository;
    private readonly IHubContext<TagHub> _hubContext;

    public TagsController(ITagRepository tagRepository, IPLCRepository plcRepository, IHubContext<TagHub> hubContext)
    {
        _tagRepository = tagRepository;
        _plcRepository = plcRepository;
        _hubContext = hubContext;
    }

    // GET: api/Tags
    /// <summary>
    /// Retrieves all tags
    /// </summary>
    /// <response code="200">Returns the list of tags</response>
    [HttpGet]
    [OutputCache(PolicyName = "TagsCache")]
    [ProducesResponseType(typeof(IEnumerable<TagDto>), 200)]
    [Authorize(Roles = "Admin,Operator")]
    public async Task<IActionResult> GetTags()
    {
        var tags = await _tagRepository.GetAllTagsAsync();
        return Ok(tags);
    }
    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<Tag>>> GetTags(int? plcId = null)
    //{
    //    if (plcId.HasValue)
    //        return Ok(await _tagRepository.GetTagsByPLCIdAsync(plcId.Value));

    //    return Ok(await _tagRepository.GetAllTagsAsync());
    //}

    // GET: api/Tags/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Tag>> GetTag(int id)
    {
        var tag = await _tagRepository.GetTagByIdAsync(id);
        return tag == null ? NotFound() : Ok(tag);
    }

    // POST: api/Tags
    [HttpPost]
    public async Task<ActionResult<Tag>> PostTag(Tag tag)
    {
        if (!await _plcRepository.PLCExistsAsync(tag.PlcId))
            return BadRequest("PLC does not exist");

        await _tagRepository.AddTagAsync(tag);
        await _hubContext.Clients.Group($"tag-{tag.Id}").SendAsync("TagValueUpdated", tag);
        return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);


    }
}