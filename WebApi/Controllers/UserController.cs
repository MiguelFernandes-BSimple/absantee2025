using Application.DTO;
using Application.Services;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    List<string> _errorMessages = new List<string>();
    public UserController(UserService userService)
    {
        _userService = userService;
    }

    // Post: api/User
    [HttpPost]
    public async Task<ActionResult<UserDTO>> PostUsers(UserDTO userDTO)
    {
        {
            var userDTOResult = await _userService.Add(userDTO);
            return Ok(userDTOResult);
        }
    }

    // Get: api/User
    [HttpGet]
    public async Task<ActionResult<UserDTO>> GetUsers()
    {
        var result = await _userService.GetAll();
        return Ok(result);
    }

    // Patch: api/User/dfgsdfgd/activation
    [HttpPatch("{id}/activation")]
    public async Task<ActionResult<UserDTO>> UpdateActivation([FromBody] Guid Id, ActivationDTO activationPeriodDTO)
    {
        if (!await _userService.Exists(Id))
            return NotFound();

        var result = await _userService.UpdateActivation(Id, activationPeriodDTO);
        return Ok(result);
    }
    /* [HttpPost()]
    public async Task<ActionResult<UserDTO>> PostRHManager(Guid Id, RHManagerDTO RHManagerDTO, List<string> errorMessage)
    {
        if (!await _userService.Exists(Id))
            return NotFound("Utilizador não encontrado.");

        var result = await rhService.CreateRHManager(Id, RHManagerDTO);

        if (result == null)
            return BadRequest("Erro ao criar RH Manager.");

        return Ok(result);
    } */
}
