
using Domain.Models;
using Domain.Factory;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("id/{id}")]
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
    public async Task<IActionResult> Create(UserDTO dto)
    {
        try
        {
            var created = await _userService.CreateAsync(dto);
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

    [HttpPut("id/{id}")]
    public async Task<IActionResult> Update(long id, UserDTO dto)
    {
        bool wasUpdated = await _userService.UpdateAsync(dto, id);

        if (!wasUpdated)
            return BadRequest("Update failed.");

        return NoContent();
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
