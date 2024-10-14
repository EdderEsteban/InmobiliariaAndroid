using System;
using System.Text;
using Inmobiliaria.Models;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Repositorios;

// Clase RepositorioPropietarios que hereda de RepositorioBD
public class RepositorioInquilinos : InmobiliariaBD.RepositorioBD
{
    // Método para listar todos los Inquilinos
    public IList<Inquilinos> ListarInquilinos()
    {
        var inquilinos = new List<Inquilinos>();
        using (var connection = GetConnection())
        {
            var sql =
                @$"Select {nameof(Inquilinos.Id_inquilino)}, {nameof(Inquilinos.Nombre)}, {nameof(Inquilinos.Apellido)}, 
                {nameof(Inquilinos.Dni)}, {nameof(Inquilinos.Direccion)}, {nameof(Inquilinos.Telefono)},{nameof(Inquilinos.Correo)} FROM inquilino";

            using (var comand = new MySqlCommand(sql, connection))
            {
                connection.Open();
                using (var reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        inquilinos.Add(
                            new Inquilinos
                            {
                                Id_inquilino = reader.GetInt32(nameof(Inquilinos.Id_inquilino)),
                                Nombre = reader.GetString(nameof(Inquilinos.Nombre)),
                                Apellido = reader.GetString(nameof(Inquilinos.Apellido)),
                                Dni = reader.GetString(nameof(Inquilinos.Dni)),
                                Direccion = reader.GetString(nameof(Inquilinos.Direccion)),
                                Telefono = reader.GetString(nameof(Inquilinos.Telefono)),
                                Correo = reader.GetString(nameof(Inquilinos.Correo))
                            }
                        );
                    }
                    connection.Close();
                }
            }
        }
        return inquilinos;
    }

    // [Guardar]
    public int GuardarNuevo(Inquilinos inquilino)
    {
        int Id = 0;
        using (var connection = GetConnection())
        {
            var sql =
            @$"INSERT INTO inquilino 
              ({nameof(Inquilinos.Nombre)}, {nameof(Inquilinos.Apellido)}, {nameof(Inquilinos.Dni)}, 
               {nameof(Inquilinos.Direccion)}, {nameof(Inquilinos.Telefono)}, {nameof(Inquilinos.Correo)}, {nameof(Inquilinos.Id_usuario)})
              VALUES 
              (@{nameof(Inquilinos.Nombre)}, @{nameof(Inquilinos.Apellido)}, @{nameof(Inquilinos.Dni)}, 
               @{nameof(Inquilinos.Direccion)}, @{nameof(Inquilinos.Telefono)}, @{nameof(Inquilinos.Correo)}, @{nameof(Inquilinos.Id_usuario)});
              SELECT LAST_INSERT_ID();";

            using (var comand = new MySqlCommand(sql, connection))
            {
                // Asignar valores a los parámetros
                comand.Parameters.AddWithValue($"@{nameof(Inquilinos.Nombre)}", inquilino.Nombre);
                comand.Parameters.AddWithValue($"@{nameof(Inquilinos.Apellido)}", inquilino.Apellido);
                comand.Parameters.AddWithValue($"@{nameof(Inquilinos.Dni)}", inquilino.Dni);
                comand.Parameters.AddWithValue($"@{nameof(Inquilinos.Direccion)}", inquilino.Direccion);
                comand.Parameters.AddWithValue($"@{nameof(Inquilinos.Telefono)}", inquilino.Telefono);
                comand.Parameters.AddWithValue($"@{nameof(Inquilinos.Correo)}", inquilino.Correo);
                comand.Parameters.AddWithValue($"@{nameof(Inquilinos.Id_usuario)}", inquilino.Id_usuario);

                connection.Open();

                Id = Convert.ToInt32(comand.ExecuteScalar());
                inquilino.Id_inquilino = Id;
                connection.Close();
            }
        }
        return Id;
    }

    // [Obtener Inquilino]
    public Inquilinos? ObtenerInquilino(int id)
    {
        Inquilinos? inquilino = null;
        using (var connection = GetConnection())
        {
            var sql =
                @$"Select {nameof(Inquilinos.Id_inquilino)}, {nameof(Inquilinos.Nombre)}, {nameof(Inquilinos.Apellido)}, 
                {nameof(Inquilinos.Dni)}, {nameof(Inquilinos.Direccion)}, {nameof(Inquilinos.Telefono)},{nameof(Inquilinos.Correo)} 
                FROM inquilino
                WHERE {nameof(Inquilinos.Id_inquilino)} = @{nameof(Inquilinos.Id_inquilino)}";

            using (var comand = new MySqlCommand(sql, connection))
            {
                comand.Parameters.AddWithValue($"@{nameof(Inquilinos.Id_inquilino)}", id);
                connection.Open();
                using (var reader = comand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        inquilino = new Inquilinos
                        {
                            Id_inquilino = reader.GetInt32(nameof(Inquilinos.Id_inquilino)),
                            Nombre = reader.GetString(nameof(Inquilinos.Nombre)),
                            Apellido = reader.GetString(nameof(Inquilinos.Apellido)),
                            Dni = reader.GetString(nameof(Inquilinos.Dni)),
                            Direccion = reader.GetString(nameof(Inquilinos.Direccion)),
                            Telefono = reader.GetString(nameof(Inquilinos.Telefono)),
                            Correo = reader.GetString(nameof(Inquilinos.Correo))
                        };
                    }
                    connection.Close();
                }
            }
        }
        return inquilino;
    }

    // [Actualizar Inquilino] 
    public void ActualizarInquilino(Inquilinos inquilino)
    {
        using (var connection = GetConnection())
        {
            var sql =
                @$"UPDATE inquilino SET
                {nameof(Inquilinos.Nombre)} = @{nameof(Inquilinos.Nombre)},
                {nameof(Inquilinos.Apellido)} = @{nameof(Inquilinos.Apellido)},
                {nameof(Inquilinos.Dni)} = @{nameof(Inquilinos.Dni)},
                {nameof(Inquilinos.Direccion)} = @{nameof(Inquilinos.Direccion)},
                {nameof(Inquilinos.Telefono)} = @{nameof(Inquilinos.Telefono)},
                {nameof(Inquilinos.Correo)} = @{nameof(Inquilinos.Correo)}
            WHERE {nameof(Inquilinos.Id_inquilino)} = @{nameof(Inquilinos.Id_inquilino)}";


            using (var command = new MySqlCommand(sql, connection))
            {
                // Agregar parámetros de forma segura
                command.Parameters.AddWithValue($"@{nameof(Inquilinos.Nombre)}", inquilino.Nombre);
                command.Parameters.AddWithValue($"@{nameof(Inquilinos.Apellido)}", inquilino.Apellido);
                command.Parameters.AddWithValue($"@{nameof(Inquilinos.Dni)}", inquilino.Dni);
                command.Parameters.AddWithValue($"@{nameof(Inquilinos.Direccion)}", inquilino.Direccion);
                command.Parameters.AddWithValue($"@{nameof(Inquilinos.Telefono)}", inquilino.Telefono);
                command.Parameters.AddWithValue($"@{nameof(Inquilinos.Correo)}", inquilino.Correo);
                command.Parameters.AddWithValue($"@{nameof(Inquilinos.Id_inquilino)}", inquilino.Id_inquilino);

                connection.Open();
                command.ExecuteNonQuery();
                // La conexión se cerrará automáticamente al salir del 'using'
            }
        }
    }


    // Método para eliminar un inquilino por su ID
    // [Eliminar Inquilino]
    public int EliminarInquilino(int id)
    {
        using (var connection = GetConnection())
        {
            var sql =
                @$"DELETE FROM inquilino 
                WHERE {nameof(Inquilinos.Id_inquilino)} = @{nameof(Inquilinos.Id_inquilino)}";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue($"@{nameof(Inquilinos.Id_inquilino)}", id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return 0;
    }


    // Método para buscar propietarios
    public IList<Inquilinos> BuscarInquilinos(BusquedaInquilinos busqueda)
    {
        var inquilinos = new List<Inquilinos>();
        try
        {
            using (var connection = GetConnection())
            {
                var sql =
                    @$"SELECT {nameof(Inquilinos.Id_inquilino)}, {nameof(Inquilinos.Nombre)}, {nameof(Inquilinos.Apellido)},
                    {nameof(Inquilinos.Dni)}, {nameof(Inquilinos.Direccion)}, {nameof(Inquilinos.Telefono)}, {nameof(Inquilinos.Correo)}
                    FROM inquilino
                    WHERE (@Nombre IS NULL OR {nameof(Inquilinos.Nombre)} LIKE CONCAT('%', @Nombre, '%'))
                    AND (@Apellido IS NULL OR {nameof(Inquilinos.Apellido)} LIKE CONCAT('%', @Apellido, '%'))
                    AND (@Dni IS NULL OR {nameof(Inquilinos.Dni)} LIKE CONCAT('%', @Dni, '%'))";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue(
                        "@Nombre",
                        string.IsNullOrEmpty(busqueda.Nombre)
                            ? (object)DBNull.Value
                            : busqueda.Nombre
                    );
                    command.Parameters.AddWithValue(
                        "@Apellido",
                        string.IsNullOrEmpty(busqueda.Apellido)
                            ? (object)DBNull.Value
                            : busqueda.Apellido
                    );
                    command.Parameters.AddWithValue(
                        "@Dni",
                        string.IsNullOrEmpty(busqueda.Dni) ? (object)DBNull.Value : busqueda.Dni
                    );

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            inquilinos.Add(
                                new Inquilinos
                                {
                                    Id_inquilino = reader.GetInt32(
                                        nameof(Inquilinos.Id_inquilino)
                                    ),
                                    Nombre = reader.GetString(nameof(Inquilinos.Nombre)),
                                    Apellido = reader.GetString(nameof(Inquilinos.Apellido)),
                                    Dni = reader.GetString(nameof(Inquilinos.Dni)),
                                    Direccion = reader.GetString(nameof(Inquilinos.Direccion)),
                                    Telefono = reader.GetString(nameof(Inquilinos.Telefono)),
                                    Correo = reader.GetString(nameof(Inquilinos.Correo))
                                }
                            );
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error al buscar propietarios", ex);
        }
        return inquilinos;
    }
}
