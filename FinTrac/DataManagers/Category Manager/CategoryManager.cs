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
        private Repository _memoryDatabase;

        public CategoryManager(Repository memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public void AddCategory(Category categoryToAdd)
        {
            ValidateRegisteredCategory(categoryToAdd);
            _memoryDatabase.Categories.Add(categoryToAdd);
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
    }
}
