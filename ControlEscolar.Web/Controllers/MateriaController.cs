using Microsoft.AspNetCore.Mvc;
using ControlEscolar.Services;
using ControlEscolar.Core.Models;

namespace ControlEscolar.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MateriaController : ControllerBase
{
    private readonly MateriaService _service;


    public MateriaController(MateriaService service)
    {
        _service = service;
    }

    [HttpPost("registrar")]
    public IActionResult RegistrarMateria([FromBody] Materia materia)
    {
        _service.RegistrarMateria(materia.Nombre, materia.Costo );
        return Ok(new { mensaje = "Materia registrada correctamente" });
    }

   [HttpGet("buscar/{nombre}")]
public IActionResult BuscarPorNombre(string nombre)
{
    var materia = _service.BuscarPorNombre(nombre);
    if (materia == null)
        return NotFound(new { mensaje = "Materia no encontrada" });

    return Ok(materia);
}
[HttpGet]
public IActionResult ObtenerMaterias()
{
    var materias = _service.ObtenerMaterias();
    return Ok(materias);
}


[HttpDelete("{id}")]
public IActionResult EliminarMateria(int id)
{
    _service.EliminarMateria(id);
    return Ok(new { mensaje = "Materia eliminada correctamente" });
}

[HttpGet("materias-alumno/{idAlumno}")]
public IActionResult ObtenerMateriasDeAlumno(int idAlumno)
{
    var materias = _service.ObtenerMateriasDeAlumno(idAlumno);
    return Ok(materias);
}

[HttpGet("costo-total/{idAlumno}")]
public IActionResult ObtenerCostoTotal(int idAlumno)
{
    var total = _service.ObtenerCostoTotal(idAlumno);
    return Ok(total);
}

[HttpPost("agregar-materia")]
public IActionResult AgregarMateriaAlumnoPorNombre([FromBody] AgregarMateriaDTO dto)
{
    var materia = _service.BuscarPorNombre(dto.NombreMateria);
    if (materia == null)
        return NotFound(new { mensaje = "La materia no existe" });

    _service.AgregarMateriaAlumno(dto.IdAlumno, materia.IdMateria);
    return Ok(new { mensaje = "Materia agregada correctamente al alumno" });
}


}
