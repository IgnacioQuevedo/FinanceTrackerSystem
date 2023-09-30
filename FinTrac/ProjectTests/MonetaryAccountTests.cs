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


    [TestMethod]

    public void GivenInitialAmmount_ShouldBeSetted()
    {
        int initialAmmount = 100;
        myMonetaryAccount.Ammount = initialAmmount;

        Assert.AreEqual(myMonetaryAccount.Ammount, initialAmmount);

    }
    #endregion


    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateAccount))]

    public void GivenInitialNegativeAmmount_ShouldThrowException()
    {
        myMonetaryAccount.Ammount = -1;
        myMonetaryAccount.ValidateMonetaryAccount();

    }


    [TestMethod]
    public void MadeAnAccount_DateShouldBeActualDate()
    {
        string actualDate = DateTime.Now.ToString("dd/MM/yyyy");

        Assert.AreEqual(myMonetaryAccount.CreationDate, actualDate);
    }

    [TestMethod]
    public void GivenCurrency_ShouldBelongToCurrencyEnum()
    {
        bool belongToEnum = Enum.IsDefined(typeof(CurrencyEnum), myMonetaryAccount.Currency);
        Assert.IsTrue(belongToEnum);

    }

    [TestMethod]


    public void CreationOfMonetaryAccount_ShouldBeValidated()
    {

        string nameToBeSetted = "Itau Saving Bank";
        int ammountToBeSetted = 100;
        CurrencyEnum currencyToBeSetted = CurrencyEnum.UY;
        string creationDate = DateTime.Now.ToString("dd/MM/yyyy");

        MonetaryAccount monetaryAccountExample = new MonetaryAccount(nameToBeSetted, ammountToBeSetted, currencyToBeSetted);

        Assert.AreEqual(nameToBeSetted, monetaryAccountExample.Name);
        Assert.AreEqual(ammountToBeSetted, monetaryAccountExample.Ammount);
        Assert.AreEqual(currencyToBeSetted, monetaryAccountExample.Currency);
        Assert.AreEqual(creationDate, monetaryAccountExample.CreationDate);
    }

}