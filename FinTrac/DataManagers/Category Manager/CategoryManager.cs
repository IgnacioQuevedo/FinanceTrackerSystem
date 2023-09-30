using BusinessLogic.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            setCategoryId(categoryToAdd);
            ValidateRegisteredCategory(categoryToAdd);
            _memoryDatabase.Categories.Add(categoryToAdd);
        }

        private void setCategoryId(Category categoryToAdd)
        {
            categoryToAdd.Id = _memoryDatabase.Categories.Count + 1;
        }

        private void ValidateRegisteredCategory(Category categoryToAdd)
        {
            foreach (var category in _memoryDatabase.Categories)
            {
                if (category.Name == categoryToAdd.Name)
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

        #region Modify Category

        public void ModifyCategory(Category categoryToUpdate)
        {
            int lengthOfCategoryList = _memoryDatabase.Categories.Count;

            for (int i = 0; i < lengthOfCategoryList; i++)
            {
                if (_memoryDatabase.Categories[i].Id == categoryToUpdate.Id)
                {
                    _memoryDatabase.Categories[i] = categoryToUpdate;
                    break;
                }
            }

        }

        #endregion

        #region Delete Category

        public void DeleteCategory(Category myCat)
        {
            _memoryDatabase.Categories.Remove(myCat);
        }

        #endregion


    }
}
