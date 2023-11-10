using System.Diagnostics.CodeAnalysis;
using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
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
        
        private Category _category;
        private Category _category2;
        private List<Category> _categoryList;
        private List<CategoryDTO> categoryDtoList;
        
        [TestInitialize]
        public void Initialize()
        {
            _testDb = _contextFactory.CreateDbContext();
            _userRepo = new UserRepositorySql(_testDb);
            _controller = new GenericController(_userRepo);
            
            
            _category = new Category("Food", StatusEnum.Enabled, TypeEnum.Outcome);
            _category.CategoryId = 1;
            _category.UserId = 1;

            _category2 = new Category("Party", StatusEnum.Enabled, TypeEnum.Outcome);

            _categoryList = new List<Category>();

            _categoryList.Add(_category);
            _categoryList.Add(_category2);

            categoryDtoList = new List<CategoryDTO>();
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
            CategoryDTO categoryDTO_ToConvert = new CategoryDTO("foood", StatusEnum.Enabled, TypeEnum.Income, 1);
            categoryDTO_ToConvert.CategoryId = 1;

            Category generatedCategory = MapperCategory.ToCategory(categoryDTO_ToConvert);

            Assert.IsInstanceOfType(generatedCategory, typeof(Category));
            Assert.AreEqual(categoryDTO_ToConvert.CategoryId, generatedCategory.CategoryId);
            Assert.AreEqual(categoryDTO_ToConvert.Name, generatedCategory.Name);
            Assert.AreEqual(categoryDTO_ToConvert.Status, generatedCategory.Status);
            Assert.AreEqual(categoryDTO_ToConvert.Type, generatedCategory.Type);
            Assert.AreEqual(categoryDTO_ToConvert.UserId, generatedCategory.UserId);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionMapper))]
        public void GivenCategoryDTOWithIncorrectData_ShouldThrowException()
        {
            CategoryDTO categoryDTO_ToConvert = new CategoryDTO("", StatusEnum.Enabled, TypeEnum.Income, 1);
            categoryDTO_ToConvert.CategoryId = 1;

            MapperCategory.ToCategory(categoryDTO_ToConvert);
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
            Assert.AreEqual(categoryToConvert.CategoryId, categoryDTO.CategoryId);
            Assert.AreEqual(categoryToConvert.Name, categoryDTO.Name);
            Assert.AreEqual(categoryToConvert.Status, categoryDTO.Status);
            Assert.AreEqual(categoryToConvert.Type, categoryDTO.Type);
        }

        #endregion

        [TestMethod]
        public void GivenCategoryList_ShouldConvertItToCategoryDTOList()
        {
            
            categoryDtoList = MapperCategory.ToListOfCategoryDTO(_categoryList);

            Assert.IsInstanceOfType(categoryDtoList[0], typeof(CategoryDTO));
            Assert.IsInstanceOfType(categoryDtoList[1], typeof(CategoryDTO));

            Assert.AreEqual(_category.Name, categoryDtoList[0].Name);
            Assert.AreEqual(_category.Status, categoryDtoList[0].Status);
            Assert.AreEqual(_category.Type, categoryDtoList[0].Type);
            Assert.AreEqual(_category.CreationDate, categoryDtoList[0].CreationDate);
            Assert.AreEqual(_category.UserId, categoryDtoList[0].UserId);
            Assert.AreEqual(_category.CategoryId, categoryDtoList[0].CategoryId);
        }

        [TestMethod]
        public void GivenCategoryDTOList_ShouldConvertItToCategoryList()
        {

            List<CategoryDTO> categoryDTOList = new List<CategoryDTO>();
            categoryDTOList.Add(MapperCategory.ToCategoryDTO(_category));
            categoryDTOList.Add(MapperCategory.ToCategoryDTO(_category2));

            List<Category> categoryList = new List<Category>();

            MapperCategory.ToListOfCategory(categoryDTOList);
            
            Assert.IsInstanceOfType(categoryList[0],typeof(Category));
            Assert.IsInstanceOfType(categoryList[1],typeof(Category));
        }
    }
}