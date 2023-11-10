using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
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

        #region Create Category

        [TestMethod]
        public void CreateCategoryMethodWithCorrectData_ShoudlAddCategoryToDb()
        {
            CategoryDTO dtoToAdd = new CategoryDTO("Food", StatusEnum.Enabled, TypeEnum.Income, _userConnected.UserId);
            Category categoryInDb = new Category();

            _controller.CreateCategory(dtoToAdd);

            categoryInDb = _testDb.Users.First().MyCategories.First();

            Assert.IsNotNull(categoryInDb.CategoryUser);
            Assert.AreEqual(dtoToAdd.UserId, categoryInDb.UserId);
            Assert.AreEqual(dtoToAdd.Name, categoryInDb.Name);
            Assert.AreEqual(dtoToAdd.Status, categoryInDb.Status);
            Assert.AreEqual(dtoToAdd.Type, categoryInDb.Type);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateCategoryMethodWithIncorrectData_ShouldThrowException()
        {
            CategoryDTO dtoToAdd = new CategoryDTO("", StatusEnum.Enabled, TypeEnum.Income, _userConnected.UserId);

            _controller.CreateCategory(dtoToAdd);
        }

        #endregion

        #region Find Category

        [TestMethod]
        public void GivenCategoryDTO_ShouldBePossibleToFindItOnDb()
        {
            CategoryDTO categoryDTO = new CategoryDTO("Food", StatusEnum.Enabled, TypeEnum.Income, 1);
            categoryDTO.CategoryId = 1;

            _controller.CreateCategory(categoryDTO);
            Category categoryFound = _controller.FindCategory(categoryDTO.CategoryId);

            Assert.AreEqual(categoryDTO.CategoryId, categoryFound.CategoryId);
            Assert.AreEqual(categoryDTO.UserId, categoryFound.UserId);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GivenCategoryDTOThatIsNotInDb_WhenTryingToFound_ShouldThrowException()
        {
            CategoryDTO categoryDTO = new CategoryDTO("Food", StatusEnum.Enabled, TypeEnum.Income, 1);
            categoryDTO.CategoryId = 1;

            _controller.FindCategory(categoryDTO.CategoryId);
        }

        #endregion

        #region Update Category

        [TestMethod]
        public void GivenCategoryDTOToUpdate_ShouldBeUpdatedInDb()
        {
            CategoryDTO categoryDto = new CategoryDTO("Food", StatusEnum.Enabled, TypeEnum.Income, 1);
            categoryDto.CategoryId = 1;
            _controller.CreateCategory(categoryDto);

            CategoryDTO categoryDtoWithUpdates = new CategoryDTO("Party", StatusEnum.Disabled, TypeEnum.Outcome, 1);
            categoryDtoWithUpdates.CategoryId = 1;

            _controller.UpdateCategory(categoryDtoWithUpdates);

            Category categoryInDbWithSupossedChanges = _controller.FindCategory(categoryDto.CategoryId);

            Assert.AreEqual(categoryInDbWithSupossedChanges.CategoryId, categoryDtoWithUpdates.CategoryId);
            Assert.AreEqual(categoryInDbWithSupossedChanges.UserId, categoryDtoWithUpdates.UserId);
            Assert.AreEqual(categoryInDbWithSupossedChanges.Name, categoryDtoWithUpdates.Name);
            Assert.AreEqual(categoryInDbWithSupossedChanges.Status, categoryDtoWithUpdates.Status);
            Assert.AreEqual(categoryInDbWithSupossedChanges.Type, categoryDtoWithUpdates.Type);
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TryingToUpdateWithoutAnyChanges_ShouldThrowException()
        {
            CategoryDTO categoryRegistered =
                new CategoryDTO("Food", StatusEnum.Enabled, TypeEnum.Income, _userConnected.UserId);
            categoryRegistered.CategoryId = 1;

            _controller.CreateCategory(categoryRegistered);

            CategoryDTO categoryWithoutUpdates =
                new CategoryDTO("Food", StatusEnum.Enabled, TypeEnum.Income, _userConnected.UserId);
            categoryWithoutUpdates.CategoryId = categoryRegistered.CategoryId;
            _controller.UpdateCategory(categoryWithoutUpdates);
        }
        #endregion

        [TestMethod]
        public void GivenACategoryDTOToDelete_ShouldBeDeletedOnDb()
        {
            CategoryDTO categoryDto = new CategoryDTO("Food", StatusEnum.Enabled, TypeEnum.Income, 1);
            categoryDto.CategoryId = 1;
            _controller.CreateCategory(categoryDto);
            List<Category> categoryListsOfUser = _testDb.Users.First().MyCategories;
            
            int amountOfCategoriesInDb = categoryListsOfUser.Count;
            _controller.DeleteCategory(categoryDto);
            int amountOfCategoriesPostDelete = categoryListsOfUser.Count;
            
            Assert.AreEqual(amountOfCategoriesInDb -1, amountOfCategoriesPostDelete);
        }
    }
}