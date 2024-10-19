using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace pract8
{
    public partial class UserProfileForm : Form
    {
        public UserProfileForm(string username, string email)
        {
            InitializeComponent();
            labelUsername.Text = $"Добро пожаловать, {username}!";
            labelEmail.Text = $"Ваша почта: {email}";
        } 

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();

            // Закрытие текущей формы (UserProfileForm)
            this.Close();
        }

        private void UserProfileForm_Load(object sender, EventArgs e)
        {

        }
    }
}
