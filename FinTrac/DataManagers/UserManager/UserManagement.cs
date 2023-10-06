using BusinessLogic.User_Components;
using System.Runtime.InteropServices;
using System.Globalization;

namespace DataManagers.UserManager
{
    public class UserManagement
    {

        private Repository _memoryDatabase;

        public UserManagement(Repository memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }


        #region addUser
        public void AddUser(User user)
        {
            if (ValidateAddUser(user))
            {
                FormatUserProperties(user);
                user.UserId = _memoryDatabase.Users.Count;
                _memoryDatabase.Users.Add(user);
            }
        }

        private void FormatUserProperties(User user)
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
                    throw new ExceptionUserManagement("Email already registered, impossible to create another account.");
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
                throw new ExceptionUserManagement("User not exists, maybe you have an error on the email or password?");
            }

            return existsUser;
        }

        #endregion


        #region Modify


        public void ModifyUser(User userNotUpdated, User userUpdated)
        {

            foreach (var user in _memoryDatabase.Users)
            {

                if (user.UserId.Equals(userNotUpdated.UserId))
                {

                    userNotUpdated.FirstName = userUpdated.FirstName;
                    userNotUpdated.LastName = userUpdated.LastName;
                    userNotUpdated.Password = userUpdated.Password;
                    userNotUpdated.Address = userUpdated.Address;

                }

            }

        }

        #endregion



    }

}