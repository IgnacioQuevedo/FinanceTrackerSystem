using BusinessLogic;
using BusinessLogic.Account_Components;
using BusinessLogic.ExchangeHistory_Components;
using BusinessLogic.User_Components;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;

namespace TestProject1;
[TestClass]
public class ExchangeHistoryTests
{

    [TestInitialize]

    public void Initialize()
    {


    }


    [TestMethod]

    public void GivenCurrency_ShouldBeSetted()
    {

        CurrencyEnum currency = CurrencyEnum.USA; 

        ExchangeHistory exchangeValue = new ExchangeHistory();

        exchangeValue.Currency = currency;
        
        Assert.AreEqual(currency, exchangeValue.Currency);
    }


    
}