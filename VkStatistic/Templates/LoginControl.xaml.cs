using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;

using VkStatistic.Templates;

namespace VkStatistic.Templates
{
    /// <summary>
    /// Логика взаимодействия для LoginControl.xaml
    /// </summary>
    public partial class LoginControlVM: UserControl
    {
        public LoginControlVM()
        {
            InitializeComponent();
            
            RegisterStyle();
        }

        void RegisterStyle()
        {
            LoginBox.Text = "email or phone";
            LoginBox.Foreground = new SolidColorBrush(Colors.Gray);
        }

        
        private void GotFocusLogin(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (txt.Text == "email or phone")
            {
                txt.Foreground = new SolidColorBrush(Colors.Black);
                txt.Text = "";
            }
        }

        void LostFocusLogin(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (txt.Text == "")
            {
                txt.Text = "email or phone";
                txt.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
       

        void Send_Data_Button(object sender, EventArgs e)
        {
            string login = LoginBox.Text;
            string password = PaswrdBox.Password.ToString();
            Debug.WriteLine(password);
        }


    }
}
