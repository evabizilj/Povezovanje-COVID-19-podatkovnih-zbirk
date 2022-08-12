using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sledilnikCovid.Persistence
{
    public class DatabaseConnection
    {
        public async void dbConnection()
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "coviddata.mysql.database.azure.com",
                Database = "coviddata",
                UserID = "evabizilj",
                Password = "Fzht62p9",
                SslMode = MySqlSslMode.Required,
            };

            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM drzava;"; // test primer

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            //izpis v konzolo
                            Console.WriteLine(string.Format(
                                "Reading from table=({0}, {1}, {2})",
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetInt32(2)));
                        }
                    }
                }
            }

        }
    }
}
