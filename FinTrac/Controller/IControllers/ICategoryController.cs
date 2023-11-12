using BusinessLogic.Dtos_Components;

namespace Controller.IControllers
{
    public interface ICategoryController
    {
        public void CreateCategory(CategoryDTO dtoToAdd);
        public CategoryDTO FindCategory(int idOfCategoryToFind, int idUserConnected);
        public void UpdateCategory(CategoryDTO categoryDtoWithUpdates);
        public void DeleteCategory(CategoryDTO categoryToDelete);
        public List<CategoryDTO> GetAllCategories(int userConnectedId);


    }
}