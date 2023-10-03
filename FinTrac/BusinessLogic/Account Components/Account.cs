using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Account_Components
{
    public abstract class Account
    {
        #region Properties
        public string Name { get; set; } = "";
        public CurrencyEnum Currency { get; set; }
        public DateTime CreationDate { get; } = DateTime.Now.Date;
        #endregion

        #region Constructor
        public Account() { }


        public Account(string name, CurrencyEnum currency)
        {
            Name = name;
            Currency = currency;
        }

        #endregion

        #region Validate Account
        public bool ValidateAccount()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new ExceptionValidateAccount("ERROR");
            }
            return true;
        }
        #endregion

    }
}
