using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos_Components
{
    public class ExchangeHistoryDTO
    {
        public CurrencyEnum Currency { get; set; }
        public decimal Value { get; set; }
        public DateTime ValueDate { get; set; }
        
        public int UserId { get; set; }

        public ExchangeHistoryDTO() { }

        public ExchangeHistoryDTO(CurrencyEnum currency, decimal value, DateTime valueDate, int userId ) 
        {
            Currency = currency;
            Value = value;
            ValueDate = valueDate;
            UserId = userId;
        }
    }
}
