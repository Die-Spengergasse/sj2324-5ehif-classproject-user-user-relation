using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sj2324_5ehif_cooking_user.Application.DTO;
using sj2324_5ehif_cooking_user.Application.Model;
using sj2324_5ehif_cooking_user.Application.Repository;

namespace sj2324_5ehif_cooking_user.Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PreferenceController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRepository<Preference> _preferenceRepository;

    public PreferenceController(IRepository<Preference> preferenceRepository, IMapper mapper)
    {
        _preferenceRepository = preferenceRepository;
        _mapper = mapper;
    }

    /// <summary>
    ///     Retrieves all preferences from the repository.
    /// </summary>
    /// <returns>
    ///     Returns a list of preferences if successful, otherwise returns a NotFound response with an error message.
    /// </returns>
    /// GET: api/Preference
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<Preference>>> GetAll()
    {
        var result = await _preferenceRepository.GetAllAsync();
        if (result.success)
            return Ok(result.entity.Select(dto => _mapper.Map<PreferenceDto>(dto)));
        return NotFound(result.message);
    }

    /// <summary>
    ///     Retrieves preferences from the repository that start with the specified string.
    /// </summary>
    /// <param name="startWith">The prefix to filter preferences.</param>
    /// <returns>
    ///     Returns a list of preferences that match the filter if successful, otherwise returns a NotFound response with an
    ///     error message.
    /// </returns>
    /// GET: api/Preference/StartsWith
    [Authorize]
    [HttpGet("api/Preference/StartsWith")]
    public async Task<ActionResult<List<Preference>>> GetAllThatStartsWith(string letters)
    {
        var result = await _preferenceRepository.GetAllAsync();

        if (!result.success) return NotFound(result.message);
        var filteredPreferences = result.entity
            .AsEnumerable() // Switch to client-side evaluation
            .Where(preference => preference.Name.StartsWith(letters, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(filteredPreferences.Select(dto => _mapper.Map<PreferenceDto>(dto)));
    }
    
    /// <summary>
    ///     Retrieves a specific preference by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the preference.</param>
    /// <returns>
    ///     Returns the preference if found, otherwise returns a NotFound response with an error message.
    /// </returns>
    /// GET: api/Preference/5
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<Preference>> GetById(string id)
    {
        var result = await _preferenceRepository.GetByIdAsync(id);
        if (result.success) return Ok(_mapper.Map<PreferenceDto>(result.entity));
        return NotFound(result.message);
    }
}