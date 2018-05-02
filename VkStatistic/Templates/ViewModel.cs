using System;
using VkNet;
using VkNet.Model;
using VkNet.Enums.Filters;
using System.Collections.ObjectModel;
using VkNet.Model.RequestParams;
using VkNet.Enums.SafetyEnums;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Threading.Tasks;

namespace VkStatistic.Templates
{
    public class Vk:INotifyPropertyChanged
    {
        VkApi _vkApi;
        string _userAccountID { set; get; }
        
        public string UserAccountID
        {
            get => _userAccountID;
            set
            {
                _userAccountID = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Data about your friends
        /// </summary>
        public ObservableCollection<UserData> Users { set; get; }
        /// <summary>
        ///     Data about written user id
        /// </summary>
        public UserData UsersStatistic { set; get; }
        /// <summary>
        ///     Data about your account
        /// </summary>
        public Account MyAccount { set; get; }

        uint countMales = 0;
        uint countFemales = 0;


        public Vk( VkApi vk)
        {
            MyAccount = new Account();
            _canExecute = true;
     
            // важливо, щоб ObservableCollection спрацював, олгошуємо його в конструкторі, а вже тоді присвоюємо DataContext 
            // інакше спочакту буде присвоєння DataContext, а тоді оголошення ObservableCollection - працювати не буде
            Users = new ObservableCollection<UserData>();
            UsersStatistic = new UserData();
        }

        private ICommand _clickCommand;
        private bool _canExecute;

        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(
                     () => {  MainProcess(); }, 
                    _canExecute)
                 );
            }
        }
 
        async void MainProcess()
        {
            _vkApi = RootVM.GetVkData();
            if (UserAccountID == "Enter ID")
            {
                return;
            }

            // reset all data 
            if(Users != null) Users.Clear();
            if (UsersStatistic != null) UsersStatistic.Clear();
           
            try
            {
                long? userID = Convert.ToInt64(UserAccountID);
                GetFriendsData(userID);                              // control
                await FriendsAllData(userID);
                GetStatisticData(userID);
                //await AccountData();
            }
            catch
            { 
                return;
            }    
        }

        async Task AccountData(long? _userId)
        {
            var users = _vkApi.Users.Get(new long[] { (long)_userId }, ProfileFields.All);

            await  Task.Run( () => {
            foreach (var user in users)
            {
                MyAccount.firstName = user.FirstName;
                MyAccount.lastName = user.LastName;
                MyAccount.Photo = (user.Photo200Orig != new Uri("https://vk.com/images/deactivated_200.png") ? (user.Photo200Orig != new Uri("https://vk.com/images/camera_200.png") ? user.Photo200Orig : (user.Sex.ToString() == "Male" ? new Uri("pack://application:,,,/Templates/Images/male.png") : new Uri("pack://application:,,,/Templates/Images/female.jpg"))) : (user.Photo200Orig != new Uri("https://vk.com/images/deactivated_200.png") ? user.Photo200Orig : (user.Sex.ToString() == "Male" ? new Uri("pack://application:,,,/Templates/Images/male.png") : new Uri("pack://application:,,,/Templates/Images/female.jpg"))));
            }
            });
        }

        async Task FriendsAllData(long? _userId)
        {
            Debug.WriteLine("Call FriendAllData");

            await Task.Run(() =>
            {
                foreach (User friend in  GetFriendsData(_userId))
                {
                    if (friend.FirstName == "DELETED") continue;
                    if (friend.Sex.ToString() == "Male") countMales++;
                    else countFemales++;


                    App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE IMPORTANT (because object ObservableCollection must not call from other stream)
                    {
                        Users.Add(
                               new UserData
                               {
                                   firstName = friend.FirstName,
                                   lastName = friend.LastName,
                                   userID = friend.Id,
                                   birthday = "День народження: " + (friend.BirthDate != null ? (Convert.ToDateTime(friend.BirthDate)).ToLongDateString() : "не вказаний"),
                                   sex = "Стать: " + friend.Sex,
                                   city = "Місто: " + (friend.City != null ? friend.City.Title.ToString() : "не вказано"),
                                   online = "Онлайн: " + (friend.Online == true ? "Онлайн" : "неонлайн"),
                                   career = "Місце роботи: " + (friend.Career.Count != 0 ? friend.Career[0].GroupId.ToString() : "не вказано"),
                                   education = "Місце навчання: " + (friend.Education != null ? friend.Education.UniversityName : "не вказано"),
                                   country = "Країна: " + (friend.Country != null ? friend.Country.Title : "не вказано"),

                                   Photo = (friend.Photo200Orig != new Uri("https://vk.com/images/deactivated_200.png") ? (friend.Photo200Orig != new Uri("https://vk.com/images/camera_200.png") ? friend.Photo200Orig : (friend.Sex.ToString() == "Male" ? new Uri("pack://application:,,,/Templates/Images/male.png") : new Uri("pack://application:,,,/Templates/Images/female.jpg"))) : (friend.Photo200Orig != new Uri("https://vk.com/images/deactivated_200.png") ? friend.Photo200Orig : (friend.Sex.ToString() == "Male" ? new Uri("pack://application:,,,/Templates/Images/male.png") : new Uri("pack://application:,,,/Templates/Images/female.jpg")))),                    
                               }
                        );
                    });
                }
            });
        } 

        ReadOnlyCollection<User> GetFriendsData(long? _userId)
        {
            FriendsGetParams parameters = new FriendsGetParams
            {
                UserId = _userId,
                Fields = ProfileFields.FirstName | ProfileFields.LastName | ProfileFields.Photo200Orig |
                         ProfileFields.BirthDate | ProfileFields.City | ProfileFields.Sex |
                         ProfileFields.Online | ProfileFields.Uid | ProfileFields.Career | ProfileFields.Education | ProfileFields.Country,
                Order = FriendsOrder.Name
            };
      
            var users = _vkApi.Friends.Get(parameters);
            return users;
        }

        void GetStatisticData(long? _userId)
        {
            Debug.WriteLine("Call GetStatistic Data");
            var users = _vkApi.Users.Get(new long[] { (long)_userId }, ProfileFields.All);

           
                foreach (var user in users)
                {
                    UsersStatistic.numberFriends = "Кількість друзів: " + user.Counters.Friends.ToString();
                    UsersStatistic.numberOnlineFriends = "Кількість друзів онлайн: " + user.Counters.OnlineFriends.ToString();
                    UsersStatistic.numberMutualFriends = "Кількість спільних друзів: " + user.Counters.MutualFriends.ToString();
                    UsersStatistic.numberFollowers = "Кількість підписників: " + user.Counters.Followers.ToString();
                    UsersStatistic.numberMales = "Кількість осіб чоловічої статі: " + this.countMales;
                    UsersStatistic.numberFemales = "Кількість осіб жіночої статі: " + countFemales;

                    UsersStatistic.numberPhotos = "Кількість фотографій: " + user.Counters.Photos.ToString();
                    UsersStatistic.numberVideos = "Кількість відеозаписів: " + user.Counters.Videos.ToString();
                    UsersStatistic.numberAudios = "Кількість аудіозаписів: " + user.Counters.Audios.ToString();

                        if (user.Photo200Orig == new Uri("https://vk.com/images/deactivated_200.png") || user.Photo200Orig == new Uri("https://vk.com/images/camera_200.png"))
                        {
                            switch (user.Sex.ToString())
                            {
                                case "Famale": UsersStatistic.Photo = new Uri("pack://application:,,,/Templates/Images/female.png"); break;
                                case "Male": UsersStatistic.Photo = new Uri("pack://application:,,,/Templates/Images/male.png"); break;
                                default: UsersStatistic.Photo = new Uri("pack://application:,,,/Templates/Images/male.png"); break;
                            }
                        }
                        else { UsersStatistic.Photo = user.Photo200Orig; }

                    UsersStatistic.firstName = user.FirstName;
                    UsersStatistic.lastName = user.LastName;
                    UsersStatistic.birthday = user.BirthDate != null ? Convert.ToDateTime(user.BirthDate).ToLongDateString() : "";
                    UsersStatistic.city = user.City == null ? "" : user.City.Title.ToString();
            }  
        }
    
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
             PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }

    public class CommandHandler : ICommand
    {
        private Action _action;
        private bool _canExecute;
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
