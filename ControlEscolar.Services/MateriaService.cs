using ControlEscolar.Core.Models;
using ControlEscolar.Data;

namespace ControlEscolar.Services
{
    public class MateriaService
    {
        private readonly MateriaRepository _repository;

        public MateriaService(MateriaRepository repository)
        {
            _repository = repository;
        }

        public void RegistrarMateria(string nombre, decimal costo)
        {
            var materia = new Materia
            {
                Nombre = nombre,
                Costo = costo
            };
            _repository.InsertarMateria(materia);
        }

        public Materia? BuscarPorNombre(string nombre)
{
    return _repository.BuscarPorNombre(nombre);
}

public void EliminarMateria(int id)
{
    _repository.EliminarMateria(id);
}        public List<Materia> ObtenerMateriasDeAlumno(int idAlumno)
{
    return _repository.ObtenerMateriasDeAlumno(idAlumno).ToList();
}
public List<Materia> ObtenerMaterias()
{
    return _repository.ObtenerMaterias();
}

public decimal ObtenerCostoTotal(int idAlumno)
{
    return _repository.ObtenerCostoTotal(idAlumno);
}

public void AgregarMateriaAlumno(int idAlumno, int idMateria)
{
    _repository.AgregarMateriaAlumno(idAlumno, idMateria);
}
    }

}
