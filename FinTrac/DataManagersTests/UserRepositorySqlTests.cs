using BusinessLogic.User_Components;
using DataManagers;


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

        #region Cleanup

        [TestCleanup]
        public void CleanUp()
        {
            _testDb.Database.EnsureDeleted();
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

        #endregion

        #region Email Mapping

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

        #endregion

        #region Login

        [TestMethod]
        public void GivenUserThatWantsToLogin_ShouldBePossibleToCheckIfHisLoginDataIsCorrect()
        {
            _userRepo.Create(_genericUser);

            Assert.IsTrue(_userRepo.UserRegistered(_genericUser));
        }

        [TestMethod]
        public void GivenUserThatWantsToLoginButIsNotRegistered_ShouldReturnFalse()
        {
            User userNotRegistered = new User("Jhon", "Camaleon", "jhonnya@gmail.com", "LittleJhonny123", "");
            Assert.IsFalse(_userRepo.UserRegistered(userNotRegistered));
        }

        #endregion

        #region Update

        [TestMethod]
        public void GivenAspectsOfUserToUpdate_ShouldBeUpdate()
        {
            _userRepo.Create(_genericUser);

            User userUpdated = new User("Jhonnyx", "Sanz", "jhonny@gmail.com", "Jhooony12345", "NW 2nd Ave");
            userUpdated.UserId = _genericUser.UserId;

            _userRepo.Update(userUpdated);

            User userInDb = _testDb.Users.First();

            Assert.AreEqual(userUpdated.FirstName, userInDb.FirstName);
            Assert.AreEqual(userUpdated.LastName, userInDb.LastName);
            Assert.AreEqual(userUpdated.Password, userInDb.Password);
            Assert.AreEqual(userUpdated.Address, userInDb.Address);
        }

        #endregion

        [TestMethod]
        public void WhenUserIsCreated_AnIdMustBeAssigned()
        {
            _userRepo.Create(_genericUser);
            Assert.AreEqual(1,_testDb.Users.First().UserId);
        }
        
    }
}