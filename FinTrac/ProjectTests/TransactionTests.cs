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
        bool callToValidationTitleMethod = genericTransaction.ValidateTitle();
        Assert.AreEqual(true, callToValidationTitleMethod);
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
        Assert.AreEqual(true, genericTransaction.ValidateAmount());
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

        //monetaryAccount is a 100% object that references only to monetaryAccount
        myMonetaryAccount.Name = "Scotia Saving Bank";
        myMonetaryAccount.Currency = CurrencyEnum.UY;
        myMonetaryAccount.Ammount = 10;
        genericTransaction.Account = myMonetaryAccount;
        Assert.AreEqual(genericTransaction.Account, myMonetaryAccount);

    }





}