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

    private ExchangeHistory genericExchangeValue;
    private CurrencyEnum currency;

    [TestInitialize]

    public void Initialize()
    {

        genericExchangeValue = new ExchangeHistory();
        currency = CurrencyEnum.USA;
    }

    #endregion

    #region Currency
    [TestMethod]

    public void GivenCurrency_ShouldBeSetted()
    {
        genericExchangeValue.Currency = currency;
        Assert.AreEqual(currency, genericExchangeValue.Currency);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionExchangeHistory))]
    
    public void GivenCurrencyThatItIsNotDollar_ShouldThrowException()
    {
        genericExchangeValue.Currency = CurrencyEnum.UY;
        genericExchangeValue.ValidateExchange();
    }


    #endregion

}