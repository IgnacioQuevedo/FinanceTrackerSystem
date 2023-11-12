﻿using System;
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
        public DateTime CreationDate { get; set; }
        public int? UserId { get; set; }

        public MonetaryAccountDTO() { }

        public MonetaryAccountDTO(string name, decimal amount, CurrencyEnumDTO currency, DateTime creationDate, int? userId)
        {
            Name = name;
            Amount = amount;
            Currency = currency;
            CreationDate = creationDate;
            UserId = userId;
        }
    }
}
