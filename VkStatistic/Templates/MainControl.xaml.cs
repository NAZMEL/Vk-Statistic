using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VkNet.Model.RequestParams;

using VkStatistic.Templates;

namespace VkStatistic.Templates
{
    public partial class UserControlVM : UserControl
    {
        public UserControlVM()
        {
            InitializeComponent();      
        }

        public Vk vk;
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string login = LoginBox.Text.ToString();
            string password = PaswrdBox.Password.ToString();
           
            vk = new Vk(login, password);
            this.DataContext = vk;

        }

        void Button_Send_Id(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("call Main Process");
            //string userId = IdUserBox.ToString();
        }

        private void SendMessage(string message, long id = 232607422)
        {
            try
            {
                vk._vkApi.Messages.Send(new MessagesSendParams
                {
                    UserId = id,
                    Message = message
                });
            }
            catch
            {
                MessageBox.Show("Для відправлення повідомлень необхідно авторизуватись!");
            }

        }
    }
}
