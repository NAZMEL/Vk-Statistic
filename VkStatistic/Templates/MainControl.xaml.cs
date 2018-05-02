using System;
using System.Windows.Controls;
using System.Windows.Media;
using VkNet;


namespace VkStatistic.Templates
{
    public partial class MainControl : UserControl
    {
        public Vk vk;
        public MainControl(ref VkApi vkApi)
        {
            InitializeComponent();
            vk = new Vk(vkApi);
            vk.UserAccountID = "Enter ID";
            DataContext = vk;
        }

        private void GotFocusID(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt.Text == "Enter ID")
            {
                txt.Foreground = new SolidColorBrush(Colors.Black);
                txt.Text = "";
            }
        }

        void LostFocusID(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (txt.Text == "")
            {
                txt.Text = "Enter ID";
                txt.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
    }
}
