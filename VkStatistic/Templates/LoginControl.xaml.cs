using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;
using VkNet;

namespace VkStatistic.Templates
{
    /// <summary>
    /// Логика взаимодействия для LoginControl.xaml
    /// </summary>
    public partial class LoginControl: UserControl
    {
        LoginingUser logUser;
        VkApi _vkApi;
        Action changeContent;
        
        public bool Status { private set; get; }

        public LoginControl( Action func)
        {
            InitializeComponent();

            ref VkApi VkApi = ref RootVM.GetVkData();

            logUser = new LoginingUser(ref VkApi);
            DataContext = logUser;
            changeContent += func;
            
            RegisterStyle();
        }

        void RegisterStyle()
        {      
            logUser.Login = "email or phone";                         
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
            else
            {
                // important
                txt.Foreground = new SolidColorBrush(Colors.Black);
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
            else
            {
                txt.Foreground = new SolidColorBrush(Colors.Black);
            }
        }
       

        void Send_Data_Button(object sender, EventArgs e)
        {
            if(PaswrdBox.Password == String.Empty || LoginBox.Text == string.Empty)
            {
                logUser.Error = "Ви не заповнили всі поля!";
                return;
            }

            if (logUser.Error != "") logUser.Error = "";

            logUser.Password = PaswrdBox.Password.ToString();
            _vkApi = logUser.Log_in();

            if (logUser.status)
            {
                changeContent();
                Debug.WriteLine("вхід завершений");
            }        
        }

        public  ref VkApi GetVkData()
        {
            return ref _vkApi;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }


  
}
