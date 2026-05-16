using System;
using System.Data.SQLite;
using System.IO;
using System.Reflection;

namespace AdvancedProgramming
{
    public static class DatabaseManager
    {
        private static readonly string DbPath = Path.GetFullPath(Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
            "..", "..", "Database", "app.db"));
        private static readonly string ConnectionString = $"Data Source={DbPath};Version=3;";

        public static SQLiteConnection GetConnection() => new SQLiteConnection(ConnectionString);

        public static void InitializeDatabase()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(DbPath));

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "CREATE TABLE IF NOT EXISTS users (id INTEGER PRIMARY KEY AUTOINCREMENT, username VARCHAR NOT NULL UNIQUE, password VARCHAR NOT NULL, Country VARCHAR, Gender VARCHAR, score INTEGER DEFAULT 0)",
                    connection))
                    command.ExecuteNonQuery();

                using (var checkCmd = new SQLiteCommand("SELECT COUNT(*) FROM users WHERE username = 'admin'", connection))
                {
                    if ((long)checkCmd.ExecuteScalar() == 0)
                        using (var adminCmd = new SQLiteCommand("INSERT INTO users (username, password) VALUES ('admin', 'admin')", connection))
                            adminCmd.ExecuteNonQuery();
                }
            }
        }
    }
}
