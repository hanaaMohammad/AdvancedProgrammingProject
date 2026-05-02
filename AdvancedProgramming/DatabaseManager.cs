using System;
using System.IO;
using System.Data.SQLite;

namespace AdvancedProgramming
{
    public static class DatabaseManager
    {
        private static readonly string DbPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "app.db");
        private static readonly string ConnectionString = $"Data Source={DbPath};Version=3;";

        public static void InitializeDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string createTableSql = @"
                        CREATE TABLE IF NOT EXISTS users (
                            username VARCHAR NOT NULL,
                            password VARCHAR NOT NULL
                        );";
                    using (var command = new SQLiteCommand(createTableSql, connection))
                        command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db_error.txt"), ex.ToString());
                throw;
            }
        }
    }
}
