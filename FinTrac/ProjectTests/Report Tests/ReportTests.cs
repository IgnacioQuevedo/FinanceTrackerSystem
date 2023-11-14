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
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;

namespace BusinessLogicTests;

[TestClass]
public class ReportTests
{
    #region Intializing Aspects

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
    private MonetaryAccount myMonetaryAccount;
    private Transaction transactionWanted1;
    private Transaction transactionWanted2;
    private Transaction transactionUnWanted1;
    private Transaction transactionUnWanted2;

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
        genericCategory.CategoryId = 1;
        genericCategory2 = new Category("Party", StatusEnum.Enabled, TypeEnum.Outcome);
        genericCategory2.CategoryId = 2;
        myMonetaryAccount = new MonetaryAccount("Brou Savings", 1000, CurrencyEnum.UY, DateTime.Now);

        loggedUser.AddMonetaryAccount(myMonetaryAccount);

        genericTransaction = new Transaction("Payment for food", 100, DateTime.Now.Date, CurrencyEnum.UY,
            TypeEnum.Outcome, genericCategory);

        myMonetaryAccount.AddTransaction(genericTransaction);

        genericTransaction = new Transaction("Payment for party", 200, DateTime.Now.Date, CurrencyEnum.USA,
            TypeEnum.Outcome, genericCategory2);

        myMonetaryAccount.AddTransaction(genericTransaction);

        loggedUser.AddCategory(genericCategory);
        loggedUser.MyCategories[0].CategoryId = 1;

        loggedUser.AddCategory(genericCategory2);
        loggedUser.MyCategories[1].CategoryId = 2;

        List<Category> myCategoriesForGoal = new List<Category>() { loggedUser.MyCategories[0] };

        goalFood = new Goal("Less food", 100, myCategoriesForGoal);

        myCategoriesForGoal = new List<Category>() { loggedUser.MyCategories[1] };

        goalParty = new Goal("Less party", 400, myCategoriesForGoal);

        loggedUser.AddGoal(goalFood);
        loggedUser.AddGoal(goalParty);

        transactionWanted1 = new Transaction("Payment for Party", 400, new DateTime(2023, 06, 01),
            CurrencyEnum.UY, TypeEnum.Outcome, genericCategory);
        transactionWanted2 = new Transaction("Payment for Party", 200, new DateTime(2023, 07, 01),
            CurrencyEnum.UY, TypeEnum.Outcome, genericCategory);
        transactionUnWanted1 = new Transaction("Payment for Snacks", 300, new DateTime(2023, 02, 01),
            CurrencyEnum.UY, TypeEnum.Outcome, genericCategory2);
        transactionUnWanted2 = new Transaction("Payment for Debt", 1000, new DateTime(2022, 01, 01),
            CurrencyEnum.UY, TypeEnum.Outcome, genericCategory2);
    }

    #endregion

    #region Monthly Report Of Goals

    [TestMethod]
    public void GivenUser_ShouldReturnReportOfGoals()
    {
        ResumeOfGoalReport resumeNeeded = new ResumeOfGoalReport(100, 100, true);
        List<ResumeOfGoalReport> listObtained = Report.MonthlyReportPerGoal(loggedUser);

        Assert.AreEqual(resumeNeeded.AmountDefined, listObtained[0].AmountDefined);
        Assert.AreEqual(resumeNeeded.TotalSpent, listObtained[0].TotalSpent);
        Assert.AreEqual(resumeNeeded.GoalAchieved, listObtained[0].GoalAchieved);
    }

    #endregion

    #region Report of all spendings per cateogry detailed

    [TestMethod]
    public void GivenUser_ShouldReturnAllSpendingsPerCategoryWithPercentajes()
    {
        decimal percent = 100.0M / 7880.0M * 100.0M;
        ResumeOfSpendigsReport resumeNeeded = new ResumeOfSpendigsReport(genericCategory, 100, percent);
        List<ResumeOfSpendigsReport> listObtained =
            Report.GiveAllSpendingsPerCategoryDetailed(loggedUser, (MonthsEnum)DateTime.Now.Month);

        Assert.AreEqual(resumeNeeded.CategoryRelated, listObtained[0].CategoryRelated);
        Assert.AreEqual(resumeNeeded.TotalSpentInCategory, listObtained[0].TotalSpentInCategory);
        Assert.AreEqual(resumeNeeded.PercentajeOfTotal, listObtained[0].PercentajeOfTotal);
    }

    #endregion

    #region Report of All Outcome Transactions

    [TestMethod]
    public void GivenUser_ShouldReturnListOfAllOutcomeTransactions()
    {
        List<Transaction> listObtained = Report.GiveAllOutcomeTransactions(loggedUser);
        Assert.AreEqual(loggedUser.MyAccounts[0].MyTransactions[0], listObtained[0]);
        Assert.AreEqual(loggedUser.MyAccounts[0].MyTransactions[1], listObtained[1]);
    }

    #endregion

    #region Report Of Spendings For Credit Card

    [TestMethod]
    public void GivenCreditCardAccount_ShouldGiveAReportOfSpendingsInTheRangeOfBalance()
    {
        DateTime genericDate = DateTime.MaxValue;
        DateTime startingDate = new DateTime(2023, 10, 16).Date;
        DateTime closingDate = new DateTime(2023, 11, 15).Date;

        CreditCardAccount credit = new CreditCardAccount("My Credits", CurrencyEnum.UY, startingDate, "Brou", "1234",
            1000, closingDate);

        genericCategory.CategoryId = 1;
        genericCategory2.CategoryId = 2;

        Transaction myTransaction = new Transaction("Payment for party", 200, new DateTime(2023, 10, 20),
            CurrencyEnum.UY, TypeEnum.Outcome, genericCategory2);

        Transaction myTransaction2 = new Transaction("Payment for FOOD", 100, new DateTime(2023, 10, 25),
            CurrencyEnum.UY, TypeEnum.Outcome, genericCategory);

        loggedUser.AddCreditAccount(credit);

        loggedUser.MyAccounts[1].AddTransaction(myTransaction);
        loggedUser.MyAccounts[1].AddTransaction(myTransaction2);


        List<Transaction> listObtained = Report.ReportOfSpendingsPerCard(credit);

        Assert.AreEqual(myTransaction, listObtained[0]);
    }

    #endregion

    #region Report Of Balance For Monetary Account

    [TestMethod]
    public void GivenMonetaryAccount_ShouldReportBalance()
    {
        decimal balanceNeeded = 1000.0M - 200.0M - 100.0M;
        decimal balanceObtained = Report.GiveAccountBalance(myMonetaryAccount);

        Assert.AreEqual(balanceNeeded, balanceObtained);
    }

    #endregion

    #region Filtering Lists of spendings Tests

    [TestMethod]
    public void GivenListOfSpendingsToBeFilteredByRangeOfDates_ShouldReturnListFilteredCorrectly()
    {
        List<Transaction> listOfSpendings = new List<Transaction>();
        listOfSpendings.Add(transactionWanted1);
        listOfSpendings.Add(transactionWanted2);
        listOfSpendings.Add(transactionUnWanted1);
        listOfSpendings.Add(transactionUnWanted2);

        List<Transaction> expectedList = new List<Transaction>();
        expectedList.Add(transactionWanted1);
        expectedList.Add(transactionWanted2);

        DateTime finalSelectedDate = new DateTime(2023, 12, 31);
        DateTime initialDate = new DateTime(2023, 05, 01);

        RangeOfDates rangeOfDates = new RangeOfDates(initialDate, finalSelectedDate);
        listOfSpendings = Report.FilterListOfSpendingsByRangeOfDate(listOfSpendings, rangeOfDates);

        Assert.AreEqual(listOfSpendings[0], expectedList[0]);
        Assert.AreEqual(listOfSpendings[1], expectedList[1]);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionReport))]
    public void GivenInitialDateBiggerThanFinalDate_ShouldThrowException()
    {
        DateTime finalSelectedDate = new DateTime(2021, 12, 31);
        DateTime initialDate = new DateTime(2023, 05, 01);

        List<Transaction> listOfSpendings = new List<Transaction>();
        RangeOfDates rangeOfDates = new RangeOfDates(initialDate, finalSelectedDate);

        Report.FilterListOfSpendingsByRangeOfDate(listOfSpendings, rangeOfDates);
    }

    [TestMethod]
    public void GivenListOfSpendingsToBeFilteredByNameOfCategory_ShouldReturnListFilteredCorrectly()
    {
        List<Transaction> listOfSpendings = new List<Transaction>();
        listOfSpendings.Add(transactionWanted1);
        listOfSpendings.Add(transactionWanted2);
        listOfSpendings.Add(transactionUnWanted1);
        listOfSpendings.Add(transactionUnWanted2);

        List<Transaction> expectedList = new List<Transaction>();
        expectedList.Add(transactionWanted1);
        expectedList.Add(transactionWanted2);

        DateTime finalSelectedDate = new DateTime(2023, 12, 31);
        DateTime initialDate = new DateTime(2023, 05, 01);

        RangeOfDates rangeOfDates = new RangeOfDates(initialDate, finalSelectedDate);
        listOfSpendings = Report.FilterListOfSpendingsByNameOfCategory(listOfSpendings, "Food");

        Assert.AreEqual(listOfSpendings[0], expectedList[0]);
        Assert.AreEqual(listOfSpendings[1], expectedList[1]);
    }

    [TestMethod]
    public void GivenListOfSpendingsToBeFilteredByUserAccount_ShouldReturnListFilteredCorrectly()
    {
        List<Transaction> listOfSpendings = new List<Transaction>();
        listOfSpendings.Add(transactionWanted1);
        listOfSpendings.Add(transactionWanted2);
        listOfSpendings.Add(transactionUnWanted1);

        List<Transaction> expectedList = new List<Transaction>();
        expectedList.Add(transactionWanted1);
        expectedList.Add(transactionWanted2);

        loggedUser.AddMonetaryAccount(myMonetaryAccount);
        loggedUser.MyAccounts[0].AddTransaction(transactionWanted1);
        loggedUser.MyAccounts[0].AddTransaction(transactionWanted2);

        listOfSpendings = Report.FilterListOfSpendingsByAccount(listOfSpendings, loggedUser.MyAccounts[0], loggedUser);

        Assert.AreEqual(listOfSpendings[0], expectedList[0]);
        Assert.AreEqual(listOfSpendings[1], expectedList[1]);
    }

    #endregion

    #region Methods Used By Reports

    [TestMethod]
    public void GivenTransactionInUSA_ShouldBeConvertedToUY()
    {
        Report.ConvertDollar(genericTransaction, loggedUser);

        decimal convertionNeeded = 200 * 38.9M;

        Assert.AreEqual(convertionNeeded, Report.ConvertDollar(genericTransaction, loggedUser));
    }

    [TestMethod]
    public void GivenUser_ShouldBePossibleToRegisterAllSpendingsPerCategory()
    {
        decimal[] arrayNeeded = new decimal[2];
        arrayNeeded[0] = 100;
        arrayNeeded[1] = 7780;
        decimal[] arrayObtained =
            Report.CategorySpendings(loggedUser, (MonthsEnum)DateTime.Now.Month, loggedUser.MyAccounts);

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
        decimal[] arrayObtained =
            Report.CategorySpendings(loggedUser, (MonthsEnum)DateTime.Now.Month, loggedUser.MyAccounts);

        Assert.AreEqual(arrayNeeded[2], arrayObtained[2]);
        Assert.AreEqual(arrayNeeded[3], arrayObtained[3]);
    }

    #endregion

    #region MovementInXDays

    [TestMethod]
    public void GivenArrayOfSpendings_ShouldBeSetToMovementInXDays()
    {
        int[] spendings = new int [5];
        MovementInXDays movements = new MovementInXDays();

        movements.Spendings = spendings;
        Assert.AreEqual(spendings, movements.Spendings);
    }
    
    [TestMethod]
    public void GivenArrayOfIncomes_ShouldBeSetToMovementInXDays()
    {
        int[] incomes = new int [5];
        MovementInXDays movements = new MovementInXDays();

        movements.Incomes = incomes;
        Assert.AreEqual(incomes, movements.Spendings);
    }
    
    
    
    
    
    
    
    
    
    #endregion
    
    
}