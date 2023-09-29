using BusinessLogic.User;
using System.Runtime.InteropServices;
using System.Globalization;

namespace DataManagers.UserManager
{
    public class UserManager
    {

        private Repository _memoryDatabase;

        public UserManager(Repository memoryDatabase) 
        {
            _memoryDatabase = memoryDatabase; 
        }


        #region addUser
        public void Add(User user)
        {
            if (ValidateAddUser(user))
            {
                FormatProperties(user);
                user.Id = _memoryDatabase.Users.Count + 1;                                                                                                              
                _memoryDatabase.Users.Add(user);
            }
        }

        private void FormatProperties(User user)
        {
            string emailFormated = user.Email.ToLower();
            string firstNameFormated = char.ToUpper(user.FirstName[0]) + user.FirstName.Substring(1).ToLower();
            string lastNameFormated = char.ToUpper(user.LastName[0]) + user.LastName.Substring(1).ToLower();
            //Now firstName and lastName has only in capital letters the first one, furthermore we deny capital letters in email.

            user.Email = emailFormated;
            user.FirstName = firstNameFormated;
            user.LastName = lastNameFormated;
        }

        public bool ValidateAddUser(User userToAdd)
        {
            EmailUsed(userToAdd.Email);
            return true;
        }
        private void EmailUsed(string UserEmail)
        {
            foreach (var someUser in _memoryDatabase.Users)
            {
                if (someUser.Email.Equals(UserEmail.ToLower()))
                {
                    throw new ExceptionUserManager("Email already registered, impossible to create another account.");
                }
            }
        }
        #endregion


        #region Login


        public bool Login(User userToBeLogged)
        {
            bool existsUser = false;
            string userEmail = userToBeLogged.Email.ToLower();
            string userPassword = userToBeLogged.Password;

            foreach (var account in _memoryDatabase.Users)

            {
                if (userEmail.Equals(account.Email))

                {
                    if (userPassword.Equals(account.Password))
                    {
                        existsUser = true;
                    }
                    
                }
            }

            if (!existsUser)
            {
                throw new ExceptionUserManager("User not exists, maybe you have an error on the email or password?");
            }

            return existsUser;
        }

        #endregion


        #region Modify


        public static void Modify(User userNotUpdated,User userUpdated)
        {

            userNotUpdated.FirstName = userUpdated.FirstName;
            userNotUpdated.LastName = userUpdated.LastName;
            userNotUpdated.Email = userUpdated.Email;
            userNotUpdated.Password = userUpdated.Password;
            userNotUpdated.Address = userUpdated.Address;

        }

        #endregion



    }

}