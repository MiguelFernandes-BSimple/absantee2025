using Application.DTO;
using Application.Services;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/rhmanagers")]
[ApiController]
public class RHManagerController : ControllerBase
{
    private readonly RHManagerService _rhManagerService;
    public RHManagerController(RHManagerService rhManagerService)
    {
        _rhManagerService = rhManagerService;
    }
    // Post: api/rhmanager
    [HttpPost]
    public async Task<ActionResult<CreateRHManagerDTO>> Postrhmanagers([FromBody] CreateRHManagerDTO rHManagerDTO)
    {
        {
            if (rHManagerDTO == null)
                return BadRequest("Invalid Arguments");

            var rHManagerDTOResult = await _rhManagerService.Add(rHManagerDTO);

            if (rHManagerDTOResult == null) return BadRequest();

            return Created("", rHManagerDTOResult);
        }
    }
}

