using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Transaction_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.User_Components;
using BusinessLogic.Account_Components;
using BusinessLogic.ExchangeHistory_Components;

namespace BusinessLogic.Report_Components
{
    public abstract class Report
    {
        public static void ConvertDolar(Transaction myTransaction, User loggedUser)
        {
            bool found = false;
            decimal dolarValue = 0;
            DateTime bestDate = DateTime.MinValue;
            if (myTransaction.Currency == CurrencyEnum.USA)
            {
                foreach (ExchangeHistory exchange in loggedUser.MyExchangesHistory)
                {
                    if (exchange.ValueDate > bestDate && exchange.ValueDate <= myTransaction.CreationDate && !found)
                    {
                        bestDate = exchange.ValueDate;
                        dolarValue = exchange.Value;
                        if (exchange.ValueDate == myTransaction.CreationDate)
                        {
                            found = true;
                        }
                    }
                }
                myTransaction.Amount = myTransaction.Amount * dolarValue;
            }
        }
    }
}
