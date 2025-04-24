using Application.DTO;
using Application.Services;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // Post: api/User
        [HttpPost("addusers")]
        public async Task<ActionResult<UserDTO>> AddUsers([FromBody] UserDTO user)
        {
            {
                var result = await _userService.Add(user);
                return Ok(result);
            }
        }

        // Get: api/User
        [HttpGet("getusers")]
        public async Task<ActionResult<UserDTO>> GetUsers()
        {
            var result = await _userService.GetAll();
            return Ok(result);
        }
    }
}