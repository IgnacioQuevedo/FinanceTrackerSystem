using BusinessLogic.User_Components;
using DataManagers;
using DataManagers.UserManager;

namespace DataManagersTests
{
    [TestClass]
    public class UserRepositorySqlTests
    {
        #region Initialize

        private UserRepositorySql _userRepo;
        private SqlContext _testDb;
        private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();
        private User _genericUser;


        [TestInitialize]
        public void TestInitialize()
        {
            _testDb = _contextFactory.CreateDbContext();
            _userRepo = new UserRepositorySql(_testDb);
            _genericUser = new User("Jhon", "Sans", "jhonny@gmail.com", "Jhooony12345", "");
        }

        #endregion

        #region Creation and Necessary Validations
        [TestMethod]
        public void CreateMethodWithCorrectValues_ShouldAddNewUser()
        {
            User userToAdd = new User("Kenny", "Dock", "kennies@gmail.com", "KennieDock222", "North Av");
            User userInDb = new User();

            _userRepo.Create(userToAdd);

            userInDb = _testDb.Users.First();
            Assert.AreEqual(userToAdd, userInDb);
        }
        
        [TestMethod]
        public void GivenNotRegisteredEmail_ShouldBeAllGood()
        {
            User userToCheckEmail = new User("Kent", "Beck", "michsanta@gmail.com", "JohnBeck1961", "NW 3rd Ave");

            _userRepo.EmailUsed(userToCheckEmail.Email);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ExceptionUserRepository))]
        public void GivenAlreadyRegisteredEmail_ShouldReturnException()
        {
            User userToAdd = new User("Mich", "Santa", "michsanta@gmail.com", "Mich123456789", "North Av 23");
            _userRepo.Create(userToAdd);

            User incorrectUser = new User("Kent", "Beck", "michsanta@gmail.com", "JohnBeck1961", "NW 3rd Ave");

            _userRepo.EmailUsed(incorrectUser.Email);
        }
        
        [TestMethod]
        public void GivenUserThatWantsToLogin_ShouldBePossibleToCheckIfHisLoginDataIsCorrect()
        {
            _userRepo.Create(_genericUser);
            
            Assert.IsTrue(_userRepo.userRegistered(_genericUser));
        }
        
        [TestMethod]
        public void GivenUserThatWantsToLoginButIsNotRegistered_ShouldReturnFalse()
        {
            User userNotRegistered = new User("Jhon", "Camaleon", "jhonnya@gmail.com","LittleJhonny123", "");
            Assert.IsFalse(_userRepo.userRegistered(userNotRegistered));
        }
        
        #endregion
        
        [TestMethod]
        public void GivenAspectsOfUserToUpdate_ShouldBeUpdate()
        {
            string firstName = "Michael";
            string lastName = "Santa";
            string passwordModified = "MichaelSanta1234";
            string address = "NW 2nd Ave";

            User userUpdated = new User("Jhonnyx", "Sanz", "jhonny@gmail.com", "Jhooony12345", "NW 2nd Ave");
            
            _userRepo.Update(userUpdated);

            Assert.AreEqual(firstName, userUpdated.FirstName);
            Assert.AreEqual(lastName, userUpdated.LastName);
            Assert.AreEqual(passwordModified, userUpdated.Password);
            Assert.AreEqual(address, userUpdated.Address);
        }
    }
}