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


        [TestInitialize]
        public void TestInitialize()
        {
            _testDb = _contextFactory.CreateDbContext();
            _userRepo = new UserRepositorySql(_testDb);
        }

        #endregion
        
        [TestMethod]
        public void CreateMethodWithCorrectValues_ShouldAddNewUser()
        {
            User userToAdd = new User("Kenny", "Dock", "kennies@gmail.com", "KennieDock222", "North Av");
            userToAdd.UserId = 1;
            
            User userInDb = new User();
            
            _userRepo.Create(userToAdd);
            
            userInDb = _testDb.Users.First();
            Assert.AreEqual(userToAdd,userInDb);






        }
        
        
        
        
        
        
        

    }

}