using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Goal_Components
{
    public class Goal
    {
        public string Title { get; set; } = "";

        public int maxAmmountToSpend { get; set; }

        public void ValidateGoal()
        {
            ValidateTitle();
            ValidateMaxAmmount();

        }

        private void ValidateMaxAmmount()
        {
            if (maxAmmountToSpend < 0)
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
    }
}
