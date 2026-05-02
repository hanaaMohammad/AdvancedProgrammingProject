using System;
using System.Data.SQLite;

namespace AdvancedProgramming
{
    public class UserManagement
    {
        public bool SignUp(string username, string password)
        {
            if (UsernameExists(username))
                return false;

            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("INSERT INTO users (username, password) VALUES (@username, @password)", conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        public bool SignIn(string username, string password)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM users WHERE username = @username AND password = @password", conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    return (long)cmd.ExecuteScalar() > 0;
                }
            }
        }

        public bool UsernameExists(string username)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM users WHERE username = @username", conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    return (long)cmd.ExecuteScalar() > 0;
                }
            }
        }
    }
}
