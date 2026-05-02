using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace AdvancedProgramming.Forms
{
    public class LogInForm : Form
    {
        private TextBox userNmaseTextBox;
        private TextBox passwordTextBox;
        private Button logInButton;
        private Button passwordTaggel;
        private bool passwordVasibilty = false;
        private Label Massage;
        public LogInForm() {
        
            InitializeLogInComponents();
           
        }

       private void  InitializeLogInComponents(){
            this.Size = new Size(400, 400);
            this.Text = "Log In";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            var userNameLabel = new Label { Text = "Username:", Location = new Point(30, 30), Size = new Size(100, 20) };
            userNmaseTextBox = new TextBox { Location = new Point(140, 28), Size = new Size(200, 20) };

            var  passwordLabel= new Label { Text = "Password:", Location = new Point(30, 70), Size = new Size(100, 20) };

            passwordTextBox = new TextBox { Location = new Point(140, 68), Size = new Size(160, 20), PasswordChar = '*' };

            passwordTaggel = new Button { Text = "👁", Location = new Point(305, 66), Size = new Size(30, 23), FlatStyle = FlatStyle.Flat };

            logInButton = new Button { Text = "log in", Location = new Point(140, 160), Size = new Size(100, 30) };
            passwordTaggel.Click += PasswordTaggel_Click;
            logInButton.Click += LogInButton_Click;


            Massage =new Label {Text="Masseg from App",Location = new Point(30,250) ,Size = new Size(320, 60) ,FlatStyle = FlatStyle.Flat  };
            Controls.Add(Massage);
            Controls.Add(passwordTaggel);
            Controls.Add(logInButton);
            Controls.Add(passwordTextBox);
            Controls.Add(userNmaseTextBox);
            Controls.Add(userNameLabel);
            Controls.Add(passwordLabel);
     



        }

        private void PasswordTaggel_Click(object sender, EventArgs e)
        {
         


            passwordVasibilty = !passwordVasibilty;
            passwordTextBox.PasswordChar = passwordVasibilty? '\0' : '*';
            passwordTaggel.Text=passwordVasibilty? "🙈" : "👁";

        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            string password = passwordTextBox.Text;
            string username = userNmaseTextBox.Text;
            var user = new UserManagement();
           if(string.IsNullOrEmpty(username)) 
                Massage.Text = "UserName is requierd pealse enter it !!";
           if(string.IsNullOrEmpty(password)) 
                Massage.Text= "Password is requierd pealse enter it !!";
           
            if (user.SignIn(username,password))
                Massage.Text = "Registration successful ";
            else
                Massage.Text = "0oops!! Registration failed ";


        }
    }
}
