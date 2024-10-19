using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace pract8
{
    public partial class PasswordRecoveryForm : Form
    {
        private string generatedCode;  // Генерируемый код для проверки
        private string userEmail;      // Email пользователя для сброса
        public PasswordRecoveryForm()
        {
            InitializeComponent();
        }

        // Обработчик для кнопки "Отправить код"
        private void buttonSendCode_Click(object sender, EventArgs e)
        {
            userEmail = textBoxEmail.Text;

            // Проверяем, что email введен корректно
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                MessageBox.Show("Введите корректный email!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверяем, существует ли email в базе данных
            using (UserContext db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Email == userEmail);
                if (user == null)
                {
                    MessageBox.Show("Email не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Генерируем код и отправляем его на email
            generatedCode = GenerateCode();
            try
            {
                SendCodeToEmail(userEmail, generatedCode);
                MessageBox.Show("Код отправлен на вашу почту!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка отправки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Обработчик для кнопки "Сбросить пароль"
        private void buttonResetPassword_Click(object sender, EventArgs e)
        {
            // Проверяем, что введенный код совпадает с отправленным
            if (textBoxCode.Text != generatedCode)
            {
                MessageBox.Show("Неверный код!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string newPassword = textBoxPassword.Text;

            // Проверяем, что новый пароль введён
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("Введите новый пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Обновляем пароль в базе данных
            using (UserContext db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Email == userEmail);
                if (user != null)
                {
                    user.Password = newPassword;
                    db.SaveChanges();
                }
            }

            MessageBox.Show("Пароль успешно обновлён!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearFields();
        }

        // Функция генерации случайного кода
        private string GenerateCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();  // Генерируем 6-значный код
        }

        // Функция отправки кода на email
        private void SendCodeToEmail(string email, string code)
        {
            MailAddress from = new MailAddress("olgayayayayayaya@yandex.ru", "deb");
            MailAddress to = new MailAddress(email);
            MailMessage message = new MailMessage(from, to)
            {
                Subject = "Код для сброса пароля",
                Body = $"Ваш код для сброса пароля: {code}",
                IsBodyHtml = true
            };

            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 25)
            {
                Credentials = new NetworkCredential("olgayayayayayaya@yandex.ru", "xguecytsxoqptpna"), // Убедитесь, что пароль защищен
                EnableSsl = true,
            };

            smtp.Send(message);
        }

        // Очистка полей формы
        private void ClearFields()
        {
            textBoxEmail.Clear();
            textBoxCode.Clear();
            textBoxPassword.Clear();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
        }
    }
}
