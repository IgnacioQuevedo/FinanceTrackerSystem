using BusinessLogic.User;
using System.Runtime.InteropServices;

namespace DataManagers.UserManager
{
    public class UserManager
    {
        public static Repository DataBase { get; set; } = new Repository();


        #region addUser
        public static void Add(User user)
        {
            if (ValidateAddUser(user))
            {
                DataBase.Accounts.Add(user);
            }
        }
        public static bool ValidateAddUser(User userToAdd)
        {
            EmailUsed(userToAdd.Email);
            return true;
        }
        private static void EmailUsed(string UserEmail)
        {
            foreach (var someUser in DataBase.Accounts)
            {
                if (someUser.Email.ToLower().Equals(UserEmail.ToLower()))
                {
                    throw new ExceptionUserManager("Email already registered, impossible to create another account.");
                }
            }
        }
        #endregion


        #region Login


        public static bool Login(User userToBeLogged)
        {
            foreach (var account in DataBase.Accounts)

            {
                if (account.Email.ToLower().Equals(userToBeLogged.Email.ToLower()) && account.Password.Equals(userToBeLogged.Password))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion






    }

}