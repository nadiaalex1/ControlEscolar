namespace ControlEscolar.Core.Models
{
    public class Alumno
    {
        public int IdAlumno { get; set; }
        public required string Nombre { get; set; }
        public required string ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public required string Usuario { get; set; }
        public required string Contrasenia { get; set; }
    }

    public class Materia
    {
        public int IdMateria { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Costo { get; set; }
    }
}

