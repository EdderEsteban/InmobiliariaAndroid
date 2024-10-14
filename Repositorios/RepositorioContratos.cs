using System;
using System.Text;
using Inmobiliaria.Models;
using MySql.Data.MySqlClient;


namespace Inmobiliaria.Repositorios;

// Clase RepositorioPropietarios que hereda de RepositorioBD
public class RepositorioContratos : InmobiliariaBD.RepositorioBD
{
    public RepositorioContratos() { }

    public IList<Contrato> ListarContratos()
    {
        var contratos = new List<Contrato>();
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var sql =
                @"SELECT id_contrato, id_inquilino, id_inmueble, monto, fecha_inicio, fecha_fin, vigencia 
                FROM contrato";
            using (var command = new MySqlCommand(sql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var contrato = new Contrato
                        {
                            Id_contrato = reader.GetInt32("id_contrato"),
                            Id_inquilino = reader.GetInt32("id_inquilino"),
                            Id_inmueble = reader.GetInt32("id_inmueble"),
                            Monto = reader.GetInt32("monto"),
                            Fecha_inicio = reader.GetDateTime("fecha_inicio"),
                            Fecha_fin = reader.GetDateTime("fecha_fin"),
                            Vigencia = reader.GetBoolean("vigencia")
                        };

                        if (contrato.Fecha_fin < DateTime.Now)
                        {
                            contrato.Vigencia = false;
                            ActualizarContrato(contrato);
                        }
                        else
                        {
                            contrato.Vigencia = true;
                            ActualizarContrato(contrato);
                        }
                        contratos.Add(contrato);
                    }
                }
                connection.Close();
            }
        }
        return contratos;
    }

    // Listar Contratos Vigentes
    public IList<Contrato> ListarContratosVigentes()
    {
        var contratos = new List<Contrato>();
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var sql =
                @"SELECT id_contrato, id_inquilino, id_inmueble, monto, fecha_inicio, fecha_fin, vigencia 
                FROM contrato
                WHERE vigencia = 1";
            using (var command = new MySqlCommand(sql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var contrato = new Contrato
                        {
                            Id_contrato = reader.GetInt32("id_contrato"),
                            Id_inquilino = reader.GetInt32("id_inquilino"),
                            Id_inmueble = reader.GetInt32("id_inmueble"),
                            Monto = reader.GetInt32("monto"),
                            Fecha_inicio = reader.GetDateTime("fecha_inicio"),
                            Fecha_fin = reader.GetDateTime("fecha_fin"),
                            Vigencia = reader.GetBoolean("vigencia")
                        };

                        if (contrato.Fecha_fin < DateTime.Now)
                        {
                            contrato.Vigencia = false;
                            ActualizarContrato(contrato);
                        }
                        else
                        {
                            contrato.Vigencia = true;
                            ActualizarContrato(contrato);
                        }
                        contratos.Add(contrato);
                    }
                }
                connection.Close();
            }
        }
        return contratos;
    }

    // Listar Contratos Terminados
    public IList<Contrato> ListarContratosTerminados()
    {
        var contratos = new List<Contrato>();
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var sql =
                @"SELECT id_contrato, id_inquilino, id_inmueble, monto, fecha_inicio, fecha_fin, vigencia 
                FROM contrato
                WHERE vigencia = 0";
            using (var command = new MySqlCommand(sql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var contrato = new Contrato
                        {
                            Id_contrato = reader.GetInt32("id_contrato"),
                            Id_inquilino = reader.GetInt32("id_inquilino"),
                            Id_inmueble = reader.GetInt32("id_inmueble"),
                            Monto = reader.GetInt32("monto"),
                            Fecha_inicio = reader.GetDateTime("fecha_inicio"),
                            Fecha_fin = reader.GetDateTime("fecha_fin"),
                            Vigencia = reader.GetBoolean("vigencia")
                        };

                        if (contrato.Fecha_fin < DateTime.Now)
                        {
                            contrato.Vigencia = false;
                            ActualizarContrato(contrato);
                        }
                        else
                        {
                            contrato.Vigencia = true;
                            ActualizarContrato(contrato);
                        }
                        contratos.Add(contrato);
                    }
                }
                connection.Close();
            }
        }
        return contratos;
    }

    public int GuardarNuevo(Contrato contrato)
    {
        int id = 0;
        using (var connection = new MySqlConnection(ConnectionString))
        {
            
            var sql =
                @"INSERT INTO contrato (id_inquilino, id_inmueble, monto, fecha_inicio, fecha_fin)
                            VALUES (@id_inquilino, @id_inmueble, @monto, @fecha_inicio, @fecha_fin, @usuario);
                            SELECT LAST_INSERT_ID();";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id_inquilino", contrato.Id_inquilino);
                command.Parameters.AddWithValue("@id_inmueble", contrato.Id_inmueble);
                command.Parameters.AddWithValue("@monto", contrato.Monto);
                command.Parameters.AddWithValue("@fecha_inicio", contrato.Fecha_inicio);
                command.Parameters.AddWithValue("@fecha_fin", contrato.Fecha_fin);
                command.Parameters.AddWithValue("@usuario", contrato.Id_usuario);

                connection.Open();
                id = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
        }
        return id;
    }

    public Contrato ObtenerContrato(int id)
    {
        Contrato? contrato = null;
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var sql =
                @"SELECT id_contrato, id_inquilino, id_inmueble, monto, fecha_inicio, fecha_fin
                            FROM contrato
                            WHERE id_contrato = @id_contrato";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id_contrato", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        contrato = new Contrato
                        {
                            Id_contrato = reader.GetInt32("id_contrato"),
                            Id_inquilino = reader.GetInt32("id_inquilino"),
                            Id_inmueble = reader.GetInt32("id_inmueble"),
                            Monto = reader.GetInt32("monto"),
                            Fecha_inicio = reader.GetDateTime("fecha_inicio"),
                            Fecha_fin = reader.GetDateTime("fecha_fin")
                        };
                    }
                }
                connection.Close();
            }
        }
        return contrato;
    }

    public List<Contrato> ObtenerContratosPorInquilino(int id)
{
    var contratos = new List<Contrato>();
    using (var connection = new MySqlConnection(ConnectionString))
    {
        var sql =
            @"SELECT id_contrato, id_inquilino, id_inmueble, monto, fecha_inicio, fecha_fin
              FROM contrato
              WHERE id_inquilino = @id_inquilino
              AND fecha_fin >= CURDATE()";

        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@id_inquilino", id);

            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var contrato = new Contrato
                    {
                        Id_contrato = reader.GetInt32("id_contrato"),
                        Id_inquilino = reader.GetInt32("id_inquilino"),
                        Id_inmueble = reader.GetInt32("id_inmueble"),
                        Monto = reader.GetInt32("monto"),
                        Fecha_inicio = reader.GetDateTime("fecha_inicio"),
                        Fecha_fin = reader.GetDateTime("fecha_fin")
                    };

                    contratos.Add(contrato); // Agregar a la lista
                }
            }
            connection.Close();
        }
    }
    return contratos; // Retornar la lista de contratos
}


    public Contrato ObtenerContratoInmueble(int id)
    {
        Contrato? contrato = null;
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var sql =
                @"SELECT id_contrato, id_inquilino, id_inmueble, monto, fecha_inicio, fecha_fin, vigencia
                            FROM contrato
                            WHERE id_inmueble = @id_inmueble";

            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id_inmueble", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        contrato = new Contrato
                        {
                            Id_contrato = reader.GetInt32("id_contrato"),
                            Id_inquilino = reader.GetInt32("id_inquilino"),
                            Id_inmueble = reader.GetInt32("id_inmueble"),
                            Monto = reader.GetInt32("monto"),
                            Fecha_inicio = reader.GetDateTime("fecha_inicio"),
                            Fecha_fin = reader.GetDateTime("fecha_fin"),
                            Vigencia = reader.GetBoolean("vigencia")
                        };
                    }
                }
                connection.Close();
            }
        }
        return contrato;
    }

    public void ActualizarContrato(Contrato contrato)
    {
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var sql =
                @"UPDATE contrato
                            SET id_inquilino = @id_inquilino,
                                id_inmueble = @id_inmueble,
                                monto = @monto,
                                fecha_inicio = @fecha_inicio,
                                fecha_fin = @fecha_fin,
                                vigencia = @vigencia
                            WHERE id_contrato = @id_contrato";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id_inquilino", contrato.Id_inquilino);
                command.Parameters.AddWithValue("@id_inmueble", contrato.Id_inmueble);
                command.Parameters.AddWithValue("@monto", contrato.Monto);
                command.Parameters.AddWithValue("@fecha_inicio", contrato.Fecha_inicio);
                command.Parameters.AddWithValue("@fecha_fin", contrato.Fecha_fin);
                command.Parameters.AddWithValue("@id_contrato", contrato.Id_contrato);
                command.Parameters.AddWithValue("@vigencia", contrato.Vigencia);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    public void EliminarContrato(int id)
    {
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var sql = "DELETE FROM contrato WHERE id_contrato = @id_contrato";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id_contrato", id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    // Metodo para saber si un inmueble tiene contrato o no
    public int InmuebleTieneContrato(int idInmueble)
{
    int contratoCount = 0;
    using (var connection = new MySqlConnection(ConnectionString))
    {
        var sql = @"SELECT COUNT(*) 
        FROM contrato 
        WHERE id_inmueble = @IdInmueble 
        AND fecha_fin > CURDATE();";
        using (var command = new MySqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@IdInmueble", idInmueble);
            connection.Open();
            contratoCount = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
        }
    }
    return contratoCount;
}

    
    // Metodo q se usa con InmueblesController para listar los contratos de un inmueble
    public IList<Contrato> ListarContratosPorInmueble(int idInmueble)
    {
        var contratos = new List<Contrato>();

        using (var connection = new MySqlConnection(ConnectionString))
        {
            var sql =
                "SELECT id_contrato, id_inquilino, id_inmueble, monto, fecha_inicio, fecha_fin "
                + "FROM contrato "
                + "WHERE id_inmueble = @IdInmueble";

            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@IdInmueble", idInmueble);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        contratos.Add(
                            new Contrato
                            {
                                Id_contrato = reader.GetInt32("id_contrato"),
                                Id_inquilino = reader.GetInt32("id_inquilino"),
                                Id_inmueble = reader.GetInt32("id_inmueble"),
                                Monto = reader.GetInt32("monto"),
                                Fecha_inicio = reader.GetDateTime("fecha_inicio"),
                                Fecha_fin = reader.GetDateTime("fecha_fin")
                            }
                        );
                    }
                }
                connection.Close();
            }
        }

        return contratos;
    }
}
