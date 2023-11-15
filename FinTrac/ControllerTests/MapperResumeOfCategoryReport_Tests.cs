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
    public class MapperResumeCategoryReport_Tests
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

        #region To Resume Of Category Report

        [TestMethod]
        public void GivenResumeOfCategoryDTO_ShouldReturnResumeOfCategory()
        {

            CategoryDTO myCategoryDTO = new CategoryDTO("Food", StatusEnumDTO.Enabled, TypeEnumDTO.Outcome, 1);

            ResumeOfCategoryReportDTO givenResumeDTO = new ResumeOfCategoryReportDTO(myCategoryDTO, 100, 75);

            ResumeOfCategoryReport resume = MapperResumeOfCategoryReport.ToResumeOfCategoryReport(givenResumeDTO);

            Assert.AreEqual(MapperCategory.ToCategory(givenResumeDTO.CategoryRelated).Name, resume.CategoryRelated.Name);
            Assert.AreEqual(givenResumeDTO.TotalSpentInCategory, resume.TotalSpentInCategory);
            Assert.AreEqual(givenResumeDTO.PercentajeOfTotal, resume.PercentajeOfTotal);
        }

        #endregion

        #region To Resume Of Category Report DTO

        [TestMethod]
        public void GivenResumeOfCategory_ShouldReturnResumeOfCategoryDTO()
        {

            Category myCategory = new Category("Food", StatusEnum.Enabled, TypeEnum.Outcome);

            ResumeOfCategoryReport givenResume = new ResumeOfCategoryReport(myCategory, 100, 75);

            ResumeOfCategoryReportDTO resumeDTO = MapperResumeOfCategoryReport.ToResumeOfCategoryReportDTO(givenResume);

            Assert.AreEqual(MapperCategory.ToCategoryDTO(givenResume.CategoryRelated).Name, resumeDTO.CategoryRelated.Name);
            Assert.AreEqual(givenResume.TotalSpentInCategory, resumeDTO.TotalSpentInCategory);
            Assert.AreEqual(givenResume.PercentajeOfTotal, resumeDTO.PercentajeOfTotal);
        }

        #endregion

        #region To List Resume Of Category Report

        [TestMethod]
        public void GivenListOfResumeOfCategoryReportDTO_ShouldConvertToBL()
        {
            List<ResumeOfCategoryReportDTO> listOfResumeDTO = new List<ResumeOfCategoryReportDTO>();

            CategoryDTO myCategoryDTO = new CategoryDTO("Food", StatusEnumDTO.Enabled, TypeEnumDTO.Outcome, 1);

            ResumeOfCategoryReportDTO givenResumeDTO = new ResumeOfCategoryReportDTO(myCategoryDTO, 100, 75);

            listOfResumeDTO.Add(givenResumeDTO);

            List<ResumeOfCategoryReport> listOfResume = MapperResumeOfCategoryReport.ToListResumeOfCategoryReport(listOfResumeDTO);

            Assert.AreEqual(MapperCategory.ToCategoryDTO(listOfResume[0].CategoryRelated).Name, givenResumeDTO.CategoryRelated.Name);
            Assert.AreEqual(listOfResume[0].TotalSpentInCategory, givenResumeDTO.TotalSpentInCategory);
            Assert.AreEqual(listOfResume[0].PercentajeOfTotal, givenResumeDTO.PercentajeOfTotal);
        }

        #endregion

        #region To Resume Of Category Report DTO

        [TestMethod]
        public void GivenListOfResumeOfCategoryReport_ShouldConvertToDTO()
        {
            List<ResumeOfCategoryReport> listOfResume = new List<ResumeOfCategoryReport>();

            Category myCategory = new Category("Food", StatusEnum.Enabled, TypeEnum.Outcome);

            ResumeOfCategoryReport givenResume = new ResumeOfCategoryReport(myCategory, 100, 75);

            listOfResume.Add(givenResume);

            List<ResumeOfCategoryReportDTO> listOfResumeDTO = MapperResumeOfCategoryReport.ToListResumeOfCategoryReportDTO(listOfResume);

            Assert.AreEqual(MapperCategory.ToCategory(listOfResumeDTO[0].CategoryRelated).Name, givenResume.CategoryRelated.Name);
            Assert.AreEqual(listOfResumeDTO[0].TotalSpentInCategory, givenResume.TotalSpentInCategory);
            Assert.AreEqual(listOfResumeDTO[0].PercentajeOfTotal, givenResume.PercentajeOfTotal);
        }

        #endregion
    }
}