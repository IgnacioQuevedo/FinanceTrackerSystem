using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Account
{
    public abstract class Account
    {

        public string Name { get; set; } = "";
        public CurrencyEnum Currency { get; set; }
        public string CreationDate { get;} = DateTime.Now.ToString("dd/MM/yyyy");


        public Account() { }


        public Account(string name, CurrencyEnum currency)
        {
            Name = name;
            Currency = currency;
        }

        public bool ValidateAccount()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new ExceptionValidateAccount("ERROR");
            }
            return true;
        }

    }
}
