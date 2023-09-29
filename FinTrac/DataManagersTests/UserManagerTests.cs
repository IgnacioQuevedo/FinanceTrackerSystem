using BusinessLogic.User;
using DataManagers;
using DataManagers.UserManager;

namespace DataManagersTests
{
    [TestClass]
    public class UserManagerTests
    {


        #region initializingAspects
        private User genericUser;
        private UserManager userManager;
        private Repository memoryDatabase;

        [TestInitialize]
        public void TestInitialize()
        {
            string firstName = "Michael";
            string lastName = "Santa";
            string email = "michSanta@gmail.com";
            string password = "AustinF2003";
            string address = "NW 2nd Ave";

            memoryDatabase = new Repository();
            userManager = new UserManager(memoryDatabase);
            genericUser = new User(firstName, lastName, email, password, address);
            userManager.Add(genericUser);
        }

        #endregion

        #region AddUser

        [TestMethod]

        public void GivenUserToAddToUsers_ValidationShouldReturnTrue()
        {
            string firstName = "Austin";
            string lastName = "Ford";
            string email = "austinFord@gmail.com";
            string password = "AustinF2003";
            string address = "NW 2nd Ave";
            User myUser = new User(firstName, lastName, email, password, address);
            Assert.AreEqual(true, userManager.ValidateAddUser(myUser));
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionUserManager))]
        public void GivenAlreadyRegisteredEmail_ShouldReturnException()
        {

            string firstName2 = "Kent";
            string lastName2 = "Beck";
            string emailUsed = "michSanta@gmail.com";
            string password2 = "JohnBeck1961";
            string address2 = "NW 3rd Ave";
            User incorrectUser = new User(firstName2, lastName2, emailUsed, password2, address2);

            userManager.ValidateAddUser(incorrectUser);

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
            int numberOfUsersAddedBefore = memoryDatabase.Users.Count;

            userManager.Add(myUser);

            Assert.AreEqual(numberOfUsersAddedBefore + 1, memoryDatabase.Users.Count);

        }
        #endregion

        #region LoginUser

        [TestMethod]

        public void GivenUserAlreadyAdded_ShouldBePossibleToLogin()
        {
            Assert.AreEqual(true, userManager.Login(genericUser));

        }
        [TestMethod]
        [ExpectedException(typeof(ExceptionUserManager))]
        public void GivenUserNotAdded_ShouldThrowException()
        {
            User myUser = new User("Ronnie", "Belgman", "ronnieBelgam@gmail.com", "RonnieMan2003", "asd");
            userManager.Login(myUser);
        }

        #endregion

        #region Modify

        [TestMethod]
        public void GivenAspectsOfUserToChange_ShouldBeChanged()
        {

            string firstName = "Michael";
            string lastName = "Santa";
            string emailModified = "michTheBest@gmail.com";
            string passwordModified = "MichaelSanta1234";
            string address = "NW 2nd Ave";

            User userUpdated = new User(firstName, lastName, emailModified, passwordModified, address);

            UserManager.Modify(genericUser, userUpdated);

            Assert.AreEqual(firstName, genericUser.FirstName);
            Assert.AreEqual(lastName, genericUser.LastName);
            Assert.AreEqual(emailModified, genericUser.Email);
            Assert.AreEqual(passwordModified, genericUser.Password);
            Assert.AreEqual(address, genericUser.Address);
        }


        #endregion

        #region IdUser

        [TestMethod]
        public void GivenUserToAdd_ShouldGenerateAnId()
        {
            Assert.AreEqual(genericUser.Id,memoryDatabase.Users.Count);
        }

        #endregion


    }
}