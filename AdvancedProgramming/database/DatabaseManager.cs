using System.Data.SQLite;
using System.IO;

namespace AdvancedProgramming
{
    public static class DatabaseManager
    {
        private static readonly string DatabaseFolder;
        private static readonly string DbPath;
        private static readonly string ConnectionString;

        static DatabaseManager()
        {
            var projectDir = Directory.GetCurrentDirectory();
            while (!File.Exists(Path.Combine(projectDir, "AdvancedProgramming.csproj")) && projectDir != Path.GetPathRoot(projectDir))
            {
                projectDir = Path.GetDirectoryName(projectDir);
            }
            DatabaseFolder = Path.Combine(projectDir, "database");
            DbPath = Path.Combine(DatabaseFolder, "app.db");
            ConnectionString = $"Data Source={DbPath};Version=3;";
        }

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }

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
