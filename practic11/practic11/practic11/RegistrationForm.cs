using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;



namespace pract8
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        // Метод для хеширования строки с использованием SHA-256
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

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if ((textBoxLogin.Text != "") & (textBoxPassword.Text != "") & (textBoxConfirmPassword.Text != "") & (textBoxEmail.Text != "")) {
                using (UserContext db = new UserContext())
                {
                    string hashedPassword = GetHashString(textBoxPassword.Text);
                    User newUser = new User(textBoxLogin.Text, hashedPassword, textBoxEmail.Text, "User");

                    db.Users.Add(newUser);
                    db.SaveChanges();

                    MessageBox.Show("Регистрация прошла успешно!");

                    // Переход на форму LoginForm
                    LoginForm loginForm = new LoginForm();
                    loginForm.Show();

                    // Закрытие текущей формы (RegistrationForm)
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
    }
}