using BusinessLogic;
using BusinessLogic.User;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;
using BusinessLogic.Account;

namespace TestProject1;
[TestClass]
public class MonetaryAccountTests
{

    [TestInitialize]
    public void TestInitialize()
    {

    }

    #region Name
    [TestMethod]

    public void GivenCorrectName_ShouldItBeSet()
    {
        Account monetaryAccount = new MonetaryAccount();
        string name = "Santander Saving Bank";
        monetaryAccount.Name = name;

        Assert.AreEqual(name, monetaryAccount.Name);
    }


    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateAccount))]
    public void GivenEmptyName_ShouldReturnException()
    {
        string name = "";
        Account myMonetaryAccount = new MonetaryAccount();
        myMonetaryAccount.Name = name;
        myMonetaryAccount.ValidateAccount();
    }


    [TestMethod]

    public void GivenInitialAmmount_ShouldBeSetted()
    {
        int initialAmmount = 100;
        MonetaryAccount myMonetaryAccount = new MonetaryAccount();
        myMonetaryAccount.Ammount = initialAmmount;

        Assert.AreEqual(myMonetaryAccount.Ammount, initialAmmount);

    }
    #endregion


    [TestMethod]
    [ExpectedException (typeof(ExceptionValidateAccount))]

    public void GivenInitialNegativeAmmount_ShouldThrowException()
    {
        MonetaryAccount myMonetaryAccount = new MonetaryAccount();
        myMonetaryAccount.Name = "Brou Saving Bank";
        myMonetaryAccount.Ammount = -1;
        myMonetaryAccount.ValidateMonetaryAccount();

    }



    [TestMethod]
    public void MadeAnAccount_DateShouldBeActualDate()
    {
        MonetaryAccount myMonetariaAccount = new MonetaryAccount();

        Assert.AreEqual(myMonetariaAccount.CreationDate, DateTime.Now.ToString("dd/MM/yyyy"));
    }

}