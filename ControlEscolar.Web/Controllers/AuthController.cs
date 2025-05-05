using Microsoft.AspNetCore.Mvc;
using ControlEscolar.Services;
namespace ControlEscolar.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AlumnoService _alumnoService;

    public AuthController(AlumnoService alumnoService)
    {
        _alumnoService = alumnoService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDTO dto)
    {
        var alumno = _alumnoService.ObtenerAlumnoPorCredenciales(dto.Usuario, dto.Contrasenia);
        if (alumno == null)
            return Unauthorized(new { mensaje = "Credenciales inválidas" });

        return Ok(alumno);
    }
}

public class LoginDTO
{
    public string? Usuario { get; set; }
    public string? Contrasenia { get; set; }
}
