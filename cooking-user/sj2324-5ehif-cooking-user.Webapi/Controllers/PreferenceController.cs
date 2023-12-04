using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sj2324_5ehif_cooking_user.Application.DTO;
using sj2324_5ehif_cooking_user.Application.Model;
using sj2324_5ehif_cooking_user.Application.Repository;

namespace sj2324_5ehif_cooking_user.Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PreferenceController : ControllerBase
{
    private readonly IRepository<Preference> _preferenceRepository;
    private readonly IMapper _mapper;
    

    public PreferenceController(IRepository<Preference> preferenceRepository, IMapper mapper)
    {
        _preferenceRepository = preferenceRepository;
        _mapper = mapper;
            
    }

    // GET: api/Preference
    [HttpGet]
    public async Task<ActionResult<List<Preference>>> GetAll()
    {
        var result = await _preferenceRepository.GetAllAsync();
        if (result.success) return Ok(result.entity);
        return NotFound(result.message);
    }

    // GET: api/Preference/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Preference>> GetById(string id)
    {
        var result = await _preferenceRepository.GetByIdAsync(id);
        if (result.success) return Ok(result.entity);
        return NotFound(result.message);
    }

    // POST: api/Preference
    [HttpPost]
    public async Task<ActionResult<Preference>> Post(AddPreferenceDto preference)
    {
        var preferenceModel = _mapper.Map<Preference>(preference);
        var result = await _preferenceRepository.InsertOneAsync(preferenceModel);
        if (result.success) return CreatedAtAction(nameof(GetById), new { id = preferenceModel.Key }, preference);
        return BadRequest(result.message);
    }

    //Todo : find out how to update correctly
    /*// PUT: api/Preference/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, Preference preference)
    {
        var result = await _preferenceRepository.UpdateOneAsync(preference);
        if (result.success) return NoContent();
        return BadRequest(result.message);
    }*/

    // DELETE: api/Preference/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _preferenceRepository.DeleteOneAsync(id);
        if (result.success) return NoContent();
        return BadRequest(result.message);
    }
}