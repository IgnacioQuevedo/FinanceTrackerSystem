using BusinessLogic;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;
using BusinessLogic.Transaction;
using BusinessLogic.Category_Components;
using BusinessLogic.Account_Components;
using System.Security.Principal;

namespace TestProject1;
[TestClass]
public class TransactionTests
{
    private Transaction genericTransaction;

    [TestInitialize]
    public void TestInitialize()
    {
        genericTransaction = new Transaction();
        genericTransaction.Title = "Title";
        genericTransaction.CreationDate = DateTime.Now.Date;
        genericTransaction.Amount = 100;
        genericTransaction.Currency = CurrencyEnum.UY;
        genericTransaction.Type = TypeEnum.Income;
    }

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

    [TestMethod]

    public void GivenDateToSet_ShuldBeSetted()
    {
        DateTime dateToBeSetted = DateTime.Now.Date;
        Assert.AreEqual(genericTransaction.CreationDate, dateToBeSetted);
    }

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

    [TestMethod]
    public void GivenCurrency_ShouldBelongToCurrencyEnum()
    {
        bool belongsToCurrencyEnum = Enum.IsDefined(typeof(CurrencyEnum), genericTransaction.Currency);
        Assert.IsTrue(belongsToCurrencyEnum);
    }

    [TestMethod]
    public void GivenType_ShouldBelongToTypeEnum()
    {
        bool belongsToTypeEnum = Enum.IsDefined(typeof(TypeEnum), genericTransaction.Type);
        Assert.IsTrue(belongsToTypeEnum);
    }
    [TestMethod]
    public void GivenMonetaryAccount_ShouldBeSetted()
    {
        MonetaryAccount myMonetaryAccount = new MonetaryAccount();
        myMonetaryAccount.Name = "Scotia Saving Bank";
        myMonetaryAccount.Currency = CurrencyEnum.UY;
        myMonetaryAccount.Ammount = 10;

        genericTransaction.Account = myMonetaryAccount;
        Assert.AreEqual(genericTransaction.Account, myMonetaryAccount);

    }

    public void GivenCreditAccount_ShouldBeSetted()
    {
        //Waiting for logic response from customer to be implemented remember adding [TestMethod] labelS
    }

    [TestMethod]
    public void GivenCorrectListToValidate_ShouldNotThrowException()
    {
        string name = "Business";
        StatusEnum status = StatusEnum.Enabled;
        TypeEnum type = TypeEnum.Income;
        Category categoryToBeAdded = new Category(name, status, type);

        List<Category> listToSet = new List<Category>();
        listToSet.Add(categoryToBeAdded);

        genericTransaction.MyCategories = listToSet;
        genericTransaction.ValidateListOfCategories();
    }








}