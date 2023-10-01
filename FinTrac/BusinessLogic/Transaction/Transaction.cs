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

        public DateTime CreationDate { get; set; } = DateTime.Now.Date;

        public int Amount { get; set; }

        public Transaction()
        {
        }

        public bool ValidateTitle()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(Title))
            {
                throw new ExceptionValidateTransaction("ERROR ON TITLE");
            }
            return isValid;
        }

        public bool ValidateAmount()
        {
            bool isValid = true;
            bool amountIsNoNegativeOrZero = Amount <= 0;

            if (amountIsNoNegativeOrZero)
            {
                throw new ExceptionValidateTransaction("ERROR ON AMOUNT");
            }
            return isValid;
        }
    }
}
