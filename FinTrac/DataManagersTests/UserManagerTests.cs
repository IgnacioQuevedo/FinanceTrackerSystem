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
            Repository myRepo = new Repository();

            string firstName = "Austin";
            string lastName = "Ford";
            string email = "austinFord@gmail.com";
            string password = "AustinF2003";
            string address = "NW 2nd Ave";
            User myUser = new User(firstName, lastName, email, password, address);
            
            myRepo.Accounts.Add(myUser);

            string firstName2 = "Kent";
            string lastName2 = "Beck";
            string emailUsed = "austinFord@gmail.com";
            string password2 = "JohnBeck1961";
            string address2 = "NW 3rd Ave";
            User incorrectUser = new User(firstName2, lastName2, emailUsed, password2, address2);
            
            
            UserManager.ValidateAddUser(incorrectUser);

        }


        #endregion
    }
}