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
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            username VARCHAR NOT NULL UNIQUE,
                            password VARCHAR NOT NULL,
                            created_at DATETIME DEFAULT CURRENT_TIMESTAMP
                        );";
                    using (var command = new SQLiteCommand(createTableSql, connection))
                        command.ExecuteNonQuery();

                    string checkAdmin = "SELECT COUNT(*) FROM users WHERE username = 'admin'";
                    using (var checkCmd = new SQLiteCommand(checkAdmin, connection))
                    {
                        long count = (long)checkCmd.ExecuteScalar();
                        if (count == 0)
                        {
                            string addAdmin = "INSERT INTO users (username, password) VALUES ('admin', 'admin')";
                            using (var adminCmd = new SQLiteCommand(addAdmin, connection))
                                adminCmd.ExecuteNonQuery();
                        }
                    }
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
