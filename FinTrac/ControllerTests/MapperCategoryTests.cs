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

        #region ToCategory

        [TestMethod]
        public void GivenCategoryDTOWithCorrectData_ShouldBePossibleToConvertToCategory()
        {
            CategoryDTO categoryDTO_ToConvert = new CategoryDTO("foood", StatusEnum.Enabled, TypeEnum.Income);
            categoryDTO_ToConvert.Id = 1;

            Category generatedCategory = MapperCategory.ToCategory(categoryDTO_ToConvert);

            Assert.IsInstanceOfType(generatedCategory, typeof(Category));
            Assert.AreEqual(categoryDTO_ToConvert.Id, generatedCategory.CategoryId);
            Assert.AreEqual(categoryDTO_ToConvert.Name, generatedCategory.Name);
            Assert.AreEqual(categoryDTO_ToConvert.Status, generatedCategory.Status);
            Assert.AreEqual(categoryDTO_ToConvert.Type, generatedCategory.Type);
        }
        
        #endregion

        #region ToCategoryDTO

        [TestMethod]
        public void GivenCategory_ShouldBePossibleToConvertToCategoryDTO()
        {
            Category categoryToConvert = new Category("Food", StatusEnum.Enabled, TypeEnum.Income);
            categoryToConvert.CategoryId = 1;

            CategoryDTO categoryDTO = MapperCategory.ToCategoryDTO(categoryToConvert);

            Assert.IsInstanceOfType(categoryDTO, typeof(CategoryDTO));
            Assert.AreEqual(categoryToConvert.CategoryId, categoryDTO.Id);
            Assert.AreEqual(categoryToConvert.Name, categoryDTO.Name);
            Assert.AreEqual(categoryToConvert.Status, categoryDTO.Status);
            Assert.AreEqual(categoryToConvert.Type, categoryDTO.Type);
        }

        #endregion
    }
}