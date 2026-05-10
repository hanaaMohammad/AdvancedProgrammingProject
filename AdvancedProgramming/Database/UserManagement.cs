using System;
using System.Data.SQLite;

namespace AdvancedProgramming
{
    public class UserManagement
    {
        public bool SignUp(string username, string password ,string Country,string Gender)
        {
            if (UsernameExists(username))
                return false;

            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("INSERT INTO users (username, password ,Country , Gender ) VALUES (@username, @password, @Country, @Gender)", conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@Country", Country);
                    cmd.Parameters.AddWithValue("@Gender", Gender);
                    cmd.Parameters.AddWithValue("@score", 0);
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

        public (string Country, string Gender) GetUserDetails(string username)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT Country, Gender FROM users WHERE username = @username", conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return (reader["Country"].ToString(), reader["Gender"].ToString());
                    }
                }
            }
            return (null, null);
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
