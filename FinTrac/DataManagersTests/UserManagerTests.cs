using BusinessLogic.User;
using DataManagers;
using DataManagers.UserManager;

namespace DataManagersTests
{
    [TestClass]
    public class UserManagerTests
    {

        #region AddUser

        [TestMethod]
        public void GivenUserToAddToRepositoryAccounts_ValidationShouldReturnTrue()
        {
            string firstName = "Austin";
            string lastName = "Ford";
            string email = "austinFord@gmail.com";
            string password = "AustinF2003";
            string address = "NW 2nd Ave";

            User myUser = new User(firstName, lastName, email, password, address);

            Assert.AreEqual(true, UserManager.ValidateAddUser(myUser));
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionUserManager))]
        public void GivenAlreadyRegisteredEmail_ShouldReturnException()
        {
            string firstName = "Michael";
            string lastName = "Santa";
            string email = "michSanta@gmail.com";
            string password = "AustinF2003";
            string address = "NW 2nd Ave";
            User myUser = new User(firstName, lastName, email, password, address);

            UserManager.DataBase.Accounts.Add(myUser);

            string firstName2 = "Kent";
            string lastName2 = "Beck";
            string emailUsed = "michSanta@gmail.com";
            string password2 = "JohnBeck1961";
            string address2 = "NW 3rd Ave";
            User incorrectUser = new User(firstName2, lastName2, emailUsed, password2, address2);
           
            UserManager.ValidateAddUser(incorrectUser);

        }

        [TestMethod]

        public void GivenUserNotRegistered_ShouldRegisterIt()
        {

            string firstName = "Franklin";
            string lastName = "Oddisey";
            string email = "FranklinOddisey@gmail.com";
            string password = "Frank2003!!";
            string address = "NW 5nd Ave";

            User myUser = new User(firstName, lastName, email, password, address);

            UserManager.Add(myUser);


        }
        #endregion
    }
}