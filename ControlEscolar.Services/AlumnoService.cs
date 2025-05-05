namespace ControlEscolar.Services{

    using ControlEscolar.Core.Models;
    using ControlEscolar.Data;
    using System.Text.RegularExpressions;
    using System;
    using System.Collections.Generic;
    using BCrypt = BCrypt.Net.BCrypt;
    using Microsoft.Data.SqlClient;


    public class AlumnoService
    {
        private readonly AlumnoRepository _repo;

        public AlumnoService(AlumnoRepository repo)
        {
            _repo = repo;
        }

public void RegistrarAlumno(string nombre, string apellidoP, string apellidoM)
        {
            var usuario = GenerarUsuario(nombre, apellidoP);
            var contrasenia = GenerarContrasena();
            var hash = BCrypt.HashPassword(contrasenia);

            var alumno = new Alumno
            {
                Nombre = nombre,
                ApellidoPaterno = apellidoP,
                ApellidoMaterno = apellidoM,
                Usuario = usuario,
                Contrasenia = hash
            };
            _repo.InsertarAlumno(alumno);
        }
        
        public void EliminarAlumno(int idAlumno)
{
    _repo.EliminarAlumno(idAlumno);
}
        public void ActualizarAlumno(int id, string nombre, string apellidoP, string apellidoM)
{
    _repo.ActualizarAlumno(id, nombre, apellidoP, apellidoM);
}  
  public List<Alumno> BuscarAlumnNombre(string nombre)
{
    return _repo.BuscarAlumNombre(nombre);
}
     public decimal ObtenerCostoTotalPorAlumno(int idAlumno)
{
    return _repo.ObtenerCostoTotalPorAlumno(idAlumno);
}

public void AgregarMateriaAlumno(int idAlumno, int idMateria)
{
    _repo.AgregarMateriaAlumno(idAlumno, idMateria);
}

 
        public List<Alumno> ObtenerAlumnos()
{
    return _repo.ObtenerAlumnos();
}

        private string GenerarUsuario(string nombre, string apellidoPaterno)
        {
            var inicial = nombre.Substring(0, 1).ToLower();
            var apellido = apellidoPaterno.ToLower();
            var random = new Random();
            var numeros = random.Next(10, 99);
            return $"{inicial}.{apellido}{numeros}";
        }

        private string GenerarContrasena()
        {
            var letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var numeros = "0123456789";
            var random = new Random();
            var contrasena = new List<char>
            {
                letras[random.Next(0, 26)],
                numeros[random.Next(0, numeros.Length)]
            };
            while (contrasena.Count < 8)
            {
                var pool = letras + numeros;
                contrasena.Add(pool[random.Next(0, pool.Length)]);
            }
            return new string(contrasena.ToArray());
        }
private readonly string _connectionString = "Server=DESKTOP-FBSC07I;Database=ControlEscolar; Integrated Security = True; TrustServerCertificate = True;";

public Alumno?  ObtenerAlumnoPorCredenciales(string usuario, string contrasenia)
{
    using var connection = new SqlConnection(_connectionString);
    var command = new SqlCommand("SELECT * FROM Alumnos WHERE Usuario = @usuario", connection);
    command.Parameters.AddWithValue("@usuario", usuario);

    connection.Open();
    using var reader = command.ExecuteReader();

    if (reader.Read())
    {
        var hash = reader["Contrasenia"].ToString();
        if (BCrypt.Verify(contrasenia, hash))
        {
            return new Alumno
            {
                IdAlumno = (int)reader["IdAlumno"],
                Nombre = reader["Nombre"].ToString()!,
                ApellidoPaterno = reader["ApellidoPaterno"].ToString()!,
                Usuario = reader["Usuario"].ToString()!,
                Contrasenia = "" 
            };
        }
    }

    return null;
}

    }
}
