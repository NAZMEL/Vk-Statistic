using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VkStatistic.Templates
{
    public class UserData : INotifyPropertyChanged
    {
        /// <summary>
        ///     Без InotifyPropetrtyChanged ObservableCollection працювати не буде
        /// </summary>
        public UserData(){}

        string FirstName { get; set; }
        string LastName { get; set; }
        string Birthday { get; set; }
        string City { get; set; }
        string Sex { get; set; }
        long? UserID { get; set; }
        string Online { get; set; }
        Uri photo { get; set; }
        string Country { set; get; }
        string Education { set; get; }
        string Career { set; get; }

        public string firstName
        {
            get => FirstName;
            set
            {
                FirstName = value;
                OnPropertyChanged("firstName");
            }
        }

        public string lastName
        { get => LastName;
            set
            {
                LastName = value;
                OnPropertyChanged("lastName");
            }
        }

        public string birthday
        { get => Birthday;

           set {
                Birthday = value;
                OnPropertyChanged("birthday");
            }
        }

        public string city
        {
            get => City;
            set
            {
                City = value;
                OnPropertyChanged("city");
            }
        }
        public string sex
        {
            get => Sex;
            set
            {
                Sex = value;
                OnPropertyChanged("Sex");
            }
        }

        public long? userID
        {
            get => UserID;
            set
            {
                UserID = value;
                OnPropertyChanged("userID");
            }
        }
        public string online
        {
            get => Online;
            set
            {
                Online = value;
                OnPropertyChanged("online");
            }
        }
        public Uri Photo
        {
            get => photo;
            set
            {
                photo = value;
                OnPropertyChanged("Photo");
            }
        }
        public string country
        {
            get => Country;
            set
            {
                Country = value;
                OnPropertyChanged("country");
            }
        }
        public string education
        {
            set
            {
                Education = value;
                OnPropertyChanged("education");
            }
            get => Education;
        }
        public string career
        {
            set
            {
                Career = value;
                OnPropertyChanged("career;");
            }
            get => Career;
        }

        string NumberFriends { set; get; }
        string NumberFollowers { set; get; }
        string NumberMutualFriends { set; get; }
        string NumberMales { set; get; }
        string NumberFemales { set; get; }
        string NumberOnlineFriends { set; get; }
        string NumberPhotos { get; set; }
        string NumberAudios { get; set; }
        string NumberVideos { get; set; }

        public string numberFriends
        {
            get => NumberFriends;
            set
            {
                NumberFriends = value;
                OnPropertyChanged();
            }
        }
        public string numberFollowers
        {
            get => NumberFollowers;
            set
            {
                NumberFollowers = value;
                OnPropertyChanged();
            }
        }
        public string numberMutualFriends
        {
            get => NumberMutualFriends;
            set
            {
                NumberMutualFriends = value;
                OnPropertyChanged();
            }
        }
        public string numberMales
        {
            get => NumberMales;
            set
            {
                NumberMales = value;
                OnPropertyChanged();
            }
        }
        public string numberFemales
        {
            get => NumberFemales;
            set
            {
                NumberFemales = value;
                OnPropertyChanged();
            }
        }
        public string numberOnlineFriends
        {
            get => NumberOnlineFriends;
            set
            {
                NumberOnlineFriends = value;
                OnPropertyChanged();
            }
        }
        public string numberPhotos
        {
            get => NumberPhotos;
            set
            {
                NumberPhotos = value;
                OnPropertyChanged();
            }
        }
        public string numberAudios
        {
            get => NumberAudios;
            set
            {
                NumberAudios = value;
                OnPropertyChanged();
            }
        }
        public string numberVideos
        {
            get => NumberVideos;
            set
            {
                NumberVideos = value;
                OnPropertyChanged();
            }
        }

  
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void Clear()
        {
            this.birthday = "";
            this.career = "";
            this.city = "";
            this.country = "";
            this.firstName = "";
            this.lastName = "";
            this.numberAudios = "";
            this.numberFemales = "";
            this.numberFollowers = "";
            this.numberFriends = "";
            this.numberMales = "";
            this.numberMutualFriends = "";
            this.numberPhotos = "";
            this.numberVideos = "";
            this.numberOnlineFriends = "";
            this.online = "";
            this.Photo = null;
            this.sex = "";
            this.userID = null;
        }
  
    }


    public class Account: INotifyPropertyChanged
    {
        public long uid { set; get; }
        public string token { set; get; }
        public string firstName { set; get; }
        public string lastName { set; get; }
        public Uri Photo { set; get; }

        string login { get; set; }
        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }

        string password { get; set; }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        string error { get; set; }
        public string Error
        {
            get => error;
            set
            {
                error = value;
                OnPropertyChanged("Error");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

}
