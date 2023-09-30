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
   
    [TestMethod]
    public void GivenCorrectName_ShouldBeSetted()
    {

        string name = "Itau volar";
        Account myCreditCard = new CreditCardAccount();
        Assert.AreEqual(name, myCreditCard.Name);
    }

    

  
}