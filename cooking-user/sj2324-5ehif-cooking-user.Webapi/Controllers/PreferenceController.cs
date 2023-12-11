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
    public async Task<ActionResult<List<PreferenceDto>>> GetAll()
    {
        var result = await _preferenceRepository.GetAllAsync();
        if (result.success)
        {
            var preferences = result.entity.Select(p => _mapper.Map<PreferenceDto>(p)).ToList();
            return Ok(preferences);
        }
        return NotFound(result.message);
    }
}
