using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;

namespace TestProject1
{
    [TestClass]
    public class ResumeOfGoalReportDTO_Tests
    {
        #region Initialize

        private ResumeOfGoalReportDTO resumeOfGoalReportDTO;

        [TestInitialize]
        public void Initialize()
        {
            resumeOfGoalReportDTO = new ResumeOfGoalReportDTO();
        }

        #endregion

        #region Amount

        [TestMethod]
        public void GivenAmount_ShouldBeSetted()
        {
            decimal amountDefined = 100;
            resumeOfGoalReportDTO.AmountDefined = amountDefined;

            Assert.AreEqual(amountDefined, 100);
        }

        #endregion

        #region Total Spent

        [TestMethod]
        public void GivenTotalSpent_ShouldBeSetted()
        {
            decimal totalSpentDefined = 100;
            resumeOfGoalReportDTO.TotalSpent = totalSpentDefined;
            Assert.AreEqual(resumeOfGoalReportDTO.TotalSpent, totalSpentDefined);
        }

        #endregion

        #region Goal Achieved

        [TestMethod]
        public void GivenGoalAchieved_ShouldBeSetted()
        {
            bool goalWasAchieved = true;
            resumeOfGoalReportDTO.GoalAchieved = goalWasAchieved;

            Assert.AreEqual(goalWasAchieved, resumeOfGoalReportDTO.GoalAchieved);

        }

        #endregion

        #region Goal Name

        [TestMethod]
        public void GivenGoalName_ShouldBeSetted()
        {
            resumeOfGoalReportDTO.GoalName = "My goal";

            Assert.AreEqual(resumeOfGoalReportDTO.GoalName, "My goal");
        }

        #endregion

        #region Constructor

        [TestMethod]
        public void GivenValues_ShoulCreateAnInstance()
        {
            decimal amountDefined = 1000;
            decimal totalSpentDefined = 200;
            bool goalWasAchieved = true;
            resumeOfGoalReportDTO = new ResumeOfGoalReportDTO(amountDefined, totalSpentDefined, goalWasAchieved);
            Assert.AreEqual(amountDefined, resumeOfGoalReportDTO.AmountDefined);
            Assert.AreEqual(totalSpentDefined, resumeOfGoalReportDTO.TotalSpent);
            Assert.AreEqual(goalWasAchieved, resumeOfGoalReportDTO.GoalAchieved);
        }

        #endregion
    }
}