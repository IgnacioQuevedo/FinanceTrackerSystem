using System.Diagnostics.CodeAnalysis;
using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.User_Components;
using Controller;
using DataManagers;
using Controller.Mappers;
using Mappers;
using BusinessLogic.Report_Components;

namespace ControllerTests
{
    [TestClass]
    public class MapperResumeOfGoalReport_Tests
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

        #region To Resume Of Goal Report DTO

        [TestMethod]
        public void GivenResumeOfGoalReport_ShouldBeConvertedToDTO()
        {
            ResumeOfGoalReport givenResume = new ResumeOfGoalReport(1000, 200, false, "My goal");
            ResumeOfGoalReportDTO resumeDTO = MapperResumeOfGoalReport.ToResumeOfGoalReportDTO(givenResume);

            Assert.AreEqual(givenResume.AmountDefined, resumeDTO.AmountDefined);
            Assert.AreEqual(givenResume.TotalSpent, resumeDTO.TotalSpent);
            Assert.AreEqual(givenResume.GoalAchieved, resumeDTO.GoalAchieved);
            Assert.AreEqual(givenResume.GoalName, resumeDTO.GoalName);
        }

        #endregion

        #region To Resume of goal report

        [TestMethod]
        public void GivenResumeOfGoalDTO_ShouldReturnResumeOfGoal()
        {
            ResumeOfGoalReportDTO givenResumeDTO = new ResumeOfGoalReportDTO(1000, 200, false, "My goal");
            ResumeOfGoalReport resume = MapperResumeOfGoalReport.ToResumeOfGoalReport(givenResumeDTO);

            Assert.AreEqual(givenResumeDTO.AmountDefined, resume.AmountDefined);
            Assert.AreEqual(givenResumeDTO.TotalSpent, resume.TotalSpent);
            Assert.AreEqual(givenResumeDTO.GoalAchieved, resume.GoalAchieved);
        }

        #endregion

        #region To List of Resume of goal report

        [TestMethod]
        public void GivenListOfResumeOfGoalReportDTO_ShouldConvertListToListOfResumeGoal()
        {
            ResumeOfGoalReportDTO givenResumeDTO = new ResumeOfGoalReportDTO(1000, 200, false, "My goal");

            List<ResumeOfGoalReportDTO> listGivenDTO = new List<ResumeOfGoalReportDTO>();

            listGivenDTO.Add(givenResumeDTO);

            List<ResumeOfGoalReport> myConvertedList = MapperResumeOfGoalReport.ToListResumeOfGoalReport(listGivenDTO);

            Assert.AreEqual(givenResumeDTO.TotalSpent, myConvertedList[0].TotalSpent);
            Assert.AreEqual(givenResumeDTO.AmountDefined, myConvertedList[0].AmountDefined);
            Assert.AreEqual(givenResumeDTO.GoalAchieved, myConvertedList[0].GoalAchieved);

        }

        #endregion

        #region ToList Resume Of GoalReport DTO

        [TestMethod]
        public void GivenListOfResumeOfGoalReport_ShouldConvertListToListOfResumeGoalDTO()
        {
            ResumeOfGoalReport givenResume = new ResumeOfGoalReport(1000, 200, false, "My goal");

            List<ResumeOfGoalReport> listGiven = new List<ResumeOfGoalReport>();

            listGiven.Add(givenResume);

            List<ResumeOfGoalReportDTO> myConvertedList = MapperResumeOfGoalReport.ToListResumeOfGoalReportDTO(listGiven);

            Assert.AreEqual(givenResume.TotalSpent, myConvertedList[0].TotalSpent);
            Assert.AreEqual(givenResume.AmountDefined, myConvertedList[0].AmountDefined);
            Assert.AreEqual(givenResume.GoalAchieved, myConvertedList[0].GoalAchieved);
            Assert.AreEqual(givenResume.GoalName, myConvertedList[0].GoalName);

        }

        #endregion
    }
}