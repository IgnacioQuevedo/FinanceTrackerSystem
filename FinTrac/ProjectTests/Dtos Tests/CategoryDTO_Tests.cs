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
            StatusEnum categoryStatus1 = StatusEnum.Enabled;
            StatusEnum categoryStatus2 = StatusEnum.Disabled;

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
            TypeEnum typeEnum = TypeEnum.Income;
            TypeEnum typeEnum2 = TypeEnum.Outcome;

            _categoryDto.Type = typeEnum;
            Assert.AreEqual(_categoryDto.Type, typeEnum);

            _categoryDto.Type = typeEnum2;
            Assert.AreEqual(_categoryDto.Type, typeEnum2);
        }

        #endregion

        [TestMethod]
        public void GivenUserOfCategoryId_ShouldBePossibleToAssignItToDTO()
        {
            int categoryUserId = 1;

            _categoryDto.CategoryUserId = 1;

            Assert.AreEqual(categoryUserId, _categoryDto.CategoryUserId);

        }


        #region Constructor

        [TestMethod]
        public void GivenValues_ShouldBePossibleToCreateACategoryDTO()
        {
            string categoryName = "Food";
            StatusEnum categoryStatus = StatusEnum.Enabled;
            TypeEnum categoryType = TypeEnum.Income;
            int categoryUserId = 1;

            CategoryDTO genericCategoryDTO = new CategoryDTO(categoryName, categoryStatus, categoryType, categoryUserId);

            Assert.AreEqual(categoryName, genericCategoryDTO.Name);
            Assert.AreEqual(categoryStatus, genericCategoryDTO.Status);
            Assert.AreEqual(categoryType, genericCategoryDTO.Type);
            Assert.AreEqual(categoryUserId, genericCategoryDTO.CategoryUserId);
        }

        #endregion


    }
}