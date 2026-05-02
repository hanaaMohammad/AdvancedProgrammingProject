using System;
using System.Windows.Forms;

namespace AdvancedProgramming
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DatabaseManager.InitializeDatabase();
            LoadUsers();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            if (DatabaseManager.AddUser(username, password))
            {
                MessageBox.Show("User added successfully!");
                txtUsername.Clear();
                txtPassword.Clear();
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Failed to add user. Username may already exist.");
            }
        }

        private void btnViewUsers_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            dgvUsers.DataSource = DatabaseManager.GetAllUsers();
        }
    }
}
