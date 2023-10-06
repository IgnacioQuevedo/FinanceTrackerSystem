using BusinessLogic;
using BusinessLogic.Category_Components;
using BusinessLogic.Goal_Components;
using BusinessLogic.User_Components;
using BusinessLogic.Account_Components;
using BusinessLogic.Transaction_Components;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;
using BusinessLogic.Report_Components;
using BusinessLogic.ExchangeHistory_Components;

namespace TestProject1;
[TestClass]
public class ReportTests
{

    [TestMethod]
    public void GivenTransactionInUSA_ShouldBeConvertedToUY()
    {

        User loggedUser = new User("Austin", "Ford", "austinFord@gmail.com", "Austin1980", "East 25 Av");

        ExchangeHistory exchangeHistory = new ExchangeHistory(CurrencyEnum.USA, 38.9M, new DateTime(2023 / 10 / 01));

        ExchangeHistory exchangeHistory2 = new ExchangeHistory(CurrencyEnum.USA, 18.9M, new DateTime(2023 / 09 / 01));

        loggedUser.AddExchangeHistory(exchangeHistory);

        Category myCategory = new Category("Food", StatusEnum.Enabled, TypeEnum.Outcome);
        Transaction myTransaction = new Transaction("Payment of food", 100, DateTime.Now.Date, CurrencyEnum.USA, TypeEnum.Outcome, myCategory);

        Report.ConvertDolar(myTransaction, loggedUser);

        decimal convertionNeeded = 100 * 38.9M;

        Assert.AreEqual(convertionNeeded, myTransaction.Amount);
    }


}