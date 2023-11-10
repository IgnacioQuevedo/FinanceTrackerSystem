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
            List<CategoryDTO> listCategoryDTO = ToListOfGoalDTO(goalToConvert);

            GoalDTO goalDTO =
                new GoalDTO(goalToConvert.Title, goalToConvert.MaxAmountToSpend, goalToConvert.CurrencyOfAmount, listCategoryDTO, goalToConvert.UserId);
            goalDTO.GoalId = goalToConvert.GoalId;

            return goalDTO;
        }

        public static Goal ToGoal(GoalDTO goalDTOToConvert)
        {
            List<Category> listOfCategories = new List<Category>();

            foreach (CategoryDTO categoryDTO in goalDTOToConvert.CategoriesOfGoalDTO)
            {
                Category category = MapperCategory.ToCategory(categoryDTO);
                listOfCategories.Add(category);
            }

            Goal goal =
                new Goal(goalDTOToConvert.Title, goalDTOToConvert.MaxAmountToSpend, listOfCategories);

            goalDTOToConvert.GoalId = goalDTOToConvert.GoalId;

            return goal;
        }



        private static List<CategoryDTO> ToListOfGoalDTO(Goal goalToConvert)
        {
            List<CategoryDTO> listCategoryDTO = new List<CategoryDTO>();

            foreach (Category category in goalToConvert.CategoriesOfGoal)
            {
                CategoryDTO categoryDTO = MapperCategory.ToCategoryDTO(category);
                listCategoryDTO.Add(categoryDTO);
            }

            return listCategoryDTO;
        }
    }
}
