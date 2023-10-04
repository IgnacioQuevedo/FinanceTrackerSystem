using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Account_Components;
using BusinessLogic.Category_Components;

namespace BusinessLogic.Goal_Components
{
    public class Goal
    {
        #region Properties
        public string Title { get; set; } = "";
        public int maxAmountToSpend { get; set; }

        public CurrencyEnum Currency { get; set; } = CurrencyEnum.UY;

        public List<Category> CategoriesOfGoal { get; set; }

        #endregion

        #region Constructor
        public Goal()
        {
            CategoriesOfGoal = new List<Category>();
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
            if (CategoriesOfGoal.Count == 0)
            {
                throw new ExceptionValidateGoal("ERROR");
            }
        }

        private void ValidateMaxAmmount()
        {
            if (maxAmountToSpend < 0)
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
