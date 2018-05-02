using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using VkNet;
using VkNet.Enums.Filters;



namespace VkStatistic.Templates
{
    public class LoginingUser: INotifyPropertyChanged
    {
        VkApi _vkApi;
        ulong appId = 6410920;
        string login { get; set; }
        long? userId { get; set; }
        static string token;
        Settings scope = Settings.All;
        public bool status = false;

        string error { get; set; }
        string[] errors = new string[]
        {
            "Invalid login or password!",
            "Unable to connect to server! Check the correct data entry, else turn on VPN",
            "You are logged successfully!"
        };

        public LoginingUser() { }

        public LoginingUser(ref VkApi vk)
        {
            _vkApi = vk;
        }

        public LoginingUser(ref VkApi vk, string _login , string _password)
        {
            Login = _login;
            Password = _password;
            this._vkApi = vk;
        }

        public string Login
        {
            private get => login;
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }

        string password { get; set; }
        public string Password
        {
            private get => password;
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        public string Error { get => error;
        set
            {
                error = value;
                OnPropertyChanged("Error");
            }
        }

        public ref VkApi Log_in()
        {
            //ref VkApi _vkApi = ref RootVM.GetVkData();

            ApiAuthParams vkAuth = new ApiAuthParams
            {
                ApplicationId = appId,
                Login = Login,
                Password = Password,
                Settings = scope
            };

            try
            {
                if (_vkApi == null)
                    _vkApi = new VkApi();

                _vkApi.Authorize(vkAuth);
                token = _vkApi.Token;
                userId = _vkApi.UserId;
                status = true;
            }

            catch (VkNet.Exception.VkApiAuthorizationException)
            {
                Error = errors[0];

                status = false;
                MessageBox.Show(errors[0]);
            }
            catch (VkNet.Exception.VkApiException)
            {
                Error = errors[1];

                status = false;
            }
            if (status) MessageBox.Show(errors[2]);
            return ref _vkApi;
        }

        public ref VkApi GetVkData()
        {
            return ref _vkApi;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
