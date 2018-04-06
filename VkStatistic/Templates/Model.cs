using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VkStatistic.Templates
{
    public class UserData : INotifyPropertyChanged
    {

        public UserData()
        {}
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
                sex = value;
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


        public string numberFriends { set; get; }
        public string numberFollowers { set; get; }
        public string numberMutualFriends { set; get; }
        public string numberMales { set; get; }
        public string numberFemales { set; get; }
        public string numberOnlineFriends { set; get; }
        public string numberPhotos { get; set; }
        public string numberAudios { get; set; }
        public string numberVideos { get; set; }

        public Uri test { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }


    public class Account
    {
        public long uid { set; get; }
        public string token { set; get; }
        public string firstName { set; get; }
        public string lastName { set; get; }
        public Uri Photo { set; get; }
    }

}
