using BusinessLogic.User;

namespace DataManagers.UserManager
{
    public class UserManager
    {
        public static Repository DataBase { get; set; } = new Repository();

        public static bool ValidateAddUser(User userToAdd)
        {
            bool isUsed = false;

            foreach (var usuario in DataBase.Accounts)
            {
                if (usuario.Email.Equals(userToAdd.Email))
                {
                    isUsed = true;
                }
            }

            if (isUsed)
            {
                throw new ExceptionUserManager("Email already registered, impossible to create another account.");
            }

            return true;
        }
    }

}