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
    private Category genericCategory2;
    private Transaction genericTransaction;
    private Goal goalFood;
    private Goal goalParty;

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
        genericCategory2 = new Category("Party", StatusEnum.Enabled, TypeEnum.Outcome);

        Account myMonetaryAcount = new MonetaryAccount("Brou Savings", 1000, CurrencyEnum.UY);

        loggedUser.AddAccount(myMonetaryAcount);

        genericTransaction = new Transaction("Payment for food", 100, DateTime.Now.Date, CurrencyEnum.USA, TypeEnum.Outcome, genericCategory);

        myMonetaryAcount.AddTransaction(genericTransaction);

        genericTransaction = new Transaction("Payment for party", 200, DateTime.Now.Date, CurrencyEnum.UY, TypeEnum.Outcome, genericCategory2);

        myMonetaryAcount.AddTransaction(genericTransaction);

        loggedUser.AddCategory(genericCategory);
        loggedUser.AddCategory(genericCategory2);

        List<Category> myCategoriesForGoal = new List<Category>() { loggedUser.MyCategories[0] };

        goalFood = new Goal("Less food", 100, myCategoriesForGoal);

        myCategoriesForGoal = new List<Category>() { loggedUser.MyCategories[1] };

        goalParty = new Goal("Less party", 400, myCategoriesForGoal);

    }


    [TestMethod]
    public void GivenTransactionInUSA_ShouldBeConvertedToUY()
    {
        Report.ConvertDolar(genericTransaction, loggedUser);

        decimal convertionNeeded = 200 * 38.9M;

        Assert.AreEqual(convertionNeeded, Report.ConvertDolar(genericTransaction, loggedUser));
    }

    [TestMethod]
    public void GivenUser_ShouldBePossibleToRegisterAllSpendingsPerCategory()
    {
        decimal[] arrayNeeded = new decimal[2];
        arrayNeeded[0] = 3890;
        arrayNeeded[1] = 200;
        decimal[] arrayObtained = Report.SpendingsPerCategory(loggedUser);

        Assert.AreEqual(arrayNeeded, arrayObtained);
    }

}