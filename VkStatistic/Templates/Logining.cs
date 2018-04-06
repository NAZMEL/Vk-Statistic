using System.Windows;
using VkNet;
using VkNet.Enums.Filters;



namespace VkStatistic.Templates
{
    public class LoginingUser
    {
        VkApi _vkApi;
        static string login { get; set; }
        static string password { get; set; }
        long? userId { get; set; }
        static string token;

        Settings scope = Settings.All;

        public bool status = false;

        public LoginingUser(string _login, string _password, ref VkApi _vkApi, ulong appId = 6410920)
        {
            login = _login;
            password = _password;
            this._vkApi = _vkApi;

            ApiAuthParams vkAuth = new ApiAuthParams
            {
                ApplicationId = appId,
                Login = login,
                Password = password,
                Settings = scope
            };

            try
            {
                if (_vkApi == null) _vkApi = new VkApi();

                _vkApi.Authorize(vkAuth);
                token = _vkApi.Token;
                userId = _vkApi.UserId;
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


        }

    }
}
