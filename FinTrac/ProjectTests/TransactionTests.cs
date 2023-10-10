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

        genericTransaction = new Transaction();
        genericTransaction.Title = "Title";
        genericTransaction.CreationDate = DateTime.Now.Date;
        genericTransaction.Amount = 100;
        genericTransaction.Currency = CurrencyEnum.UY;
        genericTransaction.Type = TypeEnum.Income;

        genericMonetaryAccount = new MonetaryAccount();
        genericMonetaryAccount.Name = "Scotia Saving Bank";
        genericMonetaryAccount.Currency = CurrencyEnum.UY;
        genericMonetaryAccount.Amount = 10;
    }
    #endregion

    #region Validate Title Tests
    [TestMethod]
    public void GivenCorrectTitle_ShouldBeSetted()
    {
        string titleToBeSet = "Title";
        Assert.AreEqual(titleToBeSet, genericTransaction.Title);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateTransaction))]
    public void GivenEmptyTitle_ShouldThrowException()
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
    public void GivenCorrectAmount_ShouldBeSetted()
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

    #region Category Tests

    [TestMethod]
    public void GivenCorrectCategory_ShouldBeSetted()
    {
        genericTransaction.TransactionCategory = genericCategory;

        Assert.AreEqual(genericCategory, genericTransaction.TransactionCategory);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateTransaction))]
    public void GivenDisabledCategory_ShouldThrowException()
    {
        genericCategory.Status = StatusEnum.Disabled;
        genericTransaction.TransactionCategory = genericCategory;
        genericTransaction.ValidateCategory();
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateTransaction))]
    public void GivenDifferenceInCategoryTypeWithTransactionType_ShouldThrowException()
    {
        genericCategory.Type = TypeEnum.Income;
        genericTransaction.Type = TypeEnum.Outcome;
        genericTransaction.TransactionCategory = genericCategory;
        genericTransaction.ValidateCategory();

    }
    #endregion

    #region Constructor

    [TestMethod]

    public void GivenCorrectValuesToCreateTransaction_ShouldBeCreated()
    {
        string title = "Payment of Clothes";
        decimal amount = 200;
        TypeEnum type = TypeEnum.Income;
        CurrencyEnum currency = CurrencyEnum.USA;
        DateTime dateTime = DateTime.Now.Date;
        Transaction transacionExample = new Transaction(title, amount, dateTime, currency, type, genericCategory);


        Assert.AreEqual(transacionExample.Title, title);
        Assert.AreEqual(transacionExample.Amount, amount);
        Assert.AreEqual(transacionExample.Type, type);
        Assert.AreEqual(transacionExample.Currency, currency);
        Assert.AreEqual(transacionExample.CreationDate, dateTime);
        Assert.AreEqual(transacionExample.TransactionCategory, genericCategory);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateTransaction))]
    public void GivenIncorrectValues_ShouldThrowException()
    {
        string title = "Payment of Clothes";
        decimal amount = 200;
        TypeEnum type = TypeEnum.Outcome;
        CurrencyEnum currency = CurrencyEnum.USA;
        DateTime dateTime = DateTime.Now.Date;
        Transaction transacionExample = new Transaction(title, amount, dateTime, currency, type, genericCategory)
    }
    #endregion
}
