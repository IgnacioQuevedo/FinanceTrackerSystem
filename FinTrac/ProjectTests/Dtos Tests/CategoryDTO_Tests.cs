using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;

namespace TestProject1
{
    [TestClass]
    public class CategoryDTO_Tests
    {

        #region Initialize

        private CategoryDTO _categoryDto;

        [TestInitialize]
        public void Initialize()
        {
            _categoryDto = new CategoryDTO();
        }

        #endregion

        #region Setting category Id

        [TestMethod]
        public void GivenId_ShouldBeSetted()
        {
            _categoryDto.CategoryId = 1;
            Assert.AreEqual(1, _categoryDto.CategoryId);
        }

        #endregion

        #region Name

        [TestMethod]
        public void GivenName_ShouldBeSetted()
        {
            string categoryName = "Food";

            CategoryDTO categoryDTO = new CategoryDTO();
            _categoryDto.Name = categoryName;

            Assert.AreEqual(categoryName, _categoryDto.Name);
        }

        #endregion

        #region Status

        [TestMethod]
        public void GivenStatus_BothShouldBeSetted()
        {
            StatusEnumDTO categoryStatus1 = StatusEnumDTO.Enabled;
            StatusEnumDTO categoryStatus2 = StatusEnumDTO.Disabled;

            _categoryDto.Status = categoryStatus1;
            Assert.AreEqual(_categoryDto.Status, categoryStatus1);

            _categoryDto.Status = categoryStatus2;
            Assert.AreEqual(_categoryDto.Status, categoryStatus2);
        }

        #endregion

        #region Type

        [TestMethod]
        public void GivenType_BothShouldBeSetted()
        {
            TypeEnumDTO typeEnum = TypeEnumDTO.Income;
            TypeEnumDTO typeEnum2 = TypeEnumDTO.Outcome;

            _categoryDto.Type = typeEnum;
            Assert.AreEqual(_categoryDto.Type, typeEnum);

            _categoryDto.Type = typeEnum2;
            Assert.AreEqual(_categoryDto.Type, typeEnum2);
        }

        #endregion

        #region Setting User Id

        [TestMethod]
        public void GivenUserOfCategoryId_ShouldBePossibleToAssignItToDTO()
        {
            int categoryUserId = 1;
            _categoryDto.UserId = 1;

            Assert.AreEqual(categoryUserId, _categoryDto.UserId);
        }

        #endregion

        #region CreationDate

        [TestMethod]
        public void GivenCreationDate_ShouldBePossibleToAssignIt()
        {
            DateTime creationDate = DateTime.Now.Date;

            _categoryDto.CreationDate = creationDate;

            Assert.AreEqual(_categoryDto.CreationDate, creationDate);
        }

        #endregion

        #region Constructor

        [TestMethod]
        public void GivenValues_ShouldBePossibleToCreateACategoryDTO()
        {
            string categoryName = "Food";
            StatusEnumDTO categoryStatus = StatusEnumDTO.Enabled;
            TypeEnumDTO categoryType = TypeEnumDTO.Income;
            int categoryUserId = 1;

            CategoryDTO genericCategoryDTO =
                new CategoryDTO(categoryName, categoryStatus, categoryType, categoryUserId);


            Assert.AreEqual(categoryName, genericCategoryDTO.Name);
            Assert.AreEqual(categoryStatus, genericCategoryDTO.Status);
            Assert.AreEqual(categoryType, genericCategoryDTO.Type);
            Assert.AreEqual(categoryUserId, genericCategoryDTO.UserId);
        }

        #endregion

    }
}