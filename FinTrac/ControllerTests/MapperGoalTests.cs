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
        private List<CategoryDTO> _genericListOfCategoriesDTO;
        private Category _category1;
        private Category _category2;

        private CategoryDTO _categoryDTO1;
        private CategoryDTO _categoryDTO2;

        private Goal _goalToConvert;
        private GoalDTO _goalDTOToConvert;

        [TestInitialize]
        public void Initialize()
        {
            _testDb = _contextFactory.CreateDbContext();
            _userRepo = new UserRepositorySql(_testDb);
            _controller = new GenericController(_userRepo);

            _genericListOfCategories = new List<Category>();
            _genericListOfCategoriesDTO = new List<CategoryDTO>();

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

            _genericListOfCategoriesDTO.Add(_categoryDTO1);
            _genericListOfCategoriesDTO.Add(_categoryDTO2);

            _goalDTOToConvert = new GoalDTO("Less party", 100, CurrencyEnum.UY, _genericListOfCategoriesDTO, 0);

            _goalToConvert = new Goal("Less party", 100, _genericListOfCategories);
            _goalToConvert.GoalId = 1;
            _goalDTOToConvert.UserId = 0;
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

        #region To Goal

        [TestMethod]
        public void GivenGoalDTOWithCorrectData_ShouldBePossibleToConvertToGoal()
        {
            Goal goalConverted = MapperGoal.ToGoal(_goalDTOToConvert, _genericListOfCategories);

            Assert.IsInstanceOfType(goalConverted, typeof(Goal));
            Assert.AreEqual(_goalDTOToConvert.GoalId, goalConverted.GoalId);
            Assert.AreEqual(_goalDTOToConvert.Title, goalConverted.Title);
            Assert.AreEqual(_goalDTOToConvert.CurrencyOfAmount, goalConverted.CurrencyOfAmount);
            Assert.AreEqual(_goalDTOToConvert.MaxAmountToSpend, goalConverted.MaxAmountToSpend);
            Assert.AreEqual(_goalDTOToConvert.UserId, goalConverted.UserId);

            Assert.AreEqual(_goalDTOToConvert.CategoriesOfGoalDTO[0].Name, goalConverted.CategoriesOfGoal[0].Name);
            Assert.AreEqual(_goalDTOToConvert.CategoriesOfGoalDTO[0].CategoryId, goalConverted.CategoriesOfGoal[0].CategoryId);
            Assert.AreEqual(_goalDTOToConvert.CategoriesOfGoalDTO[0].Status, goalConverted.CategoriesOfGoal[0].Status);
            Assert.AreEqual(_goalDTOToConvert.CategoriesOfGoalDTO[0].CreationDate, goalConverted.CategoriesOfGoal[0].CreationDate);
            Assert.AreEqual(_goalDTOToConvert.CategoriesOfGoalDTO[0].Type, goalConverted.CategoriesOfGoal[0].Type);
        }


        [TestMethod]
        [ExpectedException(typeof(ExceptionMapper))]
        public void GivenGoalDTOWithIncorrectData_ShouldThrowException()
        {
            GoalDTO goalDTO_ToConvert = new GoalDTO("", 200, CurrencyEnum.UY, _genericListOfCategoriesDTO, 1);

            goalDTO_ToConvert.GoalId = 1;

            MapperGoal.ToGoal(goalDTO_ToConvert, _genericListOfCategories);
        }

        #endregion
    }
}