using System.Data;
using Microsoft.Data.SqlClient;
using ControlEscolar.Core.Models;
using Microsoft.Extensions.Configuration;

namespace ControlEscolar.Data
{
    public class MateriaRepository
    {
        private readonly string _connectionString;

        public MateriaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ControlEscolar")!;
        }

        public void InsertarMateria(Materia materia)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sp_InsertarMateria", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@nombre", materia.Nombre);
            command.Parameters.AddWithValue("@costo", materia.Costo);

            connection.Open();
            command.ExecuteNonQuery();
        }

public List<Materia> ObtenerMaterias()
{
    var materias = new List<Materia>();

    using var connection = new SqlConnection(_connectionString);
    var command = new SqlCommand("SELECT * FROM Materias", connection);

    connection.Open();
    using var reader = command.ExecuteReader();
    while (reader.Read())
    {
        materias.Add(new Materia
        {
            IdMateria = (int)reader["IdMateria"],
            Nombre = reader["Nombre"].ToString()!,
            Costo = Convert.ToDecimal(reader["Costo"])
        });
    }

    return materias;
}



        public Materia? BuscarPorNombre(string nombre)
{
    using var connection = new SqlConnection(_connectionString);
    using var command = new SqlCommand("SELECT idMateria, nombre, costo FROM Materias WHERE nombre = @nombre", connection);
    command.Parameters.AddWithValue("@nombre", nombre);

    connection.Open();
    using var reader = command.ExecuteReader();
    if (reader.Read())
    {
        return new Materia
        {
            IdMateria = reader.GetInt32(0),
            Nombre = reader.GetString(1),
            Costo = reader.GetDecimal(2)
        };
    }

    return null!;
}


public void EliminarMateria(int id)
{
    using var connection = new SqlConnection(_connectionString);
    using var command = new SqlCommand("DELETE FROM Materias WHERE idMateria = @id", connection);
    command.Parameters.AddWithValue("@id", id);
    connection.Open();
    command.ExecuteNonQuery();
}

public IEnumerable<Materia> ObtenerMateriasDeAlumno(int idAlumno)
{
    var materias = new List<Materia>();

    using (SqlConnection conn = new SqlConnection(_connectionString))
    {
        SqlCommand cmd = new SqlCommand("sp_AgregarMateriaAlumno", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@IdAlumno", idAlumno);

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            materias.Add(new Materia
            {
                IdMateria = Convert.ToInt32(reader["IdMateria"]),
                Nombre = reader["Nombre"]?.ToString() ?? string.Empty,
                Costo = Convert.ToDecimal(reader["Costo"])
            });
        }
    }

    return materias;
}

public decimal ObtenerCostoTotal(int idAlumno)
{
    decimal total = 0;

    using (SqlConnection conn = new SqlConnection(_connectionString))
    {
        SqlCommand cmd = new SqlCommand("sp_CostoTotalAlumno", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@IdAlumno", idAlumno);

        conn.Open();
        object result = cmd.ExecuteScalar();
        if (result != DBNull.Value)
            total = Convert.ToDecimal(result);
    }

    return total;
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
