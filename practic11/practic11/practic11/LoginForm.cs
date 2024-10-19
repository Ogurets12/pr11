using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace pract8
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            textBoxPassword.UseSystemPasswordChar = true;
        }

        private string GetHashString(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hash)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if ((textBoxLogin.Text != "") & (textBoxPassword.Text != "")) { 
                using (UserContext db = new UserContext())
                {
                    var hashedPassword = GetHashString(textBoxPassword.Text);
                    User user = db.Users.FirstOrDefault(u => u.Login == textBoxLogin.Text && u.Password == hashedPassword);

                    if (user != null)
                    {
                        MessageBox.Show("Вход успешен!");
                        UserProfileForm userForm = new UserProfileForm(user.Login, user.Email);
                        userForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }
        }

        private void buttonOpenRegister_Click(object sender, EventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            registrationForm.Show();
            this.Hide(); // Скрыть текущую форму, если нужно
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PasswordRecoveryForm passwordRecoveryForm = new PasswordRecoveryForm();
            passwordRecoveryForm.Show();
            this.Hide(); // Скрыть текущую форму, если нужно
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPassword.Checked)
            {
                textBoxPassword.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
