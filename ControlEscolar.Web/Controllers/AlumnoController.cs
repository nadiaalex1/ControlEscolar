using Microsoft.AspNetCore.Mvc;
using ControlEscolar.Services;

namespace ControlEscolar.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlumnoController : ControllerBase
{
    private readonly AlumnoService _service;

    public AlumnoController(AlumnoService service)
    {
        _service = service;
    }

[HttpPost("registrar")]
public IActionResult RegistrarAlumno([FromBody] AlumnoDto alumno)
{
    Console.WriteLine($"Recibido: {alumno.Nombre} {alumno.ApellidoPaterno} {alumno.ApellidoMaterno}");
    _service.RegistrarAlumno(alumno.Nombre, alumno.ApellidoPaterno, alumno.ApellidoMaterno);
    return Ok(new { mensaje = "Alumno registrado correctamente" });
}

    [HttpGet("obtener")]
public IActionResult ObtenerAlumnos()
{
    try
    {
        var alumnos = _service.ObtenerAlumnos();
        return Ok(alumnos);
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { mensaje = "Error interno: " + ex.Message });
    }
}

[HttpGet("buscar")]
public IActionResult BuscarPorNombre(string nombre)
{
    try
    {
        var resultados = _service.BuscarAlumnNombre(nombre);
        return Ok(resultados);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Error al buscar alumnos: {ex.Message}");
    }
}


[HttpGet("costo-total/{idAlumno}")]
public IActionResult ObtenerCostoTotal(int idAlumno)
{
    var total = _service.ObtenerCostoTotalPorAlumno(idAlumno);
    return Ok(new { total });
}

[HttpPost("agregar-materia")]
public IActionResult AgregarMateriaAlumno([FromBody] MateriaAlumnoDto dto)
{
    _service.AgregarMateriaAlumno(dto.IdAlumno, dto.IdMateria);
    return Ok(new { mensaje = "Materia agregada al alumno" });
}

public class MateriaAlumnoDto
{
    public int IdAlumno { get; set; }
    public int IdMateria { get; set; }
}

[HttpDelete("eliminar/{id}")]
public IActionResult EliminarAlumno(int id)
{
    try
    {
        _service.EliminarAlumno(id);
        return Ok(new { mensaje = "Alumno eliminado correctamente" });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { mensaje = "Error: " + ex.Message });
    }
}

[HttpPut("actualizar/{id}")]
public IActionResult ActualizarAlumno(int id, [FromBody] AlumnoDto alumno)
{
    _service.ActualizarAlumno(id, alumno.Nombre, alumno.ApellidoPaterno, alumno.ApellidoMaterno);
    return Ok(new { mensaje = "Alumno actualizado correctamente" });
}
}


public class AlumnoDto
{
    public required string Nombre { get; set; }
    public required string ApellidoPaterno { get; set; }
    public required string ApellidoMaterno { get; set; }
    public required string Usuario { get; set; }
    public required string Contrasenia { get; set; }
}




