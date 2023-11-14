using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusinessLogic.Dtos_Components
{
    public class MonetaryAccountDTO : AccountDTO
    {
        #region Properties
        public decimal Amount { get; set; }
   
        #endregion

        #region Constructors

        public MonetaryAccountDTO() { }

        public MonetaryAccountDTO(string name, decimal amount, CurrencyEnumDTO currency, DateTime creationDate, int? userId) : base(name,currency,creationDate,userId)
        {
            Amount = amount;
        }

        #endregion
    }
}
