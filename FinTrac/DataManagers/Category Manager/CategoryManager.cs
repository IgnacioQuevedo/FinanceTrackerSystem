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

        public void AddCategory(Category myCat)
        {
            foreach (var category in _memoryDatabase.Categories)
            {
                if (category.Name == myCat.Name)
                {
                    throw new ExceptionCategoryManager("Category name already registered, impossible to create another Category.");
                }
            }
            _memoryDatabase.Categories.Add(myCat);
        }


    }
}
