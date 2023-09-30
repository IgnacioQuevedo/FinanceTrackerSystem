using BusinessLogic;
using BusinessLogic.User;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;
using BusinessLogic.Account;
using System.Security.Principal;

namespace TestProject1;

[TestClass]
public class CreditCardAccountTests
{
    private Account myCreditCard;

    #region Init
    [TestInitialize]
    public void TestInitialized()
    {
        myCreditCard = new CreditCardAccount();
    }
    #endregion
    #region Name
    [TestMethod]
    public void GivenCorrectName_ShouldBeSetted()
    {
        string name = "Itau volar";
        myCreditCard.Name = name;
        Assert.AreEqual(name, myCreditCard.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateAccount))]
    public void GivenEmptyName_ShouldThrowException()
    {
        string name = "";
        myCreditCard.Name = name;

        myCreditCard.ValidateAccount();
    }
    #endregion


    [TestMethod]    

    public void DateofCreditCard_ShouldBeActualDate()
    {
        string date = DateTime.Now.ToString("dd/MM/yyyy");
        Assert.AreEqual(date, myCreditCard.CreationDate);
    }

}