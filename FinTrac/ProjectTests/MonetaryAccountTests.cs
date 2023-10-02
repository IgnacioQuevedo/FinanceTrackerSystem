using BusinessLogic;
using BusinessLogic.User;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;
using BusinessLogic.Account;
using System.Security.Principal;

namespace TestProject1;
[TestClass]


public class MonetaryAccountTests
{
    #region Init
    private Account myAccount;
    private MonetaryAccount myMonetaryAccount;


    [TestInitialize]
    public void TestInitialize()
    {
        myAccount = new MonetaryAccount();
        myMonetaryAccount = new MonetaryAccount();

        //myAccout is a polimorfed object
        myAccount.Name = "Scotia Saving Bank";
        myAccount.Currency = CurrencyEnum.UY;

        //monetaryAccount is a 100% object that references only to monetaryAccount
        myMonetaryAccount.Name = myAccount.Name;
        myMonetaryAccount.Currency = CurrencyEnum.UY;
        myMonetaryAccount.Ammount = 10;
    }

    #endregion

    #region Name
    [TestMethod]

    public void GivenCorrectName_ShouldItBeSet()
    {
        string name = "Santander Saving Bank";
        myAccount.Name = name;

        Assert.AreEqual(name, myAccount.Name);
    }


    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateAccount))]
    public void GivenEmptyName_ShouldReturnException()
    {
        string name = "";
        myMonetaryAccount.Name = name;

        myMonetaryAccount.ValidateAccount();
    }

    #endregion

    #region Ammount

    [TestMethod]
    public void GivenInitialAmmount_ShouldBeSetted()
    {
        int initialAmmount = 100;
        myMonetaryAccount.Ammount = initialAmmount;

        Assert.AreEqual(myMonetaryAccount.Ammount, initialAmmount);

    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateAccount))]

    public void GivenInitialNegativeAmmount_ShouldThrowException()
    {
        myMonetaryAccount.Ammount = -1;
        myMonetaryAccount.ValidateMonetaryAccount();

    }

    #endregion

    #region Creation Date
    [TestMethod]
    public void MadeAnAccount_DateShouldBeActualDate()
    {
        DateTime actualDate = DateTime.Now.Date;

        Assert.AreEqual(myMonetaryAccount.CreationDate, actualDate);
    }
    #endregion

    #region Currency

    [TestMethod]
    public void GivenCurrency_ShouldBelongToCurrencyEnum()
    {
        bool belongToEnum = Enum.IsDefined(typeof(CurrencyEnum), myMonetaryAccount.Currency);
        Assert.IsTrue(belongToEnum);

    }
    #endregion

    #region Constructor
    [TestMethod]
    public void CreationOfMonetaryAccount_ShouldBeValidated()
    {

        string nameToBeSetted = "Itau Saving Bank";
        int ammountToBeSetted = 100;
        CurrencyEnum currencyToBeSetted = CurrencyEnum.UY;
        DateTime creationDate = DateTime.Now.Date;

        MonetaryAccount monetaryAccountExample = new MonetaryAccount(nameToBeSetted, ammountToBeSetted, currencyToBeSetted);

        Assert.AreEqual(nameToBeSetted, monetaryAccountExample.Name);
        Assert.AreEqual(ammountToBeSetted, monetaryAccountExample.Ammount);
        Assert.AreEqual(currencyToBeSetted, monetaryAccountExample.Currency);
        Assert.AreEqual(creationDate, monetaryAccountExample.CreationDate);
    }
    #endregion
}