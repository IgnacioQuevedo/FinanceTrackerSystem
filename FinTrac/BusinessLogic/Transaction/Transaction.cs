using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Transaction
{
    public class Transaction
    {
        public string Title { get; set; }

        public Transaction()
        {
        }

        public bool ValidateTitle()
        {
            bool isValid = true;
            if (Title == "")
            {
                throw new ExceptionValidateTransaction("");
            }
            return isValid;
        }
    }
}
