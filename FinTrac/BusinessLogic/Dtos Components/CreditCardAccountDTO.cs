using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusinessLogic.Dtos_Components
{
    public class CreditCardAccountDTO
    {

        public int CreditCardAccountId { get; set; }
        public string Name { get; set; }
        public CurrencyEnumDTO Currency { get; set; }
        public DateTime CreationDate { get; set; }
        public string IssuingBank { get; set; }



        public CreditCardAccountDTO() { }


    }
}
