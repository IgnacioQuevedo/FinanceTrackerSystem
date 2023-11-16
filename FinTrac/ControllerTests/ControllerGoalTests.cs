using BusinessLogic.Category_Components;
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

            _categoryDTO1 = new CategoryDTO("Food", (StatusEnumDTO)StatusEnum.Enabled, (TypeEnumDTO)TypeEnum.Outcome, 1);
            _categoryDTO1.CategoryId = 1;

            _genericListCategoryDTO = new List<CategoryDTO>();

            _genericListCategoryDTO.Add(_categoryDTO1);

            _goalDTOToAdd = new GoalDTO("Less party", 200, CurrencyEnumDTO.UY, _genericListCategoryDTO, 1);
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
            Assert.AreEqual((CurrencyEnum)_goalDTOToAdd.CurrencyOfAmount, goalInDb.CurrencyOfAmount);
            Assert.AreEqual(_goalDTOToAdd.MaxAmountToSpend, goalInDb.MaxAmountToSpend);
            Assert.AreEqual(_goalDTOToAdd.UserId, goalInDb.UserId);

            Assert.AreEqual(_goalDTOToAdd.CategoriesOfGoalDTO[0].Name, goalInDb.CategoriesOfGoal[0].Name);
            Assert.AreEqual((StatusEnum)_goalDTOToAdd.CategoriesOfGoalDTO[0].Status, goalInDb.CategoriesOfGoal[0].Status);
            Assert.AreEqual((TypeEnum)_goalDTOToAdd.CategoriesOfGoalDTO[0].Type, goalInDb.CategoriesOfGoal[0].Type);
            Assert.AreEqual(_goalDTOToAdd.CategoriesOfGoalDTO[0].UserId, goalInDb.CategoriesOfGoal[0].UserId);
            Assert.AreEqual(_goalDTOToAdd.CategoriesOfGoalDTO[0].CategoryId, goalInDb.CategoriesOfGoal[0].CategoryId);

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GivenGoalDTOWitBadData_ShouldThrowException()
        {
            _goalDTOToAdd.Title = "";
            _controller.CreateGoal(_goalDTOToAdd);
        }

        #endregion

        #region Get All GoalsDTO

        [TestMethod]
        public void GivenUserId_ShouldReturnItCorrespondingListOfGoalsDTO()
        {
            _controller.CreateCategory(_categoryDTO1);
            _controller.CreateGoal(_goalDTOToAdd);

            _goalDTOToAdd.GoalId = 1;

            List<GoalDTO> goalsInDb = _controller.GetAllGoalsDTO(_userConnected.UserId);

            Assert.AreEqual(_goalDTOToAdd.UserId, goalsInDb[0].UserId);
            Assert.AreEqual(_goalDTOToAdd.GoalId, goalsInDb[0].GoalId);
            Assert.AreEqual(_goalDTOToAdd.Title, goalsInDb[0].Title);
            Assert.AreEqual(_goalDTOToAdd.MaxAmountToSpend, goalsInDb[0].MaxAmountToSpend);
            Assert.AreEqual(_goalDTOToAdd.CurrencyOfAmount, goalsInDb[0].CurrencyOfAmount);

            Assert.AreEqual(_goalDTOToAdd.CategoriesOfGoalDTO[0].CategoryId, goalsInDb[0].CategoriesOfGoalDTO[0].CategoryId);
            Assert.AreEqual(_goalDTOToAdd.CategoriesOfGoalDTO[0].Name, goalsInDb[0].CategoriesOfGoalDTO[0].Name);
            Assert.AreEqual(_goalDTOToAdd.CategoriesOfGoalDTO[0].Status, goalsInDb[0].CategoriesOfGoalDTO[0].Status);
            Assert.AreEqual(_goalDTOToAdd.CategoriesOfGoalDTO[0].Type, goalsInDb[0].CategoriesOfGoalDTO[0].Type);
            Assert.AreEqual(_goalDTOToAdd.CategoriesOfGoalDTO[0].UserId, goalsInDb[0].CategoriesOfGoalDTO[0].UserId);
        }

        #endregion

        #region Get All goals from user

        [TestMethod]
        public void GivenUserId_ShouldReturnItCorrespondingListOfGoals()
        {
            _controller.CreateCategory(_categoryDTO1);
            _controller.CreateGoal(_goalDTOToAdd);

            _goalDTOToAdd.GoalId = 1;

            List<Goal> myListOfGoalsInDb = _controller.ReceiveGoalListFromUser(_userConnected.UserId);

            Assert.AreEqual(_goalDTOToAdd.UserId, myListOfGoalsInDb[0].UserId);
            Assert.AreEqual(_goalDTOToAdd.GoalId, myListOfGoalsInDb[0].GoalId);
            Assert.AreEqual(_goalDTOToAdd.Title, myListOfGoalsInDb[0].Title);
            Assert.AreEqual(_goalDTOToAdd.MaxAmountToSpend, myListOfGoalsInDb[0].MaxAmountToSpend);
            Assert.AreEqual((CurrencyEnum)_goalDTOToAdd.CurrencyOfAmount, myListOfGoalsInDb[0].CurrencyOfAmount);

            Assert.AreEqual(_goalDTOToAdd.CategoriesOfGoalDTO[0].CategoryId, myListOfGoalsInDb[0].CategoriesOfGoal[0].CategoryId);
            Assert.AreEqual(_goalDTOToAdd.CategoriesOfGoalDTO[0].Name, myListOfGoalsInDb[0].CategoriesOfGoal[0].Name);
            Assert.AreEqual((StatusEnum)_goalDTOToAdd.CategoriesOfGoalDTO[0].Status, myListOfGoalsInDb[0].CategoriesOfGoal[0].Status);
            Assert.AreEqual((TypeEnum)_goalDTOToAdd.CategoriesOfGoalDTO[0].Type, myListOfGoalsInDb[0].CategoriesOfGoal[0].Type);
            Assert.AreEqual(_goalDTOToAdd.CategoriesOfGoalDTO[0].UserId, myListOfGoalsInDb[0].CategoriesOfGoal[0].UserId);


        }

        #endregion

        #region Find Goal In Db

        [TestMethod]
        public void GivenGoalToFindDTO_ShouldReturnGoalInDb()
        {
            _controller.CreateCategory(_categoryDTO1);
            _controller.CreateGoal(_goalDTOToAdd);
            _goalDTOToAdd.GoalId = 1;

            Goal goalInDb = _controller.FindGoalInDb(_goalDTOToAdd);

            Assert.AreEqual(goalInDb.CategoriesOfGoal.Count, _goalDTOToAdd.CategoriesOfGoalDTO.Count);
            Assert.AreEqual(goalInDb.Title, _goalDTOToAdd.Title);
            Assert.AreEqual(goalInDb.GoalId, _goalDTOToAdd.GoalId);
            Assert.AreEqual(goalInDb.UserId, _goalDTOToAdd.UserId);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GivenGoalToFindDTOWhichIsNotCreated_ShouldThrowException()
        {
            _goalDTOToAdd.GoalId = -1;
            _controller.FindGoalInDb(_goalDTOToAdd);
        }
        
        #endregion

    }

}

