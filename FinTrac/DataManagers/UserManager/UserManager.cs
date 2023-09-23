using BusinessLogic.User;

namespace DataManagers.UserManager
{
    public class UserManager
    {
        public static Repository DataBase { get; set; } = new Repository();


        public static void Add(User user)
        {

            DataBase.Accounts.Add(user);

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
                if (someUser.Email.Equals(UserEmail))
                {
                    throw new ExceptionUserManager("Email already registered, impossible to create another account.");
                }
            }
        }
    }

}