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
        public string Last4Digits { get; set; }

        public CreditCardAccount() { }


        public void ValidateCreditCardAccount()
        {
            ValidateName();
            ValidateLast4Digits();
        }

        private void ValidateName()
        {
            if (string.IsNullOrEmpty(IssuingBank))
            {
                throw new ExceptionValidateAccount("ERROR ON ISSUING BANK NAME");
            }
        }

        private void ValidateLast4Digits()
        {
            int lengthOfLastDigits = Last4Digits.Length;


            foreach (char caracter in Last4Digits)
            {
                if (!char.IsDigit(caracter))
                {
                    throw new ExceptionValidateAccount("ERROR");
                }
            }

            if (string.IsNullOrEmpty(Last4Digits) || lengthOfLastDigits < 4)
            {
                throw new ExceptionValidateAccount("ERROR ON LAST 4 DIGITS");
            }
        }
    }
}
