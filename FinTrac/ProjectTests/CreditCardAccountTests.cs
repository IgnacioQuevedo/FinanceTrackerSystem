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

    [TestInitialize]
    public void TestInitialized()
    {
        myCreditCard = new CreditCardAccount();
    }

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

        myCreditCard.ValidateCreditCardAccount();

    }
  
}