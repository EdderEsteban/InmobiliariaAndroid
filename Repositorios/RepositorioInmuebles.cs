using System;
using System.Text;
using Inmobiliaria.Models;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Repositorios;

// Clase RepositorioPropietarios que hereda de RepositorioBD
public class RepositorioInmuebles : InmobiliariaBD.RepositorioBD
{
    public RepositorioInmuebles() { }

    // Método para listar todos los Inmuebles
    public IList<Inmuebles> ListarTodosInmuebles()
    {
        var inmuebles = new List<Inmuebles>();
        try
        {
            // Se obtiene la conexión a la base de datos
            using (var connection = GetConnection())
            {
                // Consulta SQL para seleccionar todos los campos de la tabla propietario
                var sql =
                    $@" SELECT i.Id_inmueble, i.Direccion, i.Uso, i.Id_tipo, ti.Tipo AS TipoInmueble, i.Cantidad_Ambientes, i.Precio_Alquiler, i.Latitud, i.Longitud, i.activo, i.disponible, i.Id_propietario, p.Nombre AS NombrePropietario, p.Apellido AS ApellidoPropietario
            FROM 
                inmueble i
                INNER JOIN tipo_inmueble ti ON i.Id_tipo = ti.Id_tipo
                INNER JOIN propietario p ON i.Id_propietario = p.Id_propietario
                WHERE i.borrado = 0";

                // Creación del comando SQL
                using (var command = new MySqlCommand(sql, connection))
                {
                    // Apertura de la conexión
                    connection.Open();
                    // Ejecución del comando y obtención de un lector de datos
                    using (var reader = command.ExecuteReader())
                    {
                        // Lectura de cada registro y creación de objetos Inmueble
                        while (reader.Read())
                        {
                            // Manejo de los Enum en C#
                            string uso = reader.GetString(nameof(Inmuebles.Uso));
                            UsoInmueble usoEnum;
                            Enum.TryParse(uso, out usoEnum);
                            // Fin Manejo de los Enum en C#

                            inmuebles.Add(
                                new Inmuebles
                                {
                                    Id_inmueble = reader.GetInt32(nameof(Inmuebles.Id_inmueble)),
                                    Direccion = reader.GetString(nameof(Inmuebles.Direccion)),
                                    Uso = usoEnum,
                                    Tipo = new InmuebleTipo
                                    {
                                        Tipo = reader.GetString("TipoInmueble"),
                                    },
                                    Cantidad_Ambientes = reader.GetInt32(
                                        nameof(Inmuebles.Cantidad_Ambientes)
                                    ),
                                    Precio_Alquiler = reader.GetDecimal(
                                        nameof(Inmuebles.Precio_Alquiler)
                                    ),
                                    Latitud = reader.GetString(nameof(Inmuebles.Latitud)),
                                    Longitud = reader.GetString(nameof(Inmuebles.Longitud)),

                                    Propietarios = new Propietarios
                                    {
                                        Nombre = reader.GetString("NombrePropietario"),
                                        Apellido = reader.GetString("ApellidoPropietario"),
                                    },

                                    Activo = reader.GetBoolean(nameof(Inmuebles.Activo)),
                                    Disponible = reader.GetBoolean(nameof(Inmuebles.Disponible)),
                                }
                            );
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Manejo de excepciones y registro del error
            throw new Exception("Error al listar Inmuebles", ex);
        }
        return inmuebles;
    }

    // Método para Listar Inmuebles Activos
    public IList<Inmuebles> ListarInmueblesInactivos()
    {
        var inmuebles = new List<Inmuebles>();
        try
        {
            // Se obtiene la conexión a la base de datos
            using (var connection = GetConnection())
            {
                // Consulta SQL para seleccionar todos los campos de la tabla propietario
                var sql =
                    $@" SELECT i.Id_inmueble, i.Direccion, i.Uso, i.Id_tipo, ti.Tipo AS TipoInmueble, i.Cantidad_Ambientes, i.Precio_Alquiler, i.Latitud, i.Longitud, i.activo, i.disponible, i.Id_propietario, p.Nombre AS NombrePropietario, p.Apellido AS ApellidoPropietario
            FROM 
                inmueble i
                INNER JOIN tipo_inmueble ti ON i.Id_tipo = ti.Id_tipo
                INNER JOIN propietario p ON i.Id_propietario = p.Id_propietario
                WHERE i.activo = 0 AND i.borrado = 0";

                // Creación del comando SQL
                using (var command = new MySqlCommand(sql, connection))
                {
                    // Apertura de la conexión
                    connection.Open();
                    // Ejecución del comando y obtención de un lector de datos
                    using (var reader = command.ExecuteReader())
                    {
                        // Lectura de cada registro y creación de objetos Inmueble
                        while (reader.Read())
                        {
                            // Manejo de los Enum en C#
                            string uso = reader.GetString(nameof(Inmuebles.Uso));
                            UsoInmueble usoEnum;
                            Enum.TryParse(uso, out usoEnum);
                            // Fin Manejo de los Enum en C#

                            inmuebles.Add(
                                new Inmuebles
                                {
                                    Id_inmueble = reader.GetInt32(nameof(Inmuebles.Id_inmueble)),
                                    Direccion = reader.GetString(nameof(Inmuebles.Direccion)),
                                    Uso = usoEnum,
                                    Tipo = new InmuebleTipo
                                    {
                                        Tipo = reader.GetString("TipoInmueble"),
                                    },
                                    Cantidad_Ambientes = reader.GetInt32(
                                        nameof(Inmuebles.Cantidad_Ambientes)
                                    ),
                                    Precio_Alquiler = reader.GetDecimal(
                                        nameof(Inmuebles.Precio_Alquiler)
                                    ),
                                    Latitud = reader.GetString(nameof(Inmuebles.Latitud)),
                                    Longitud = reader.GetString(nameof(Inmuebles.Longitud)),

                                    Propietarios = new Propietarios
                                    {
                                        Nombre = reader.GetString("NombrePropietario"),
                                        Apellido = reader.GetString("ApellidoPropietario"),
                                    },

                                    Activo = reader.GetBoolean(nameof(Inmuebles.Activo)),
                                    Disponible = reader.GetBoolean(nameof(Inmuebles.Disponible)),
                                }
                            );
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Manejo de excepciones y registro del error
            throw new Exception("Error al listar Inmuebles", ex);
        }
        return inmuebles;
    }

    // Método para Listar Inmuebles Disponibles
    public IList<Inmuebles> ListarInmueblesDisponibles()
    {
        var inmuebles = new List<Inmuebles>();
        try
        {
            // Se obtiene la conexión a la base de datos
            using (var connection = GetConnection())
            {
                // Consulta SQL para seleccionar todos los campos de la tabla propietario
                var sql =
                    $@" SELECT i.Id_inmueble, i.Direccion, i.Uso, i.Id_tipo, ti.Tipo AS TipoInmueble, i.Cantidad_Ambientes, i.Precio_Alquiler, i.Latitud, i.Longitud, i.activo, i.disponible, i.Id_propietario, p.Nombre AS NombrePropietario, p.Apellido AS ApellidoPropietario
            FROM 
                inmueble i
                INNER JOIN tipo_inmueble ti ON i.Id_tipo = ti.Id_tipo
                INNER JOIN propietario p ON i.Id_propietario = p.Id_propietario
                 WHERE i.disponible = 1 AND i.activo = 1 AND i.borrado = 0";

                // Creación del comando SQL
                using (var command = new MySqlCommand(sql, connection))
                {
                    // Apertura de la conexión
                    connection.Open();
                    // Ejecución del comando y obtención de un lector de datos
                    using (var reader = command.ExecuteReader())
                    {
                        // Lectura de cada registro y creación de objetos Inmueble
                        while (reader.Read())
                        {
                            // Manejo de los Enum en C#
                            string uso = reader.GetString(nameof(Inmuebles.Uso));
                            UsoInmueble usoEnum;
                            Enum.TryParse(uso, out usoEnum);
                            // Fin Manejo de los Enum en C#

                            inmuebles.Add(
                                new Inmuebles
                                {
                                    Id_inmueble = reader.GetInt32(nameof(Inmuebles.Id_inmueble)),
                                    Direccion = reader.GetString(nameof(Inmuebles.Direccion)),

                                    Uso = usoEnum,
                                    Tipo = new InmuebleTipo
                                    {
                                        Tipo = reader.GetString("TipoInmueble"),
                                    },
                                    Cantidad_Ambientes = reader.GetInt32(
                                        nameof(Inmuebles.Cantidad_Ambientes)
                                    ),
                                    Precio_Alquiler = reader.GetDecimal(
                                        nameof(Inmuebles.Precio_Alquiler)
                                    ),
                                    Latitud = reader.GetString(nameof(Inmuebles.Latitud)),
                                    Longitud = reader.GetString(nameof(Inmuebles.Longitud)),

                                    Propietarios = new Propietarios
                                    {
                                        Nombre = reader.GetString("NombrePropietario"),
                                        Apellido = reader.GetString("ApellidoPropietario"),
                                    },

                                    Activo = reader.GetBoolean(nameof(Inmuebles.Activo)),
                                    Disponible = reader.GetBoolean(nameof(Inmuebles.Disponible)),
                                }
                            );
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Manejo de excepciones y registro del error
            throw new Exception("Error al listar Inmuebles", ex);
        }
        return inmuebles;
    }

    // Método para Listar Inmuebles Alquilados
    public IList<Inmuebles> ListarInmueblesAlquilados()
    {
        var inmuebles = new List<Inmuebles>();
        try
        {
            // Se obtiene la conexión a la base de datos
            using (var connection = GetConnection())
            {
                // Consulta SQL para seleccionar todos los campos de la tabla propietario
                var sql =
                    $@" SELECT i.Id_inmueble, i.Direccion, i.Uso, i.Id_tipo, ti.Tipo AS TipoInmueble, i.Cantidad_Ambientes, i.Precio_Alquiler, i.Latitud, i.Longitud, i.activo, i.disponible, i.Id_propietario, p.Nombre AS NombrePropietario, p.Apellido AS ApellidoPropietario
            FROM 
                inmueble i
                INNER JOIN tipo_inmueble ti ON i.Id_tipo = ti.Id_tipo
                INNER JOIN propietario p ON i.Id_propietario = p.Id_propietario
                WHERE i.disponible = 0 AND i.activo = 1 AND i.borrado = 0";

                // Creación del comando SQL
                using (var command = new MySqlCommand(sql, connection))
                {
                    // Apertura de la conexión
                    connection.Open();
                    // Ejecución del comando y obtención de un lector de datos
                    using (var reader = command.ExecuteReader())
                    {
                        // Lectura de cada registro y creación de objetos Inmueble
                        while (reader.Read())
                        {
                            // Manejo de los Enum en C#
                            string uso = reader.GetString(nameof(Inmuebles.Uso));
                            UsoInmueble usoEnum;
                            Enum.TryParse(uso, out usoEnum);
                            // Fin Manejo de los Enum en C#

                            inmuebles.Add(
                                new Inmuebles
                                {
                                    Id_inmueble = reader.GetInt32(nameof(Inmuebles.Id_inmueble)),
                                    Direccion = reader.GetString(nameof(Inmuebles.Direccion)),
                                    Uso = usoEnum,
                                    Tipo = new InmuebleTipo
                                    {
                                        Tipo = reader.GetString("TipoInmueble"),
                                    },
                                    Cantidad_Ambientes = reader.GetInt32(
                                        nameof(Inmuebles.Cantidad_Ambientes)
                                    ),
                                    Precio_Alquiler = reader.GetDecimal(
                                        nameof(Inmuebles.Precio_Alquiler)
                                    ),
                                    Latitud = reader.GetString(nameof(Inmuebles.Latitud)),
                                    Longitud = reader.GetString(nameof(Inmuebles.Longitud)),

                                    Propietarios = new Propietarios
                                    {
                                        Nombre = reader.GetString("NombrePropietario"),
                                        Apellido = reader.GetString("ApellidoPropietario"),
                                    },

                                    Activo = reader.GetBoolean(nameof(Inmuebles.Activo)),
                                    Disponible = reader.GetBoolean(nameof(Inmuebles.Disponible)),
                                }
                            );
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Manejo de excepciones y registro del error
            throw new Exception("Error al listar Inmuebles", ex);
        }
        return inmuebles;
    }

    // Método para Listar Inmuebles por Propietario
    public IList<Inmuebles> ListarInmueblesPorPropietario(int idPropietario)
    {
        var inmuebles = new List<Inmuebles>();
        try
        {
            // Se obtiene la conexión a la base de datos
            using (var connection = GetConnection())
            {
                // Consulta SQL para seleccionar todos los campos de la tabla propietario
                var sql =
                    @"
                    SELECT 
                        i.Id_inmueble, 
                        i.Direccion, 
                        i.Uso, 
                        i.Id_tipo, 
                        ti.Tipo AS TipoInmueble, 
                        i.Cantidad_Ambientes, 
                        i.Precio_Alquiler, 
                        i.Latitud, 
                        i.Longitud, 
                        i.activo, 
                        i.disponible, 
                        i.Id_propietario, 
                        p.Nombre AS NombrePropietario, 
                        p.Apellido AS ApellidoPropietario
                    FROM 
                        inmueble i
                        INNER JOIN tipo_inmueble ti ON i.Id_tipo = ti.Id_tipo
                        INNER JOIN propietario p ON i.Id_propietario = p.Id_propietario
                    WHERE 
                        i.Id_propietario = @IdPropietario";

                // Creación del comando SQL
                using (var command = new MySqlCommand(sql, connection))
                {
                    // Asignación de los valores
                    command.Parameters.AddWithValue("@IdPropietario", idPropietario);
                    // Apertura de la conexión
                    connection.Open();
                    // Ejecución del comando y obtención de un lector de datos
                    using (var reader = command.ExecuteReader())
                    {
                        // Lectura de cada registro y creación de objetos Inmueble
                        while (reader.Read())
                        {
                            // Manejo de los Enum en C#
                            string uso = reader.GetString(nameof(Inmuebles.Uso));
                            UsoInmueble usoEnum;
                            Enum.TryParse(uso, out usoEnum);
                            // Fin Manejo de los Enum en C#

                            inmuebles.Add(
                                new Inmuebles
                                {
                                    Id_inmueble = reader.GetInt32(nameof(Inmuebles.Id_inmueble)),
                                    Direccion = reader.GetString(nameof(Inmuebles.Direccion)),
                                    Uso = usoEnum,
                                    Tipo = new InmuebleTipo
                                    {
                                        Tipo = reader.GetString("TipoInmueble"),
                                    },
                                    Cantidad_Ambientes = reader.GetInt32(
                                        nameof(Inmuebles.Cantidad_Ambientes)
                                    ),
                                    Precio_Alquiler = reader.GetDecimal(
                                        nameof(Inmuebles.Precio_Alquiler)
                                    ),
                                    Latitud = reader.GetString(nameof(Inmuebles.Latitud)),
                                    Longitud = reader.GetString(nameof(Inmuebles.Longitud)),

                                    Propietarios = new Propietarios
                                    {
                                        Nombre = reader.GetString("NombrePropietario"),
                                        Apellido = reader.GetString("ApellidoPropietario"),
                                    },

                                    Activo = reader.GetBoolean(nameof(Inmuebles.Activo)),
                                    Disponible = reader.GetBoolean(nameof(Inmuebles.Disponible)),
                                }
                            );
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Manejo de excepciones y registro del error
            throw new Exception("Error al listar Inmuebles", ex);
        }
        return inmuebles;
    }

    // Metodo para Listar Tipos Inmueble
    public IList<InmuebleTipo> ListarTiposInmueble()
    {
        var listado = new List<InmuebleTipo>();
        try
        {
            using (var connection = GetConnection())
            {
                var sql = "SELECT * FROM tipo_inmueble;";
                using (var command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listado.Add(
                                new InmuebleTipo
                                {
                                    Id_tipo = reader.GetInt32("Id_Tipo"),
                                    Tipo = reader.GetString("Tipo")
                                }
                            );
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Manejo de excepciones y registro del error
            throw new Exception("Error al listar Inmuebles", ex);
        }

        return listado;
    }

    // Metodo para guardar un tipo inmueble
    public int GuardarTipoInmueble(InmuebleTipo tipo)
    {
        int Id = 0;
        using (var connection = GetConnection())
        {
            var sql = "INSERT INTO tipo_inmueble (Tipo) VALUES (@Tipo);";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Tipo", tipo.Tipo);
                connection.Open();
                Id = command.ExecuteNonQuery();
            }
        }
        return Id;
    }

    // Metodo para borrar un tipo inmueble
    public int BorrarTipoInmueble(int id)
        {
            using (var connection = GetConnection())
            {
                var sql =
                    @$"DELETE FROM tipo_inmueble 
                WHERE {nameof(InmuebleTipo.Id_tipo)} = @{nameof(InmuebleTipo.Id_tipo)}";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue($"@{nameof(InmuebleTipo.Id_tipo)}", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return 0;
        }

    // Método para guardar un nuevo inmueble
    public int GuardarNuevo(Inmuebles inmueble)
    {
        int Id = 0;
        using (var connection = GetConnection())
        {
            var sql =
                @$"INSERT INTO inmueble ({nameof(Inmuebles.Direccion)}, {nameof(Inmuebles.Uso)}, 
                {nameof(Inmuebles.Id_tipo)}, {nameof(Inmuebles.Cantidad_Ambientes)}, {nameof(Inmuebles.Precio_Alquiler)}, {nameof(Inmuebles.Latitud)}, {nameof(Inmuebles.Longitud)}, {nameof(Inmuebles.Activo)}, {nameof(Inmuebles.Disponible)}, {nameof(Inmuebles.Id_propietario)})
                VALUES (@{nameof(Inmuebles.Direccion)}, @{nameof(Inmuebles.Uso)}, @{nameof(Inmuebles.Id_tipo)},
                 @{nameof(Inmuebles.Cantidad_Ambientes)}, @{nameof(Inmuebles.Precio_Alquiler)},
                @{nameof(Inmuebles.Latitud)}, @{nameof(Inmuebles.Longitud)}, @{nameof(Inmuebles.Activo)}, 
                @{nameof(Inmuebles.Disponible)}, @{nameof(Inmuebles.Id_propietario)});
                SELECT LAST_INSERT_ID();";

            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue(
                    $"@{nameof(Inmuebles.Direccion)}",
                    inmueble.Direccion
                );
                command.Parameters.AddWithValue($"@{nameof(Inmuebles.Uso)}", inmueble.Uso);
                command.Parameters.AddWithValue($"@{nameof(Inmuebles.Id_tipo)}", inmueble.Id_tipo);
                command.Parameters.AddWithValue(
                    $"@{nameof(Inmuebles.Cantidad_Ambientes)}",
                    inmueble.Cantidad_Ambientes
                );
                command.Parameters.AddWithValue(
                    $"@{nameof(Inmuebles.Precio_Alquiler)}",
                    inmueble.Precio_Alquiler
                );
                command.Parameters.AddWithValue($"@{nameof(Inmuebles.Latitud)}", inmueble.Latitud);
                command.Parameters.AddWithValue(
                    $"@{nameof(Inmuebles.Longitud)}",
                    inmueble.Longitud
                );
                command.Parameters.AddWithValue($"@{nameof(Inmuebles.Activo)}", inmueble.Activo);
                command.Parameters.AddWithValue(
                    $"@{nameof(Inmuebles.Disponible)}",
                    inmueble.Disponible
                );
                command.Parameters.AddWithValue(
                    $"@{nameof(Inmuebles.Id_propietario)}",
                    inmueble.Id_propietario
                );

                connection.Open();

                Id = Convert.ToInt32(command.ExecuteScalar());
                inmueble.Id_inmueble = Id;
                connection.Close();
            }
        }
        return Id;
    }

    // Método para obtener un inmueble por su ID
    public Inmuebles? ObtenerInmueble(int id)
    {
        Inmuebles? inmueble = null;
        try
        {
            using (var connection = GetConnection())
            {
                var sql =
                    @" SELECT i.Id_inmueble, i.Direccion, i.Uso, i.Id_tipo, ti.Tipo AS TipoInmueble, i.Cantidad_Ambientes,
                i.Precio_Alquiler, i.Latitud, i.Longitud, i.activo, i.disponible, i.Id_propietario, p.Nombre AS NombrePropietario, p.Apellido AS ApellidoPropietario
            FROM 
                inmueble i
                INNER JOIN tipo_inmueble ti ON i.Id_tipo = ti.Id_tipo
                INNER JOIN propietario p ON i.Id_propietario = p.Id_propietario
            WHERE i.id_inmueble = @id;";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //Manejo de los Enum en C#
                            string uso = reader.GetString(nameof(Inmuebles.Uso));
                            UsoInmueble usoEnum;
                            Enum.TryParse(uso, out usoEnum);
                            //Fin Manejo de los Enum en C#

                            inmueble = new Inmuebles
                            {
                                Id_inmueble = reader.GetInt32(nameof(Inmuebles.Id_inmueble)),
                                Direccion = reader.GetString(nameof(Inmuebles.Direccion)),
                                Uso = usoEnum,
                                Tipo = new InmuebleTipo
                                {
                                    Tipo = reader.GetString("TipoInmueble"),
                                },
                                Cantidad_Ambientes = reader.GetInt32(
                                    nameof(Inmuebles.Cantidad_Ambientes)
                                ),
                                Precio_Alquiler = reader.GetDecimal(
                                    nameof(Inmuebles.Precio_Alquiler)
                                ),
                                Latitud = reader.GetString(nameof(Inmuebles.Latitud)),
                                Longitud = reader.GetString(nameof(Inmuebles.Longitud)),
                                Id_propietario = reader.GetInt32(nameof(Inmuebles.Id_propietario)),
                                Propietarios = new Propietarios
                                {
                                    Nombre = reader.GetString("NombrePropietario"),
                                    Apellido = reader.GetString("ApellidoPropietario"),
                                },
                                Activo = reader.GetBoolean(nameof(Inmuebles.Activo)),
                                Disponible = reader.GetBoolean(nameof(Inmuebles.Disponible)),
                            };
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Maneja cualquier excepción que ocurra durante la ejecución del código y lanza una nueva excepción con un mensaje descriptivo.
            throw new Exception("Error al obtener el inmueble", ex);
        }

        // Devuelve el objeto 'inmueble' si se encontró, o null si no.
        return inmueble;
    }

    // Método para obtener la lista de inmuebles por su dirección
    public List<Inmuebles> ObtenerInmueblesPorDireccion(string direccion)
    {
        if (string.IsNullOrEmpty(direccion))
        {
            // Retornar una lista vacía si no se proporciona una dirección válida
            return new List<Inmuebles>();
        }
        var inmuebles = new List<Inmuebles>();
        try
        {
            using (var connection = GetConnection())
            {
                var sql =
                    @"
                SELECT 
                    i.Id_inmueble, i.Direccion, i.Uso, i.Id_tipo, ti.Tipo AS TipoInmueble, 
                    i.Cantidad_Ambientes, i.Precio_Alquiler, i.Latitud, i.Longitud, 
                    i.activo, i.disponible, i.Id_propietario, 
                    p.Nombre AS NombrePropietario, p.Apellido AS ApellidoPropietario
                FROM 
                    inmueble i
                INNER JOIN tipo_inmueble ti ON i.Id_tipo = ti.Id_tipo
                INNER JOIN propietario p ON i.Id_propietario = p.Id_propietario
                WHERE i.Direccion LIKE @direccion;"; // Búsqueda por dirección

                using (var command = new MySqlCommand(sql, connection))
                {
                    // Parámetro de búsqueda con comodín para direcciones que contienen el texto ingresado
                    command.Parameters.AddWithValue("@direccion", direccion + "%");
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read()) // Iterar sobre los resultados
                        {
                            string uso = reader.GetString(nameof(Inmuebles.Uso));
                            UsoInmueble usoEnum;
                            Enum.TryParse(uso, out usoEnum); // Manejo de enums

                            var inmueble = new Inmuebles
                            {
                                Id_inmueble = reader.GetInt32(nameof(Inmuebles.Id_inmueble)),
                                Direccion = reader.GetString(nameof(Inmuebles.Direccion)),
                                Uso = usoEnum,
                                Tipo = new InmuebleTipo
                                {
                                    Tipo = reader.GetString("TipoInmueble"),
                                },
                                Cantidad_Ambientes = reader.GetInt32(
                                    nameof(Inmuebles.Cantidad_Ambientes)
                                ),
                                Precio_Alquiler = reader.GetDecimal(
                                    nameof(Inmuebles.Precio_Alquiler)
                                ),
                                Latitud = reader.GetString(nameof(Inmuebles.Latitud)),
                                Longitud = reader.GetString(nameof(Inmuebles.Longitud)),
                                Id_propietario = reader.GetInt32(nameof(Inmuebles.Id_propietario)),
                                Propietarios = new Propietarios
                                {
                                    Nombre = reader.GetString("NombrePropietario"),
                                    Apellido = reader.GetString("ApellidoPropietario"),
                                },
                                Activo = reader.GetBoolean(nameof(Inmuebles.Activo)),
                                Disponible = reader.GetBoolean(nameof(Inmuebles.Disponible)),
                            };

                            // Agregar el inmueble a la lista
                            inmuebles.Add(inmueble);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción
            throw new Exception("Error al obtener los inmuebles por dirección", ex);
        }

        // Retornar la lista de inmuebles encontrados
        return inmuebles;
    }

    // Método para actualizar los datos de un inmueble
    public void ActualizarInmueble(Inmuebles inmueble)
    {
        using (var connection = GetConnection())
            try
            {
                var sql =
                    @$"UPDATE Inmueble SET
                            {nameof(Inmuebles.Direccion)} = @{nameof(Inmuebles.Direccion)},
                            {nameof(Inmuebles.Uso)} = @{nameof(Inmuebles.Uso)},
                            {nameof(Inmuebles.Id_tipo)} = @{nameof(Inmuebles.Id_tipo)},
                            {nameof(Inmuebles.Cantidad_Ambientes)} = @{nameof(Inmuebles.Cantidad_Ambientes)},
                            {nameof(Inmuebles.Precio_Alquiler)} = @{nameof(Inmuebles.Precio_Alquiler)},
                            {nameof(Inmuebles.Latitud)} = @{nameof(Inmuebles.Latitud)},
                            {nameof(Inmuebles.Longitud)} = @{nameof(Inmuebles.Longitud)},
                            {nameof(Inmuebles.Activo)} = @{nameof(Inmuebles.Activo)},
                            {nameof(Inmuebles.Disponible)} = @{nameof(Inmuebles.Disponible)},
                            {nameof(Inmuebles.Id_propietario)} = @{nameof(Inmuebles.Id_propietario)}
                        WHERE {nameof(Inmuebles.Id_inmueble)} = @{nameof(Inmuebles.Id_inmueble)}";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmuebles.Id_inmueble)}",
                        inmueble.Id_inmueble
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmuebles.Direccion)}",
                        inmueble.Direccion
                    );
                    command.Parameters.AddWithValue($"@{nameof(Inmuebles.Uso)}", inmueble.Uso);
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmuebles.Id_tipo)}",
                        inmueble.Id_tipo
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmuebles.Cantidad_Ambientes)}",
                        inmueble.Cantidad_Ambientes
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmuebles.Precio_Alquiler)}",
                        inmueble.Precio_Alquiler
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmuebles.Latitud)}",
                        inmueble.Latitud
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmuebles.Longitud)}",
                        inmueble.Longitud
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmuebles.Activo)}",
                        inmueble.Activo
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmuebles.Disponible)}",
                        inmueble.Disponible
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmuebles.Id_propietario)}",
                        inmueble.Id_propietario
                    );

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones y registro del error
                throw new Exception("Error al actualizar el propietario", ex);
            }
    }

    // Método para actualizar los datos de un inmueble sin contrato de alquiler
    public void ActualizarInmuebleExceptoDisponible(Inmuebles inmueble)
    {
        try
        {
            using (var connection = GetConnection())
            {
                var sql =
                    @$"UPDATE Inmueble SET
                            {nameof(Inmuebles.Direccion)} = @{nameof(Inmuebles.Direccion)},
                            {nameof(Inmuebles.Uso)} = @{nameof(Inmuebles.Uso)},
                            {nameof(Inmuebles.Id_tipo)} = @{nameof(Inmuebles.Id_tipo)},
                            {nameof(Inmuebles.Cantidad_Ambientes)} = @{nameof(Inmuebles.Cantidad_Ambientes)},
                            {nameof(Inmuebles.Precio_Alquiler)} = @{nameof(Inmuebles.Precio_Alquiler)},
                            {nameof(Inmuebles.Latitud)} = @{nameof(Inmuebles.Latitud)},
                            {nameof(Inmuebles.Longitud)} = @{nameof(Inmuebles.Longitud)},
                            {nameof(Inmuebles.Activo)} = @{nameof(Inmuebles.Activo)},
                             {nameof(Inmuebles.Disponible)} = @{nameof(Inmuebles.Disponible)},
                            {nameof(Inmuebles.Id_propietario)} = @{nameof(Inmuebles.Id_propietario)}
                        WHERE {nameof(Inmuebles.Id_inmueble)} = @{nameof(Inmuebles.Id_inmueble)}";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmuebles.Id_inmueble)}",
                        inmueble.Id_inmueble
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmuebles.Direccion)}",
                        inmueble.Direccion
                    );
                    command.Parameters.AddWithValue($"@{nameof(Inmuebles.Uso)}", inmueble.Uso);
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmuebles.Id_tipo)}",
                        inmueble.Id_tipo
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmuebles.Cantidad_Ambientes)}",
                        inmueble.Cantidad_Ambientes
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmuebles.Precio_Alquiler)}",
                        inmueble.Precio_Alquiler
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmuebles.Latitud)}",
                        inmueble.Latitud
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmuebles.Longitud)}",
                        inmueble.Longitud
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmuebles.Activo)}",
                        inmueble.Activo
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmuebles.Disponible)}",
                        inmueble.Disponible
                    );
                    command.Parameters.AddWithValue(
                        $"@{nameof(Inmuebles.Id_propietario)}",
                        inmueble.Id_propietario
                    );

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            // Manejo de excepciones y registro del error
            throw new Exception("Error al actualizar el inmueble", ex);
        }
    }

    // Cambiar Disponibilidad de Inmueble
    public int CambiarEstadoInmueble(int id)
    {
        try
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                var sql =
                    $"UPDATE inmueble SET {nameof(Inmuebles.Disponible)} = 0 WHERE {nameof(Inmuebles.Id_inmueble)} = @{nameof(Inmuebles.Id_inmueble)}";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue($"@{nameof(Inmuebles.Id_inmueble)}", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            // Manejo de excepciones y registro del error
            throw new Exception("Error al cambiar el estado del inmueble", ex);
        }
        return 0;
    }

    // Método para Eliminar Inmueble
    public int EliminarInmueble(int id)
    {
        var resultado = 0;
        try
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                string sql =
                    @$"UPDATE Inmueble SET
                            {nameof(Inmuebles.Borrado)} = @Borrado
                            WHERE {nameof(Inmuebles.Id_inmueble)} = @Id_inmueble";

                using (var command = new MySqlCommand(sql, connection))
                {
                    // Aquí se establece Borrado a true para indicar que el inmueble está borrado
                    command.Parameters.AddWithValue("@Id_inmueble", id);
                    command.Parameters.AddWithValue("@Borrado", true); // Marca como borrado

                    connection.Open();
                    resultado = command.ExecuteNonQuery(); // Devuelve el número de filas afectadas
                }
            }
        }
        catch (Exception ex)
        {
            // Manejo de excepciones y registro del error
            throw new Exception("Error al eliminar el inmueble", ex);
        }
        return resultado;
    }
}
