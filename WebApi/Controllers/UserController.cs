
using Domain.Models;
using Domain.Factory;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IUserFactory _userFactory;

    public UsersController(UserService userService, IUserFactory userFactory)
    {
        _userService = userService;
        _userFactory = userFactory;
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        try
        {
            var user = await _userFactory.Create(dto.Names, dto.Surnames, dto.Email, dto.DeactivationDate ?? DateTime.MaxValue);
            var created = await _userService.CreateAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = created.GetId() }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
