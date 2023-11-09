using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Goal_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.Mappers
{
    public abstract class MapperGoal
    {
        public static GoalDTO ToGoalDTO(Goal goalToConvert)
        {
            List<CategoryDTO> listCategoryDTO = new List<CategoryDTO>();

            foreach (Category category in goalToConvert.CategoriesOfGoal)
            {
                CategoryDTO categoryDTO = MapperCategory.ToCategoryDTO(category);
                listCategoryDTO.Add(categoryDTO);
            }

            GoalDTO goalDTO =
                new GoalDTO(goalToConvert.Title, goalToConvert.MaxAmountToSpend, goalToConvert.CurrencyOfAmount, listCategoryDTO, goalToConvert.UserId);
            goalDTO.GoalId = goalToConvert.GoalId;

            return goalDTO;
        }

    }
}
