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
}

