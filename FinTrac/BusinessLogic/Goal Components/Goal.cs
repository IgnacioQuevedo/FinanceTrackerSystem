using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Category_Components;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using BusinessLogic.User_Components;

namespace BusinessLogic.Goal_Components
{
    public class Goal
    {
        #region Properties

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GoalId { get; set; }
        public string Title { get; set; } = "";
        public int MaxAmountToSpend { get; set; }
        public CurrencyEnum CurrencyOfAmount { get; set; } = CurrencyEnum.UY;
        public List<Category> CategoriesOfGoal { get; set; }
        public int? UserId { get; set; }
        public User GoalUser { get; set; }

        #endregion

        #region Constructor

        public Goal()
        {
        }

        public Goal(string title, int maxAmount, List<Category> categoriesAsignedToGoal)
        {
            Title = title;
            MaxAmountToSpend = maxAmount;
            CategoriesOfGoal = categoriesAsignedToGoal;
            ValidateGoal();
        }

        #endregion

        #region Validations

        public void ValidateGoal()
        {
            ValidateTitle();
            ValidateMaxAmmount();
            ValidateAmountOfCategories();
        }

        private void ValidateAmountOfCategories()
        {
            if (CategoriesOfGoal == null || CategoriesOfGoal.Count == 0)
            {
                throw new ExceptionValidateGoal("It is necessary to set at least one category");
            }
        }

        private void ValidateMaxAmmount()
        {
            if (MaxAmountToSpend < 0)
            {
                throw new ExceptionValidateGoal("Error on max ammount to spent, cannot be negative");
            }
        }

        private void ValidateTitle()
        {
            if (string.IsNullOrEmpty(Title))
            {
                throw new ExceptionValidateGoal("Error on goal tittle, it cannot be empty");
            }
        }

        #endregion
    }
}