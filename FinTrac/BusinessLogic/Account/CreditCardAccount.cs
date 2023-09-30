using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Account
{
    public class CreditCardAccount : Account
    {

        public string IssuingBank { get; set; }


        public CreditCardAccount() { }


        public void ValidateCreditCardAccount()
        {
            if(IssuingBank == "")
            {
                throw new ExceptionValidateAccount("ERROR ON ISSUING BANK NAME");
            }
        }

    }
}
