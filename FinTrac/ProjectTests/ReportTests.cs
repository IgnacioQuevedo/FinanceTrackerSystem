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

        genericTransaction = new Transaction("Payment for food", 100, DateTime.Now.Date, CurrencyEnum.UY, TypeEnum.Outcome, genericCategory);

        myMonetaryAcount.AddTransaction(genericTransaction);

        genericTransaction = new Transaction("Payment for party", 200, DateTime.Now.Date, CurrencyEnum.USA, TypeEnum.Outcome, genericCategory2);

        myMonetaryAcount.AddTransaction(genericTransaction);

        loggedUser.AddCategory(genericCategory);
        loggedUser.AddCategory(genericCategory2);

        List<Category> myCategoriesForGoal = new List<Category>() { loggedUser.MyCategories[0] };

        goalFood = new Goal("Less food", 100, myCategoriesForGoal);

        myCategoriesForGoal = new List<Category>() { loggedUser.MyCategories[1] };

        goalParty = new Goal("Less party", 400, myCategoriesForGoal);

        loggedUser.AddGoal(goalFood);
        loggedUser.AddGoal(goalParty);

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
        arrayNeeded[0] = 100;
        arrayNeeded[1] = 7780;
        decimal[] arrayObtained = Report.CategorySpendings(loggedUser, (MonthsEnum)DateTime.Now.Month, loggedUser.MyAccounts);

        Assert.AreEqual(arrayNeeded[0], arrayObtained[0]);
        Assert.AreEqual(arrayNeeded[1], arrayObtained[1]);
    }

    [TestMethod]
    public void GivenUser_ShouldBePossibleToRegisterTotalSpendingInTheEndOfArray()
    {
        decimal[] arrayNeeded = new decimal[4];
        arrayNeeded[0] = 100;
        arrayNeeded[1] = 7780;
        arrayNeeded[2] = 0;
        arrayNeeded[3] = 7880;
        decimal[] arrayObtained = Report.CategorySpendings(loggedUser, (MonthsEnum)DateTime.Now.Month, loggedUser.MyAccounts);

        Assert.AreEqual(arrayNeeded[2], arrayObtained[2]);
        Assert.AreEqual(arrayNeeded[3], arrayObtained[3]);
    }

    [TestMethod]
    public void GivenUser_ShouldReturnReportOfGoals()
    {
        ResumeOfGoalReport resumeNeeded = new ResumeOfGoalReport(100, 100, true);
        List<ResumeOfGoalReport> listObtained = Report.MonthlyReportPerGoal(loggedUser);

        Assert.AreEqual(resumeNeeded.AmountDefined, listObtained[0].AmountDefined);
        Assert.AreEqual(resumeNeeded.TotalSpent, listObtained[0].TotalSpent);
        Assert.AreEqual(resumeNeeded.GoalAchieved, listObtained[0].GoalAchieved);

    }

    [TestMethod]
    public void GivenUser_ShouldReturnAllSpendingsPerCategoryWithPercentajes()
    {
        decimal percent = (100.0M / 7880.0M) * 100.0M;
        ResumeOfSpendigsReport resumeNeeded = new ResumeOfSpendigsReport(genericCategory, 100, percent);
        List<ResumeOfSpendigsReport> listObtained = Report.GiveAllSpendingsPerCategoryDetailed(loggedUser, (MonthsEnum)DateTime.Now.Month);

        Assert.AreEqual(resumeNeeded.CategoryRelated, listObtained[0].CategoryRelated);
        Assert.AreEqual(resumeNeeded.TotalSpentInCategory, listObtained[0].TotalSpentInCategory);
        Assert.AreEqual(resumeNeeded.PercentajeOfTotal, listObtained[0].PercentajeOfTotal);
    }

    [TestMethod]
    public void GivenUser_ShouldReturnListOfAllOutcomeTransactions()
    {
        List<Transaction> listObtained = Report.GiveAllOutcomeTransactions(loggedUser);
        Assert.AreEqual(loggedUser.MyAccounts[0].MyTransactions[0], listObtained[0]);
        Assert.AreEqual(loggedUser.MyAccounts[0].MyTransactions[1], listObtained[1]);
    }

    [TestMethod]
    public void GivenCreditCardAccount_ShouldGiveAReportOfSpendingsInTheRangeOfBalance()
    {
        CreditCardAccount credit = new CreditCardAccount("My Credits", CurrencyEnum.UY, "Brou", "1234", 1000, DateTime.Now);

        Transaction myTransaction = new Transaction("Payment for party", 200, DateTime.Now.Date, CurrencyEnum.UY, TypeEnum.Outcome, genericCategory2);

        Transaction myTransaction2 = new Transaction("Payment for FOOD", 100, new DateTime(2023, 9, 9), CurrencyEnum.UY, TypeEnum.Outcome, genericCategory);

        loggedUser.AddAccount(credit);

        loggedUser.MyAccounts[1].AddTransaction(myTransaction);
        loggedUser.MyAccounts[1].AddTransaction(myTransaction2);


        List<Transaction> listObtained = Report.ReportOfSpendingsPerCard(credit);

        Assert.AreEqual(myTransaction, listObtained[0]);
        Assert.AreEqual(myTransaction2, listObtained[1]);

    }



}