using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Goal_Components
{
    public class ExceptionValidateGoal : Exception
    {
        public ExceptionValidateGoal(string message) : base(message) { }
    }
}
