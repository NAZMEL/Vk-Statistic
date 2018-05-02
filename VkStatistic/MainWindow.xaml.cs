using System;
using System.Windows;
using System.Windows.Controls;
using VkNet;
using System.ComponentModel;

using VkStatistic.Templates;

namespace VkStatistic
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new RootVM();
        }
    }

    public class RootVM : INotifyPropertyChanged
    {
        static public VkApi _vkApi;
        UserControl _CurrentContentVM;
       static Action CurrentContentAction;

        static UserControl[] userControls = new UserControl[] { new LoginControl(delegate { CurrentContentAction(); }), new MainControl(ref _vkApi)};

        static uint i = 0;

        public RootVM()
        {
            _vkApi = new VkApi();
            CurrentContentAction += ChangeCurrentContent;
            CurrentContentVM = userControls[i]; 
        }

        public UserControl CurrentContentVM
        {
            get => _CurrentContentVM;
            set
            {
                if (_CurrentContentVM != value)
                {
                    _CurrentContentVM = value;
                    OnPropertyChanged("CurrentContentVM");
                }
            }
        }

        public void ChangeCurrentContent()
        {
            _vkApi = (CurrentContentVM as LoginControl).GetVkData();
            CurrentContentVM = userControls[++i];    
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {   
            if (this.PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, e);
            }
        }

        public static ref VkApi GetVkData()
        {
            return ref _vkApi;
        }
    }


}
