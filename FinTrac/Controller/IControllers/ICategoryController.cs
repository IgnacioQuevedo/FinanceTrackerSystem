using BusinessLogic.Dtos_Components;
using BusinessLogic.Category_Components;

namespace Controller.IControllers
{
    public interface ICategoryController
    {
        public void CreateCategory(CategoryDTO dtoToAdd);
        public CategoryDTO FindCategory(int idOfCategoryToFind);
        public void UpdateCategory(CategoryDTO categoryDtoWithUpdates);
        public void DeleteCategory(int categoryDtoCategoryId);
        public List<CategoryDTO> GetAllCategories(int userConnectedId);


    }
}