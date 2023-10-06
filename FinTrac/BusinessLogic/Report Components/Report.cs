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
using BusinessLogic.Goal_Components;

namespace BusinessLogic.Report_Components
{
    public abstract class Report
    {

        public static decimal ConvertDolar(Transaction myTransaction, User loggedUser)
        {
            bool found = false;
            decimal dolarValue = 0;
            DateTime bestDate = DateTime.MinValue;
            foreach (ExchangeHistory exchange in loggedUser.MyExchangesHistory)
            {
                if (exchange.ValueDate > bestDate && exchange.ValueDate <= myTransaction.CreationDate && !found)
                {
                    if (exchange.ValueDate == myTransaction.CreationDate)
                    {
                        found = true;
                        bestDate = exchange.ValueDate;
                        dolarValue = exchange.Value;
                    }
                    else
                    {
                        bestDate = exchange.ValueDate;
                        dolarValue = exchange.Value;
                    }
                }
            }
            return myTransaction.Amount * dolarValue;
        }

        public static decimal[] SpendingsPerCategory(User loggedUser)
        {
            throw new NotImplementedException();
        }

    }
}



