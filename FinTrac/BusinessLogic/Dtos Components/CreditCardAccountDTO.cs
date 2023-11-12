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
        public string Last4Digits { get; set; }
        public decimal AvailableCredit { get; set; }
        public DateTime ClosingDate { get; set; }
        public int? UserId { get; set; }

        public CreditCardAccountDTO() { }

        public CreditCardAccountDTO(string name, CurrencyEnumDTO currency, DateTime creationDate, string issuingBank, string last4digits, decimal availableCredit, DateTime closingDate, int? userId)
        {
            Name = name;
            Currency = currency;
            CreationDate = creationDate;
            IssuingBank = issuingBank;
            Last4Digits = last4digits;
            AvailableCredit = availableCredit;
            ClosingDate = closingDate;
            UserId = userId;
        }


    }
}
