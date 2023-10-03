using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Account_Components
{
    public class MonetaryAccount : Account
    {
        public decimal Ammount { get; set; }


        public MonetaryAccount() { }

        public MonetaryAccount(string accountName, decimal ammount, CurrencyEnum currencyType) : base(accountName, currencyType)
        {
            Ammount = ammount;
            ValidateMonetaryAccount();
        }

        public void ValidateMonetaryAccount()
        {
            ValidateAmmount();
        }

        private void ValidateAmmount()
        {
            if (Ammount < 0)
            {
                throw new ExceptionValidateAccount("ERROR ON AMMOUNT");
            }
        }

    }
}
