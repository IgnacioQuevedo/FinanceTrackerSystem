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

    #region Initialize

    private ExchangeHistory genericExchange;
    private CurrencyEnum currency;

    [TestInitialize]

    public void Initialize()
    {

        genericExchange = new ExchangeHistory();
        currency = CurrencyEnum.USA;
    }

    #endregion

    #region Currency
    [TestMethod]

    public void GivenCurrency_ShouldBeSetted()
    {
        genericExchange.Currency = currency;
        Assert.AreEqual(currency, genericExchange.Currency);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionExchangeHistory))]
    
    public void GivenCurrencyThatItIsNotDollar_ShouldThrowException()
    {
        genericExchange.Currency = CurrencyEnum.UY;
        genericExchange.ValidateExchange();
    }


    #endregion

}