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
      
    }
}