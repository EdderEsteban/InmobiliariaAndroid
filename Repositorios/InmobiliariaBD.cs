using MySql.Data.MySqlClient;

namespace InmobiliariaBD
{
    public abstract class RepositorioBD
    {
        protected readonly string ConnectionString = "Server=localhost;Database=inmobiliaria_edder;User=root;Password=;";

        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}