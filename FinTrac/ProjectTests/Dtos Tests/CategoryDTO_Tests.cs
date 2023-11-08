using BusinessLogic.Dto_Components;
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

        [TestMethod]
        public void GivenId_ShouldBeSetted()
        {
            _categoryDto.Id = 1;
            Assert.AreEqual(1,_categoryDto.Id);
        }
        
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
            Assert.AreEqual(_categoryDto.Status,categoryStatus1);
            
            _categoryDto.Status = categoryStatus2;
            Assert.AreEqual(_categoryDto.Status,categoryStatus2);
        }

        #endregion

        #region Type

        [TestMethod]
        public void GivenType_BothShouldBeSetted()
        {
            TypeEnum typeEnum = TypeEnum.Income;
            TypeEnum typeEnum2 = TypeEnum.Outcome;
            
            _categoryDto.Type = typeEnum;
            Assert.AreEqual(_categoryDto.Type,typeEnum);
            
            _categoryDto.Type = typeEnum2;
            Assert.AreEqual(_categoryDto.Type,typeEnum2);
        }

        #endregion

        #region Constructor

        [TestMethod] 
        public void GivenValues_ShouldBePossibleToCreateACategoryDTO()
        {
            string categoryName = "Food";
            StatusEnum categoryStatus = StatusEnum.Enabled;
            TypeEnum categoryType = TypeEnum.Income;

            CategoryDTO genericCategoryDTO = new CategoryDTO(categoryName, categoryStatus, categoryType);

            Assert.AreEqual(categoryName,genericCategoryDTO.Name);
            Assert.AreEqual(categoryStatus,genericCategoryDTO.Status);
            Assert.AreEqual(categoryType,genericCategoryDTO.Type);
        }

        #endregion
      
      
    }
}