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
        public void GivenGoal_ShouldBePossibleToConvertToGoalDTO()
        {
            List<Category> listOfCategories = new List<Category>();
            Category category1 = new Category("Food", StatusEnum.Enabled, TypeEnum.Outcome);
            Category category2 = new Category("Party", StatusEnum.Enabled, TypeEnum.Outcome);

            listOfCategories.Add(category1);
            listOfCategories.Add(category2);

            Goal goalToConvert = new Goal("Less party", 100, listOfCategories);
            goalToConvert.GoalId = 1;

            GoalDTO goalDTO = MapperGoal.ToGoalDTO(goalToConvert);

            Assert.IsInstanceOfType(goalDTO, typeof(GoalDTO));
            Assert.AreEqual(goalDTO.GoalId, goalToConvert.GoalId);
            Assert.AreEqual(goalDTO.Title, goalToConvert.Title);
            Assert.AreEqual(goalDTO.MaxAmountToSpend, goalToConvert.MaxAmountToSpend);
            Assert.AreEqual(goalDTO.CurrencyOfAmount, goalToConvert.CurrencyOfAmount);
            Assert.AreEqual(goalDTO.UserId, goalToConvert.UserId);
            Assert.AreEqual(goalDTO.CategoriesOfGoalDTO, goalToConvert.CategoriesOfGoal);
        }

    }
}