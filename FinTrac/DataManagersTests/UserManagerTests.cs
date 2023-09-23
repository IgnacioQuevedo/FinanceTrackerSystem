using BusinessLogic.User;
using DataManagers;
using DataManagers.UserManager;

namespace DataManagersTests
{
    [TestClass]
    public class UserManagerTests
    {

        private User genericUser;

        [TestInitialize]
        public void TestInitialize()
        {
            string firstName = "Austin";
            string lastName = "Ford";
            string email = "austinFord@gmail.com";
            string password = "AustinF2003";
            string address = "NW 2nd Ave";

            genericUser = new User(firstName, lastName, email, password, address);
        }


        #region AddUser

        [TestMethod]

        public void GivenUserToAddToRepositoryAccounts_ValidationShouldReturnTrue()
        {
            Assert.AreEqual(true, UserManager.ValidateAddUser(genericUser));
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
            int numberOfUsersAddedBefore = UserManager.DataBase.Accounts.Count;

            UserManager.Add(myUser);

            Assert.AreEqual(numberOfUsersAddedBefore + 1,UserManager.DataBase.Accounts.Count);

        }
        #endregion

        #region loginUser

        [TestMethod]

        public void GivenUserAlreadyAdded_ShouldBePossibleToLogin()
        {

            Assert.AreEqual(true,UserManager.Login(genericUser));

        }

        #endregion


    }
}