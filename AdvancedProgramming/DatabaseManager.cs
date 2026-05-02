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
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db_error.txt"), ex.ToString());
                throw;
            }
        }

        public static bool AddUser(string username, string password)
        {
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO users (username, password) VALUES (@username, @password)";
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db_error.txt"), ex.ToString());
                return false;
            }
        }

        public static System.Data.DataTable GetAllUsers()
        {
            var table = new System.Data.DataTable();
            try
            {
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string sql = "SELECT id, username, created_at FROM users ORDER BY id";
                    using (var adapter = new SQLiteDataAdapter(sql, connection))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db_error.txt"), ex.ToString());
            }
            return table;
        }
    }
}
