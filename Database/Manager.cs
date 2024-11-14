using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazendaUrbanaDesktop.Database
{
    public static class DatabaseConnection
    {
        private const string ConnectionString = @"Server=(LocalDB)\MSSQLLocalDB;Integrated Security=true;Initial Catalog=FazendaUrbanaDB;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public static void ExecuteQuery(string query)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao executar a consulta: {ex.Message}");
                }
            }
        }

        public static SqlDataReader ExecuteReader(string query)
        {
            var connection = GetConnection();
            connection.Open();
            using (var command = new SqlCommand(query, connection))
            {
                return command.ExecuteReader();
            }
        }
    }
}
