using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Category_Components
{

    public class ExceptionValidateCategory : Exception
    {
        public ExceptionValidateCategory(string message) : base(message) { }
    }

}
