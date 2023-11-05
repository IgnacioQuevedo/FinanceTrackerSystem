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
    }
}
