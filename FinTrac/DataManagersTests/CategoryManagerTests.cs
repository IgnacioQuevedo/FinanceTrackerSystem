using BusinessLogic.Category;
using DataManagers;
using DataManagers.Category_Manager;

namespace DataManagersTests
{
    [TestClass]
    public class CategoryManagerTest
    {

        #region initializingAspects
        private Category genericCategory;
        private CategoryManager categoryManager;
        private Repository memoryDatabase;

        [TestInitialize]
        public void TestInitialize()
        {
            string name = "Outcomes";
            StatusEnum status = (StatusEnum)1;
            TypeEnum type = (TypeEnum)1;

            memoryDatabase = new Repository();
            categoryManager = new CategoryManager(memoryDatabase);
            genericCategory = new Category(name, status, type);
        }

        #endregion

        [TestMethod]
        public void GivenCorrectCategoryToAdd_ShouldAddCategory()
        {
            int numberOfCategoriesAddedBefore = memoryDatabase.Categories.Count;
            categoryManager.AddCategory(genericCategory);
            Assert.AreEqual(numberOfCategoriesAddedBefore + 1, memoryDatabase.Categories.Count);
        }

    }
}