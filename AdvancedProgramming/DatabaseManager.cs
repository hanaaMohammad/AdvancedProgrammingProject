using System.Data.SQLite;

namespace AdvancedProgramming
{
    public static class DatabaseManager
    {
        private static readonly string DbPath = "app.db";
        private static readonly string ConnectionString = $"Data Source={DbPath};Version=3;";

        public static void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(
                    "CREATE TABLE IF NOT EXISTS users (id INTEGER PRIMARY KEY AUTOINCREMENT, username VARCHAR NOT NULL UNIQUE, password VARCHAR NOT NULL)",
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
