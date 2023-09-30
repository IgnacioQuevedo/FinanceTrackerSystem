using BusinessLogic.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagers.Category_Manager
{
    public class CategoryManager
    {

        #region Properties
        private Repository _memoryDatabase;
        #endregion

        #region Constructor
        public CategoryManager(Repository memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }
        #endregion

        #region Add Category

        public void AddCategory(Category categoryToAdd)
        {

            ValidateRegisteredCategory(categoryToAdd);
            _memoryDatabase.Categories.Add(categoryToAdd);
        }

        private void ValidateRegisteredCategory(Category categoryToAdd)
        {
            foreach (var category in _memoryDatabase.Categories)
            {
                if (category.Id == categoryToAdd.Id)
                {
                    throw new ExceptionCategoryManager("Category name already registered, impossible to create another Category.");
                }
            }
        }

        #endregion

        #region Get Categories
        public List<Category> GetCategories()
        {
            List<Category> listOfCategories = _memoryDatabase.Categories;
            return listOfCategories;
        }
        #endregion
    }
}
