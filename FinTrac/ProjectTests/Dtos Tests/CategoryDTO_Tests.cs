using BusinessLogic.Dto_Components;

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
      
    }
}