using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.Goal_Components;
using BusinessLogic.User_Components;
using Controller;
using DataManagers;

namespace ControllerTests
{
    [TestClass]
    public class ControllerCategoryTests
    {
        #region Initialize

        private GenericController _controller;
        private SqlContext _testDb;
        private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();

        private UserRepositorySql _userRepo;
        private UserDTO _userConnected;

        [TestInitialize]
        public void Initialize()
        {
            _testDb = _contextFactory.CreateDbContext();
            _userRepo = new UserRepositorySql(_testDb);
            _controller = new GenericController(_userRepo);
            
            _userConnected = new UserDTO("Jhon", "Sans", "jhonnie@gmail.com", "Jhoooniee123!", "");
            _userConnected.UserId = 1;

            
            _controller.RegisterUser(_userConnected);
            _controller.SetUserConnected(_userConnected.UserId);
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
        public void CreateCategoryMethodWithCorrectData_ShoudlAddCategoryToDb()
        {
            CategoryDTO dtoToAdd = new CategoryDTO("Food", StatusEnum.Enabled, TypeEnum.Income,_userConnected.UserId);
            Category categoryInDb = new Category();

            _controller.CreateCategory(dtoToAdd);

            categoryInDb= _testDb.Users.First().MyCategories.First();
            
            Assert.IsNotNull(categoryInDb.CategoryUser);
            Assert.AreEqual(dtoToAdd.CategoryUserId,categoryInDb.UserId);
            Assert.AreEqual(dtoToAdd.Name,categoryInDb.Name);
            Assert.AreEqual(dtoToAdd.Status,categoryInDb.Status);
            Assert.AreEqual(dtoToAdd.Type,categoryInDb.Type);

        }
        
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateCategoryMethodWithIncorrectData_ShouldThrowException()
        {
            CategoryDTO dtoToAdd = new CategoryDTO("", StatusEnum.Enabled, TypeEnum.Income,_userConnected.UserId);
            Category categoryInDb = new Category();

            _controller.CreateCategory(dtoToAdd);
            
        }
        
        
        
        
        
        
        
        
    }
}