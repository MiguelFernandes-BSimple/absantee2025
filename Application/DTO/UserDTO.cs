using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTO;

public record UserDTO
{
    public string Names { get; set; }
    public string Surnames { get; set; }
    public string Email { get; set; }
    public DateTime FinalDate { get; set; }
    public UserDTO()
    {
    }
}
