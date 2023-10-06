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
            throw new NotImplementedException();
        }
    }
}
