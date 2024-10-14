using System;
using System.Collections.Generic;
using Inmobiliaria.Models;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Repositorios;

public class RepositorioPago : InmobiliariaBD.RepositorioBD
{
    public RepositorioPago() { }

    public IList<Pago> ListarPagos()
    {
        var pagos = new List<Pago>();
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var sql =
                @"SELECT id_pago, id_contrato, fecha_pago, monto, periodo 
            FROM pago";
            using (var command = new MySqlCommand(sql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pagos.Add(
                            new Pago
                            {
                                Id_Pago = reader.GetInt32("id_pago"),
                                Id_Contrato = reader.GetInt32("id_contrato"),
                                Fecha_Pago = reader.GetDateTime("fecha_pago"),
                                Monto = reader.GetDecimal("monto"),
                                Periodo = reader.GetDateTime("periodo"),
                            }
                        );
                    }
                }
                connection.Close();
            }
        }
        return pagos;
    }

    public int GuardarNuevo(Pago pago)
    {
        int id = 0;
       
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var sql =
                @"INSERT INTO pago (id_contrato, fecha_pago, monto, periodo, id_inquilino, id_Usuario) 
                        VALUES (@id_contrato, @fecha_pago, @monto, @periodo, @id_inquilino, @id_Usuario); 
                        SELECT LAST_INSERT_ID();";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id_contrato", pago.Id_Contrato);
                command.Parameters.AddWithValue("@fecha_pago", pago.Fecha_Pago);
                command.Parameters.AddWithValue("@monto", pago.Monto);
                command.Parameters.AddWithValue("@periodo", pago.Periodo);
                command.Parameters.AddWithValue("@id_inquilino", pago.Id_Inquilino);
                command.Parameters.AddWithValue("@id_Usuario", pago.Id_Usuario);

                connection.Open();
                id = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
        }
        return id;
    }

    public Pago ObtenerPago(int id)
    {
        Pago? pago = null;
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var sql =
                @"SELECT id_pago, id_contrato, fecha_pago, monto, periodo, id_inquilino 
            FROM pago WHERE id_pago = @id_pago";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id_pago", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        pago = new Pago
                        {
                            Id_Pago = reader.GetInt32("id_pago"),
                            Id_Contrato = reader.GetInt32("id_contrato"),
                            Fecha_Pago = reader.GetDateTime("fecha_pago"),
                            Monto = reader.GetDecimal("monto"),
                            Periodo = reader.GetDateTime("periodo"),
                            Id_Inquilino = reader.GetInt32("id_inquilino"),
                        };
                    }
                }
                connection.Close();
            }
        }
        return pago;
    }

    public void ActualizarPago(Pago pago)
    {
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var sql =
                @"UPDATE pago 
                        SET id_contrato = @id_contrato, 
                            fecha_pago = @fecha_pago, 
                            monto = @monto,
                            periodo = @periodo 
                        WHERE id_pago = @id_pago";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id_contrato", pago.Id_Contrato);
                command.Parameters.AddWithValue("@fecha_pago", pago.Fecha_Pago);
                command.Parameters.AddWithValue("@monto", pago.Monto);
                command.Parameters.AddWithValue("@id_pago", pago.Id_Pago);
                command.Parameters.AddWithValue("@periodo", pago.Periodo);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    public void EliminarPago(int id)
    {
        using (var connection = new MySqlConnection(ConnectionString))
        {
            var sql = "DELETE FROM pago WHERE id_pago = @id_pago";
            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id_pago", id);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    public bool ExistePago(Pago pago)
    {
        bool resultado = false; // Inicializa el resultado en falso

        try
        {
            // Obtén el mes y año del periodo
            int periodoMes = pago.Periodo.Month;
            int periodoAnio = pago.Periodo.Year;

            // Consulta para verificar si existe el pago
            var query =
                @"SELECT COUNT(*) FROM pago 
            WHERE id_inquilino = @id_inquilino 
            AND id_contrato = @id_contrato 
            AND MONTH(periodo) = @periodoMes 
            AND YEAR(periodo) = @periodoAnio";

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_inquilino", pago.Id_Inquilino);
                    command.Parameters.AddWithValue("@id_contrato", pago.Id_Contrato);
                    command.Parameters.AddWithValue("@periodoMes", periodoMes);
                    command.Parameters.AddWithValue("@periodoAnio", periodoAnio);

                    // Ejecuta la consulta y convierte el resultado
                    var count = Convert.ToInt32(command.ExecuteScalar());

                    // Si el conteo es mayor que 0, significa que existe el pago
                    resultado = count > 0;
                    
                }
            }
        }
        catch (Exception ex)
        {
            // Maneja la excepción si ocurre un error
            Console.WriteLine($"Error al verificar la existencia del pago: {ex.Message}");
        }

        return resultado; // Retorna el resultado
    }
}
