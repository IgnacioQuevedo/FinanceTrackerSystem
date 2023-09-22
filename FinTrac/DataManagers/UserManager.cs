using BusinessLogic.User;
namespace DataManagers
{
    public class UserManager
    {
        private static Repository dataBase = new Repository();

        public static bool ValidateAddUser(User userToAdd)
        {

            dataBase.Accounts.Add(userToAdd);

            return true;
        }


    }
}