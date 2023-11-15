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

        [TestMethod]
        public void GivenCategoryDTO_ShouldBeSetted()
        {
            CategoryDTO myCategoryDTO = new CategoryDTO("Food", StatusEnumDTO.Enabled, TypeEnumDTO.Outcome, 1);

            resumeOfCategoryReportDTO.CategoryRelated = myCategoryDTO;

            Assert.AreEqual(myCategoryDTO, resumeOfCategoryReportDTO.CategoryRelated);
        }


    }
}