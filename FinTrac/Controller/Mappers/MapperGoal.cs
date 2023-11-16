using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Exceptions;
using BusinessLogic.Enums;
using BusinessLogic.Goal_Components;
using Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.Mappers
{
    public abstract class MapperGoal
    {
        #region To GoalDTO

        public static GoalDTO ToGoalDTO(Goal goalToConvert, List<CategoryDTO> listCategoryDTO)
        {

            GoalDTO goalDTO =
                new GoalDTO(goalToConvert.Title, goalToConvert.MaxAmountToSpend, (CurrencyEnumDTO)goalToConvert.CurrencyOfAmount, listCategoryDTO, goalToConvert.UserId);
            goalDTO.GoalId = goalToConvert.GoalId;

            return goalDTO;
        }

        public static List<GoalDTO> ToListOfGoalDTO(List<Goal> listOfGoals)
        {
            List<GoalDTO> myListOfGoalDTO = new List<GoalDTO>();

            foreach (Goal goal in listOfGoals)
            {
                myListOfGoalDTO.Add(ToGoalDTO(goal, MapperCategory.ToListOfCategoryDTO(goal.CategoriesOfGoal)));
            }

            return myListOfGoalDTO;
        }

        #endregion

        #region To Goal

        public static Goal ToGoal(GoalDTO goalDTOToConvert, List<Category> listOfCategories)
        {
            try
            {
                Goal goal =
                    new Goal(goalDTOToConvert.Title, goalDTOToConvert.MaxAmountToSpend, listOfCategories);

                goal.CurrencyOfAmount = (CurrencyEnum)goalDTOToConvert.CurrencyOfAmount;
                goal.UserId = goalDTOToConvert.UserId;

                goalDTOToConvert.GoalId = goal.GoalId;

                return goal;
            }
            catch (ExceptionValidateGoal Exception)
            {
                throw new ExceptionMapper(Exception.Message);
            }
        }


        #endregion
    }
}
