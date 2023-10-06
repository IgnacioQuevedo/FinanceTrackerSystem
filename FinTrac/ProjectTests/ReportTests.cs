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
    private User loggedUser;
    private ExchangeHistory genericExchangeHistory;
    private ExchangeHistory genericExchangeHistory2;
    private ExchangeHistory genericExchangeHistory3;
    private ExchangeHistory genericExchangeHistory4;
    private Category genericCategory;
    private Transaction genericTransaction;

    [TestInitialize]
    public void Initialize()
    {
        loggedUser = new User("Austin", "Ford", "austinFord@gmail.com", "Austin1980", "East 25 Av");

        genericExchangeHistory = new ExchangeHistory(CurrencyEnum.USA, 38.9M, DateTime.Now.Date);

        genericExchangeHistory2 = new ExchangeHistory(CurrencyEnum.USA, 18.9M, new DateTime(2008, 3, 1, 7, 0, 0));

        genericExchangeHistory3 = new ExchangeHistory(CurrencyEnum.USA, 18.9M, new DateTime(2023, 3, 19, 7, 0, 0));

        genericExchangeHistory4 = new ExchangeHistory(CurrencyEnum.USA, 18.9M, new DateTime(2023, 10, 05, 7, 0, 0));

        loggedUser.AddExchangeHistory(genericExchangeHistory);
        loggedUser.AddExchangeHistory(genericExchangeHistory2);
        loggedUser.AddExchangeHistory(genericExchangeHistory3);
        loggedUser.AddExchangeHistory(genericExchangeHistory4);

        genericCategory = new Category("Food", StatusEnum.Enabled, TypeEnum.Outcome);
        genericTransaction = new Transaction("Payment of food", 100, DateTime.Now.Date, CurrencyEnum.USA, TypeEnum.Outcome, genericCategory);
    }


    [TestMethod]
    public void GivenTransactionInUSA_ShouldBeConvertedToUY()
    {
        Report.ConvertDolar(genericTransaction, loggedUser);

        decimal convertionNeeded = 100 * 38.9M;

        Assert.AreEqual(convertionNeeded, genericTransaction.Amount);
    }


}