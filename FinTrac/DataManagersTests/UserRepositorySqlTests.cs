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
        
    }
}