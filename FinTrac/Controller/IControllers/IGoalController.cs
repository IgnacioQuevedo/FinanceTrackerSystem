using BusinessLogic.Dtos_Components;

namespace Controller.IControllers
{
    public interface IGoalController
    {
        public void CreateGoal(GoalDTO dtoToAdd);
        public List<GoalDTO> GetAllGoalsDTO(int userConnectedId);
        public void DeleteCategory(int categoryDtoCategoryId);
        public List<CategoryDTO> GetAllCategories(int userConnectedId);


    }
}