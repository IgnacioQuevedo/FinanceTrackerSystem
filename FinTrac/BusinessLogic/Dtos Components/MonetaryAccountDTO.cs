using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusinessLogic.Dtos_Components
{
    public class MonetaryAccountDTO
    {

        public int MonetaryAccountId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public CurrencyEnumDTO Currency { get; set; }

        public MonetaryAccountDTO() { }
    }
}
