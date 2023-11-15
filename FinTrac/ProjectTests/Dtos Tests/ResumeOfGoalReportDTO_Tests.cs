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

        [TestMethod]
        public void GivenTotalSpent_ShouldBeSetted()
        {
            resumeOfGoalReportDTO.TotalSpent = 100;
            Assert.AreEqual(resumeOfGoalReportDTO.TotalSpent, 100);
        }

    }
}