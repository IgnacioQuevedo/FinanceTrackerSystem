using BusinessLogic;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;
using BusinessLogic.Transaction_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.Account_Components;
using BusinessLogic.User_Components;
using System.Security.Principal;
using System.Collections.Generic;

namespace TestProject1;
[TestClass]
public class TransactionTests
{
    #region Initializing aspects

    private Transaction genericTransaction;
    private CreditCardAccount myCreditCardAccount;
    private Category genericCategory;
    private Category genericCategory2;
    private MonetaryAccount genericMonetaryAccount;
    private User genericUser;

    [TestInitialize]
    public void TestInitialize()
    {
        string nameCategory = "Business One";
        StatusEnum statusCategory = StatusEnum.Enabled;
        TypeEnum typeCategory = TypeEnum.Income;
        genericCategory = new Category(nameCategory, statusCategory, typeCategory);

        string name2 = "Food";
        StatusEnum status2 = StatusEnum.Enabled;
        TypeEnum type2 = TypeEnum.Outcome;
        genericCategory2 = new Category(name2, status2, type2);

        genericTransaction = new Transaction();
        genericTransaction.Title = "Title";
        genericTransaction.CreationDate = DateTime.Now.Date;
        genericTransaction.Amount = 100;
        genericTransaction.Currency = CurrencyEnum.UY;
        genericTransaction.Type = TypeEnum.Income;

        myCreditCardAccount = new CreditCardAccount();
        myCreditCardAccount.Name = "Scotia Credit Card";
        myCreditCardAccount.Currency = CurrencyEnum.UY;
        myCreditCardAccount.IssuingBank = "Santander";
        myCreditCardAccount.Last4Digits = "1233";
        myCreditCardAccount.AvailableCredit = 15000;
        myCreditCardAccount.ClosingDate = new DateTime(2023, 11, 1);

        genericMonetaryAccount = new MonetaryAccount();
        genericMonetaryAccount.Name = "Scotia Saving Bank";
        genericMonetaryAccount.Currency = CurrencyEnum.UY;
        genericMonetaryAccount.Ammount = 10;

        string firstName = "Austin";
        string lastName = "Ford";
        string email = "austinFord@gmail.com";
        string password = "Austin1980";
        string address = "East 25 Av";
        genericUser = new User(firstName, lastName, email, password, address);
        genericUser.MyCategories.Add(genericCategory);
    }
    #endregion

    #region Validate Title Tests
    [TestMethod]
    public void GivenCorrectTitle_ShouldReturnTrue()
    {
        string titleToBeSet = "Title";
        Assert.AreEqual(titleToBeSet, genericTransaction.Title);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateTransaction))]
    public void GivenEmptyTitle_ShouldReturnFalse()
    {
        genericTransaction.Title = "";
        genericTransaction.ValidateTitle();
    }

    #endregion

    #region Date Tests

    [TestMethod]

    public void GivenDateToSet_ShuldBeSetted()
    {
        DateTime dateToBeSetted = DateTime.Now.Date;
        Assert.AreEqual(genericTransaction.CreationDate, dateToBeSetted);
    }

    #endregion

    #region Amount Tests

    [TestMethod]
    public void GivenCorrectAmount_ShouldReturnTrue()
    {
        decimal amountToBeSet = 100;
        Assert.AreEqual(amountToBeSet, genericTransaction.Amount);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateTransaction))]
    public void GivenWrongAmountToSet_ShouldThrowException()
    {
        genericTransaction.Amount = 0;
        genericTransaction.ValidateAmount();
    }

    #endregion

    #region Currency Tests

    [TestMethod]
    public void GivenCurrency_ShouldBelongToCurrencyEnum()
    {
        bool belongsToCurrencyEnum = Enum.IsDefined(typeof(CurrencyEnum), genericTransaction.Currency);
        Assert.IsTrue(belongsToCurrencyEnum);
    }

    #endregion

    #region Type Tests

    [TestMethod]
    public void GivenType_ShouldBelongToTypeEnum()
    {
        bool belongsToTypeEnum = Enum.IsDefined(typeof(TypeEnum), genericTransaction.Type);
        Assert.IsTrue(belongsToTypeEnum);
    }

    #endregion


}