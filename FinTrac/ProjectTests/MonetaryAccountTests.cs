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

    [TestMethod]

    public void GivenCorrectName_ShouldItBeSet()
    {

        MonetaryAccount monetaryAccount = new MonetaryAccount();
        string name = "Santander Saving Bank";
        monetaryAccount.Name = name;

        Assert.AreEqual(name, monetaryAccount.Name);
    }



}