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
        myMonetaryAccount.ValidateName();
    }

    #endregion


}