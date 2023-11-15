using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;

namespace TestProject1
{
    [TestClass]
    public class ResumeOfCategoryReportDTO_Tests
    {
        #region Initialize

        private ResumeOfCategoryReportDTO resumeOfCategoryReportDTO;

        [TestInitialize]
        public void Initialize()
        {
            resumeOfCategoryReportDTO = new ResumeOfCategoryReportDTO();
        }

        #endregion

        #region CategoryDTO

        [TestMethod]
        public void GivenCategoryDTO_ShouldBeSetted()
        {
            CategoryDTO myCategoryDTO = new CategoryDTO("Food", StatusEnumDTO.Enabled, TypeEnumDTO.Outcome, 1);

            resumeOfCategoryReportDTO.CategoryRelated = myCategoryDTO;

            Assert.AreEqual(myCategoryDTO, resumeOfCategoryReportDTO.CategoryRelated);
        }

        #endregion

        #region Total Spent

        [TestMethod]
        public void GivenTotalSpent_ShouldBeSetted()
        {
            resumeOfCategoryReportDTO.TotalSpentInCategory = 100;

            Assert.AreEqual(100, resumeOfCategoryReportDTO.TotalSpentInCategory);
        }

        #endregion

        #region Percentaje

        [TestMethod]
        public void GivenPercentaje_ShouldBeSetted()
        {
            resumeOfCategoryReportDTO.PercentajeOfTotal = 75;

            Assert.AreEqual(75, resumeOfCategoryReportDTO.PercentajeOfTotal);
        }

        #endregion

        [TestMethod]
        public void GivenValues_ShouldCreateResume()
        {
            CategoryDTO myCategoryDTO = new CategoryDTO("Food", StatusEnumDTO.Enabled, TypeEnumDTO.Outcome, 1);

            resumeOfCategoryReportDTO = new ResumeOfCategoryReportDTO(myCategoryDTO, 100, 75);

            Assert.AreEqual(myCategoryDTO, resumeOfCategoryReportDTO.CategoryRelated);
            Assert.AreEqual(100, resumeOfCategoryReportDTO.TotalSpentInCategory);
            Assert.AreEqual(75, resumeOfCategoryReportDTO.PercentajeOfTotal);
        }
    }
}