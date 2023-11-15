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

    }
}