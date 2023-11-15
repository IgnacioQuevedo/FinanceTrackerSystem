using System.Net.Mime;
using BusinessLogic.Account_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.Transaction_Components;
using BusinessLogic.User_Components;
using DataManagers;


namespace DataManagersTests
{
    [TestClass]
    public class HelperTests
    {
        #region Initialize

        private UserRepositorySql _userRepo;
        private SqlContext _testDb;
        private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();
        private User _genericUser;
        private UserDTO _genericUserDTO;
        private UserLoginDTO _genericUserLoginDTO;


        [TestInitialize]
        public void TestInitialize()
        {
            _testDb = _contextFactory.CreateDbContext();
            _userRepo = new UserRepositorySql(_testDb);
            _genericUser = new User("Jhon", "Sans", "jhonny@gmail.com", "Jhooony12345", "");
            _genericUserDTO = new UserDTO("Jhon", "Sans", "jhonny@gmail.com", "Jhooony12345", "");
            _genericUserLoginDTO = new UserLoginDTO(1, "jhonny@gmail.com", "Jhooony12345");
        }

        #endregion

        #region Cleanup

        [TestCleanup]
        public void CleanUp()
        {
            _testDb.Database.EnsureDeleted();
        }

        #endregion


        [TestMethod]
        public void GivenTwoSimplePropertiesThatAreEqual_AreTheSameObject_ShouldReturnTrue()
        {
            int number1 = 10;
            int number2 = 10;
            
            Assert.AreEqual(Helper.AreTheSameObject(number1,number2));
            

        }
        
        
    }
}