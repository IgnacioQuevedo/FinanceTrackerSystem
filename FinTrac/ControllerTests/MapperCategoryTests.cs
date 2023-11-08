using BusinessLogic.Category_Components;
using BusinessLogic.Dto_Components;
using BusinessLogic.Enums;
using BusinessLogic.User_Components;
using Controller;
using DataManagers;
using Controller.Mappers;
using Mappers;

namespace ControllerTests
{
    [TestClass]
    public class MapperCategoryTests
    {
        #region Initialize

        private GenericController _controller;
        private SqlContext _testDb;
        private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();
        
        private UserRepositorySql _userRepo;


        [TestInitialize]
        public void Initialize()
        {
            _testDb = _contextFactory.CreateDbContext();
            _userRepo = new UserRepositorySql(_testDb);
            _controller = new GenericController(_userRepo);
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
        public void GivenCategory_ShouldBePossibleToConvertToCategoryDTO()
        {
            Category categoryToConvert = new Category("Food", StatusEnum.Enabled, TypeEnum.Income);

            CategoryDTO categoryDTOs = MapperCategory.ToCategoryDTO(categoryToConvert);
            
            Assert.IsInstanceOfType(categoryDTOs,typeof(CategoryDTO));
        }
    }
}