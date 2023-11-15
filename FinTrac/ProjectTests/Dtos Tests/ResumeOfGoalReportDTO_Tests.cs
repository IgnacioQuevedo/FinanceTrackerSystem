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

        [TestMethod]
        public void GivenAmount_ShouldBeSetted()
        {
            resumeOfGoalReportDTO.AmountDefined = 100;

            Assert.AreEqual(1, 0);

        }

    }
}