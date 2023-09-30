﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLogic.Account
{
    public class MonetaryAccount : Account
    {
        public decimal Ammount { get; set; }


        public MonetaryAccount() { }


        public void ValidateMonetaryAccount()
        {
            ValidateAccount();
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
