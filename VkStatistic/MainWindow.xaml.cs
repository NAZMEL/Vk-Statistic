using System;
using System.Collections.Generic;
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
using VkNet;
using VkNet.Model;
using VkNet.Enums.Filters;
using System.Collections;
using System.Collections.ObjectModel;
using VkNet.Model.RequestParams;
using System.Diagnostics;
using System.Net;
//using System.Drawing;
using VkNet.Enums.SafetyEnums;
using System.ComponentModel;
using System.Threading;

using VkStatistic.Templates;

namespace VkStatistic
{

    public partial class MainWindow : Window
    {
        public Vk vk;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new RootVM(ref vk);
        }
    }
        
    

    class RootVM : INotifyPropertyChanged
    {
        object currentContentVM;

        public RootVM(ref Vk _vk)
        {
            CurrentContentVM = new LoginControl();
        }
        public object CurrentContentVM
        {
            get => currentContentVM;
            set
            {
                if (currentContentVM != value)
                {
                    currentContentVM = value;
                    OnPropertyChanged("CurrentContentVM");
                }
            }
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
    }


}
