using BusinessLogic.Category_Components;
using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos_Components
{
    public class GoalDTO
    {
        public string Title { get; set; }
        public int MaxAmountToSpend { get; set; }
        public CurrencyEnum CurrencyOfAmount { get; set; }
        public List<Category> CategoriesOfGoalDTO { get; set; }

        public GoalDTO() { }

        public GoalDTO(string title, int maxAmountToSpend, CurrencyEnum currencyOfAmount, List<Category> categoriesOfGoal)
        {
            Title = title;
            MaxAmountToSpend = maxAmountToSpend;
            CurrencyOfAmount = currencyOfAmount;
            CategoriesOfGoalDTO = categoriesOfGoal;
        }
    }
}
