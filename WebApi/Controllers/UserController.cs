
using Domain.Models;
using Domain.Factory;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
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

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var user = await _userService.GetByEmailAsync(email);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpGet("name/{names}")]
    public async Task<IActionResult> GetByNames(string names)
    {
        var users = await _userService.GetByNamesAsync(names);
        if (!users.Any())
            return NotFound("No users found with the given name.");

        return Ok(users);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] CreateUserDto dto)
    {
        var existing = await _userService.GetByIdAsync(id);
        if (existing == null)
            return NotFound("User not found.");

        var user = new User(id, dto.Names, dto.Surnames, dto.Email, new PeriodDateTime(
            DateTime.UtcNow, (dto.DeactivationDate ?? DateTime.MaxValue).ToUniversalTime()
        ));

        try
        {
            await _userService.UpdateAsync(user);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveUsers()
    {
        var users = await _userService.GetActiveUsersAsync();
        if (!users.Any())
            return NotFound("No active users found.");
        return Ok(users);
    }
}
