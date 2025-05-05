
namespace ControlEscolar.Data
{
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using ControlEscolar.Core.Models;
using Microsoft.Extensions.Configuration;


    public class AlumnoRepository
    {
        private readonly string _connectionString;

        public AlumnoRepository(IConfiguration configuration)
        {
            var tempConnectionString = configuration.GetConnectionString("ControlEscolar");
            if (string.IsNullOrWhiteSpace(tempConnectionString))
            {
                throw new InvalidOperationException("La cadena de conexión 'ControlEscolar' no está configurada.");
            }
            _connectionString = tempConnectionString;
        }

        public void InsertarAlumno(Alumno alumno)
{
    try
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("sp_InsertarAlumno", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@nombre", alumno.Nombre);
        command.Parameters.AddWithValue("@apellidoPaterno", alumno.ApellidoPaterno);
        command.Parameters.AddWithValue("@apellidoMaterno", alumno.ApellidoMaterno ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@usuario", alumno.Usuario);
        command.Parameters.AddWithValue("@contrasenia", alumno.Contrasenia);

        connection.Open();
        command.ExecuteNonQuery();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error al insertar alumno: " + ex.Message);
        throw; 
    }
}

public void EliminarAlumno(int idAlumno)
{
    using var connection = new SqlConnection(_connectionString);
    using var command = new SqlCommand("DELETE FROM Alumnos WHERE idAlumno = @id", connection);
    command.Parameters.AddWithValue("@id", idAlumno);

    connection.Open();
    command.ExecuteNonQuery();
}
public void ActualizarAlumno(int id, string nombre, string apellidoP, string apellidoM)
{
    using var connection = new SqlConnection(_connectionString);
    using var command = new SqlCommand("UPDATE Alumnos SET nombre = @nombre, apellidoPaterno = @apellidoP, apellidoMaterno = @apellidoM WHERE idAlumno = @id", connection);
    command.Parameters.AddWithValue("@id", id);
    command.Parameters.AddWithValue("@nombre", nombre);
    command.Parameters.AddWithValue("@apellidoP", apellidoP);
    command.Parameters.AddWithValue("@apellidoM", apellidoM ?? (object)DBNull.Value);

    connection.Open();
    command.ExecuteNonQuery();
}
        public List<Alumno> ObtenerAlumnos()
{
    var alumnos = new List<Alumno>();

    using var connection = new SqlConnection(_connectionString);
    using var command = new SqlCommand("SELECT idAlumno, nombre, apellidoPaterno, apellidoMaterno, usuario FROM Alumnos", connection);
    connection.Open();
    using var reader = command.ExecuteReader();

    while (reader.Read())
    {
        alumnos.Add(new Alumno
        {
            IdAlumno = reader.GetInt32(0),
            Nombre = reader.GetString(1),
            ApellidoPaterno = reader.GetString(2),
            ApellidoMaterno = reader.IsDBNull(3) ? "" : reader.GetString(3),
            Usuario = reader.GetString(4),
            Contrasenia = "" 
        });
    }

    return alumnos;
}


public List<Alumno> BuscarAlumNombre(string nombre)
{
    var alumnos = new List<Alumno>();

    using var connection = new SqlConnection(_connectionString);
    using var command = new SqlCommand("sp_BuscarAlumNombre", connection);
    command.CommandType = CommandType.StoredProcedure;
    command.Parameters.AddWithValue("@nombre", nombre);

    connection.Open();
    using var reader = command.ExecuteReader();

    while (reader.Read())
    {
        alumnos.Add(new Alumno
        {
            IdAlumno = reader.GetInt32(0),
            Nombre = reader.GetString(1),
            ApellidoPaterno = reader.GetString(2),
            ApellidoMaterno = reader.IsDBNull(3) ? "" : reader.GetString(3),
            Usuario = reader.GetString(4),
            Contrasenia = ""
        });
    }

    return alumnos;
}

public decimal ObtenerCostoTotalPorAlumno(int idAlumno)
{
    using var connection = new SqlConnection(_connectionString);
    using var command = new SqlCommand("sp_CostoTotalAlumno", connection);
    command.CommandType = CommandType.StoredProcedure;

    command.Parameters.AddWithValue("@idAlumno", idAlumno);

    connection.Open();
    var result = command.ExecuteScalar();

    return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
}


public void AgregarMateriaAlumno(int idAlumno, int idMateria)
{
    using (SqlConnection conn = new SqlConnection(_connectionString))
    {
        SqlCommand cmd = new SqlCommand("sp_AgregarMateriaAlumno", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@IdAlumno", idAlumno);
        cmd.Parameters.AddWithValue("@IdMateria", idMateria);
        conn.Open();
        cmd.ExecuteNonQuery();
    }
}

    }

}
