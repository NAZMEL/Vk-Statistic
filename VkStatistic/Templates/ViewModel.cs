using System;
using VkNet;
using VkNet.Model;
using VkNet.Enums.Filters;
using System.Collections.ObjectModel;
using VkNet.Model.RequestParams;
//using System.Drawing;
using VkNet.Enums.SafetyEnums;
using System.Threading;
using System.Diagnostics;
using System.Windows;


namespace VkStatistic.Templates
{
    public class Vk
    {
        public VkApi _vkApi;
        public long? userAccountID { set; get; }

        /// <summary>
        /// Data about your friends
        /// </summary>
        public ObservableCollection<UserData> Users { get; set; }
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

        public Vk(string login , string password)
        {
            _vkApi = new VkApi();
            MyAccount = new Account();
           
            if(Logining(login, password)) MainProcess();
        }

        public void MainProcess(long? userId = 232607422)
        {
            Debug.WriteLine("Main Process");
            FriendsAllData(userId);
            GetStatisticData(userId);
            //AccountData();
        }

        void AccountData(long? _userId = 232607422)
        {
            var users = _vkApi.Users.Get(new long[] { (long)_userId }, ProfileFields.All);
            foreach(var user in users)
            {
                MyAccount.firstName = user.FirstName;
                MyAccount.lastName = user.LastName;
                MyAccount.Photo = (user.Photo200Orig != new Uri("https://vk.com/images/deactivated_200.png") ? (user.Photo200Orig != new Uri("https://vk.com/images/camera_200.png") ? user.Photo200Orig : (user.Sex.ToString() == "Male" ? new Uri("pack://application:,,,/Templates/Images/male.png") : new Uri("pack://application:,,,/Templates/Images/female.jpg"))) : (user.Photo200Orig != new Uri("https://vk.com/images/deactivated_200.png") ? user.Photo200Orig : (user.Sex.ToString() == "Male" ? new Uri("pack://application:,,,/Templates/Images/male.png") : new Uri("pack://application:,,,/Templates/Images/female.jpg"))));
                
            }
        }

        bool  Logining(string _login, string _password, ulong appId = 6410920)
        {
            Settings scope = Settings.All;
            bool status = false;

            ApiAuthParams vkAuth = new ApiAuthParams{
                ApplicationId = appId,
                Login = _login,
                Password = _password,
                Settings = scope
            };
            try
            {
                if (_vkApi == null) _vkApi = new VkApi();

                _vkApi.Authorize(vkAuth);
                MyAccount.token = _vkApi.Token;
                MyAccount.uid = (long)_vkApi.UserId;
                status = true;
            }

            catch (VkNet.Exception.VkApiAuthorizationException)
            {
                MessageBox.Show("Неправильно введені логін або пароль!");
                _vkApi = null;
                status = false;
            }
            catch (VkNet.Exception.VkApiException)
            {
                MessageBox.Show("Неможливо з'єднатись з сервером!");
                _vkApi = null;
                status = false;
            }
            catch
            {
                MessageBox.Show("Інша помилка");
            }
            if (status) MessageBox.Show("Ви увійшли");
            return status;

        }

        void FriendsAllData(long? _userId)
        {
            Users = new ObservableCollection<UserData>();
            
            foreach (User friend in GetFriendsData(_userId))
            {
                if (friend.FirstName == "DELETED") continue;
                if (friend.Sex.ToString() == "Male") countMales++;
                else countFemales++;

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
            };
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
            var users = _vkApi.Users.Get(new long[] { (long)_userId }, ProfileFields.All);
            foreach (var user in users)
            {
                UsersStatistic = new UserData()
                {
                    numberFriends = "Кількість друзів: " + user.Counters.Friends.ToString(),
                    numberOnlineFriends = "Кількість друзів онланйн: " + user.Counters.OnlineFriends.ToString(),
                    numberMutualFriends = "Кількість спільних друзів: " + user.Counters.MutualFriends.ToString(),
                    numberFollowers = "Кількість підписників: " + user.Counters.Followers.ToString(),
                    numberMales = "Кількість осіб чоловічої статі: " + this.countMales, numberFemales = "Кількість осіб жіночої статі: " + countFemales,

                    numberPhotos = "Кількість фотографій: " + user.Counters.Photos.ToString(),
                    numberVideos = "Кількість відеозаписів: " + user.Counters.Videos.ToString(),
                    numberAudios = "Кількість аудіозаписів: " + user.Counters.Audios.ToString(),

                    Photo = user.Photo200Orig,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    birthday = Convert.ToDateTime(user.BirthDate).ToLongDateString(),
                    city = user.City.Title
                };
            }
        }
    }
}
