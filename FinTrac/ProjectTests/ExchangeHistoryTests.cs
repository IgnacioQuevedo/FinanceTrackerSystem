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

    #region Value

    [TestMethod]

    public void GivenExchangeValue_ShouldBeSetted()
    {
        decimal exchangeValue = 40.5M;

        genericExchange.Value = exchangeValue;

        Assert.AreEqual(exchangeValue, genericExchange.Value);

    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionExchangeHistory))]
    public void GivenExchangeNegativeValue_ShouldThrowException()
    {
        genericExchange.Currency = CurrencyEnum.USA;
        genericExchange.Value = -38.9M;
        genericExchange.ValidateExchange();
    }

    #endregion

    #region Date of Value
    [TestMethod]
    public void GivenAValueDate_ShouldBeSetted()
    {
        DateTime dateOfDollarValue = new DateTime(2023/8/4);
        genericExchange.ValueDate = dateOfDollarValue;
        Assert.AreEqual(dateOfDollarValue, genericExchange.ValueDate);
    }
    #endregion

    #region Constructor

    [TestMethod]
    public void GivenCorrectValues_ShouldBePossibleToCreateAnExchangeHistory()
    {
        CurrencyEnum currency = CurrencyEnum.USA;
        decimal exchangeValue = 38.5M;
        DateTime valueDate = new DateTime(2023/10/4);

        ExchangeHistory historyToday = new ExchangeHistory(currency,exchangeValue,valueDate);
        Assert.AreEqual(currency, historyToday.Currency);
        Assert.AreEqual(exchangeValue, historyToday.Value);
        Assert.AreEqual(valueDate, historyToday.ValueDate);
    }

    [TestMethod]
    [ExpectedException (typeof(ExceptionExchangeHistory))]

    public void GivenIncorrectValues_ShouldThrowExcepton()
    {
        CurrencyEnum currency = CurrencyEnum.UY;
        decimal exchangeValue = 38.5M;
        DateTime valueDate = new DateTime(2023 / 10 / 4);

        ExchangeHistory historyToday = new ExchangeHistory(currency, exchangeValue, valueDate);
    }

    #endregion
}