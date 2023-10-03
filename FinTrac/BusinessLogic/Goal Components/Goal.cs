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



        public void ValidateGoal()
        {

            if(Title == null || Title.Length == 0) 
            {
                throw new ExceptionValidateGoal("Error on goal tittle, it cannot be empty");
            
            }

        }

    }
}
