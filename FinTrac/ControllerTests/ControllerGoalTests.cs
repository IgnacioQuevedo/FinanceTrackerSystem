using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.Goal_Components;
using BusinessLogic.User_Components;
using Controller;
using Controller.IControllers;
using DataManagers;

namespace ControllerTests
{
    [TestClass]
    public class ControllerGoalTests
    {
        #region Initialize

        private GenericController _controller;
        private ICategoryController _categoryController;
        private SqlContext _testDb;
        private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();

        private UserRepositorySql _userRepo;
        private GoalDTO _goalDTOToAdd;
        private UserDTO _userConnected;
        private UserLoginDTO _userLoginDTO;
        private List<CategoryDTO> _genericListCategoryDTO;

        private CategoryDTO _categoryDTO1;
        private CategoryDTO _categoryDTO2;

        [TestInitialize]
        public void Initialize()
        {
            _testDb = _contextFactory.CreateDbContext();
            _userRepo = new UserRepositorySql(_testDb);
            _controller = new GenericController(_userRepo);

            _categoryDTO1 = new CategoryDTO("Food", StatusEnum.Enabled, TypeEnum.Outcome, 1);
            _categoryDTO1.CategoryId = 1;

            _genericListCategoryDTO = new List<CategoryDTO>();

            _genericListCategoryDTO.Add(_categoryDTO1);

            _goalDTOToAdd = new GoalDTO("Less party", 200, CurrencyEnum.UY, _genericListCategoryDTO, 1);
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

        #region Create Goal

        [TestMethod]
        public void CreateMethod_ShouldAddNewGoalIntoDb()
        {
            _controller.CreateCategory(_categoryDTO1);
            _controller.CreateGoal(_goalDTOToAdd);

            Goal goalInDb = _testDb.Users.First().MyGoals[0];

            _goalDTOToAdd.GoalId = 1;

            Assert.AreEqual(_goalDTOToAdd.GoalId, goalInDb.GoalId);
            Assert.AreEqual(_goalDTOToAdd.Title, goalInDb.Title);
            Assert.AreEqual(_goalDTOToAdd.CurrencyOfAmount, goalInDb.CurrencyOfAmount);
            Assert.AreEqual(_goalDTOToAdd.MaxAmountToSpend, goalInDb.MaxAmountToSpend);
            Assert.AreEqual(_goalDTOToAdd.UserId, goalInDb.UserId);

            Assert.AreEqual(_goalDTOToAdd.CategoriesOfGoalDTO[0].Name, goalInDb.CategoriesOfGoal[0].Name);
            Assert.AreEqual(_goalDTOToAdd.CategoriesOfGoalDTO[0].Status, goalInDb.CategoriesOfGoal[0].Status);
            Assert.AreEqual(_goalDTOToAdd.CategoriesOfGoalDTO[0].Type, goalInDb.CategoriesOfGoal[0].Type);
            Assert.AreEqual(_goalDTOToAdd.CategoriesOfGoalDTO[0].UserId, goalInDb.CategoriesOfGoal[0].UserId);
            Assert.AreEqual(_goalDTOToAdd.CategoriesOfGoalDTO[0].CategoryId, goalInDb.CategoriesOfGoal[0].CategoryId);

        }

        #endregion


    }
}
