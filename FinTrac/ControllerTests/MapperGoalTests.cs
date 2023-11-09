using BusinessLogic.Category_Components;
using BusinessLogic.Goal_Components;
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
    public class MapperGoalTests
    {
        #region Initialize

        private GenericController _controller;
        private SqlContext _testDb;
        private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();

        private UserRepositorySql _userRepo;


        private List<Category> _genericListOfCategories;
        private Category _category1;
        private Category _category2;

        private CategoryDTO _categoryDTO1;
        private CategoryDTO _categoryDTO2;

        private Goal _goalToConvert;
        private GoalDTO GoalDTOToConvert;

        [TestInitialize]
        public void Initialize()
        {
            _testDb = _contextFactory.CreateDbContext();
            _userRepo = new UserRepositorySql(_testDb);
            _controller = new GenericController(_userRepo);


            _genericListOfCategories = new List<Category>();
            _category1 = new Category("Food", StatusEnum.Enabled, TypeEnum.Outcome);
            _category2 = new Category("Party", StatusEnum.Enabled, TypeEnum.Outcome);
            _category1.CategoryId = 1;
            _category2.CategoryId = 1;

            _genericListOfCategories.Add(_category1);
            _genericListOfCategories.Add(_category2);

            _categoryDTO1 = new CategoryDTO("Food", StatusEnum.Enabled, TypeEnum.Outcome, 0);
            _categoryDTO2 = new CategoryDTO("Party", StatusEnum.Enabled, TypeEnum.Outcome, 0);
            _categoryDTO1.CategoryId = 1;
            _categoryDTO2.CategoryId = 1;

            _goalToConvert = new Goal("Less party", 100, _genericListOfCategories);
            _goalToConvert.GoalId = 1;
        }

        #endregion

        #region Cleanup

        [TestCleanup]
        public void CleanUp()
        {
            _testDb.Database.EnsureDeleted();
        }

        #endregion

        #region To GoalDTO

        [TestMethod]
        public void GivenGoal_ShouldBePossibleToConvertToGoalDTO()
        {
            GoalDTO goalDTO = MapperGoal.ToGoalDTO(_goalToConvert);

            Assert.IsInstanceOfType(goalDTO, typeof(GoalDTO));
            Assert.AreEqual(goalDTO.GoalId, _goalToConvert.GoalId);
            Assert.AreEqual(goalDTO.Title, _goalToConvert.Title);
            Assert.AreEqual(goalDTO.MaxAmountToSpend, _goalToConvert.MaxAmountToSpend);
            Assert.AreEqual(goalDTO.CurrencyOfAmount, _goalToConvert.CurrencyOfAmount);
            Assert.AreEqual(goalDTO.UserId, _goalToConvert.UserId);
            Assert.IsTrue(Helper.AreTheSameObject(goalDTO.CategoriesOfGoalDTO[0], _categoryDTO1));
            Assert.IsTrue(Helper.AreTheSameObject(goalDTO.CategoriesOfGoalDTO[1], _categoryDTO2));
        }

        #endregion

        [TestMethod]
        public void GivenGoalDTOWithCorrectData_ShouldBePossibleToConvertToGoal()
        {
            Goal goalConverted = MapperGoal.ToGoal(goalDTOToConvert);

            Assert.IsInstanceOfType(goalConverted, typeof(Goal));
            Assert.AreEqual(categoryDTO_ToConvert.CategoryId, generatedCategory.CategoryId);
            Assert.AreEqual(categoryDTO_ToConvert.Name, generatedCategory.Name);
            Assert.AreEqual(categoryDTO_ToConvert.Status, generatedCategory.Status);
            Assert.AreEqual(categoryDTO_ToConvert.Type, generatedCategory.Type);
            Assert.AreEqual(categoryDTO_ToConvert.CategoryUserId, generatedCategory.UserId);
        }


    }
}