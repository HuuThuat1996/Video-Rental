using VideoRentalStoreSystem.DAL.DBContextEF;
using VideoRentalStoreSystem.DAL.Repositories;

namespace VideoRentalStoreSystem.BLL
{
    /// <summary>
    /// 
    /// </summary>
    public static class SecurityService
    {
        private static bool isLogin = false;
        private static bool isBlock = false;
        private static int countLoginTimes = 1;
        private static string position = "";
        private static UserRepository userRepository = new UserRepository(new DBVRContext());

        public static void Login(string userName, string password)
        {
            if (countLoginTimes > 5)
            {
                isBlock = true;
                return;
            }
            User user = userRepository.Get(userName, password);
            if(user != null)
            {
                isLogin = true;
                position = user.position;
            }
            countLoginTimes++;
        }
        public static string getPosition()
        {
            return position;
        }
        public static void Logout()
        {
            isLogin = false;
            isBlock = false;
            countLoginTimes = 1;
        }

        public static bool IsBlock()
        {
            return isBlock;
        }

        public static bool IsLogin()
        {
            return isLogin;
        }
    }
}
