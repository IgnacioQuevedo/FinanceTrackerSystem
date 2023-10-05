using BusinessLogic;
using BusinessLogic.User_Components;
using BusinessLogic.ExchangeHistory_Components;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;
using BusinessLogic.Account_Components;

namespace TestProject1
{
    [TestClass]
    public class ExchangeHistoryManagementTests
    {
        #region Initialize

        private string firstName;
        private string lastName;
        private string email;
        private string password;
        private string address;
        private User genericUser;

        private DateTime date;
        ExchangeHistory exchangeHistoryExample;

        [TestInitialize]
        public void Init()
        {
            firstName = "Austin";
            lastName = "Ford";
            email = "austinFord@gmail.com";
            password = "AustinF2003";
            address = "NW 2nd Ave";
            genericUser = new User(firstName, lastName, email, password, address);

            date = new DateTime(2023 / 10 / 4);
            exchangeHistoryExample = new ExchangeHistory(CurrencyEnum.USA, 38.5M, date);
        }

        #endregion

        #region Add Exchange History

        [TestMethod]
        public void GivenAExchangeHistory_ShouldBePossibleToAdd()
        {
            genericUser.AddExchangeHistory(exchangeHistoryExample);

            Assert.AreEqual(exchangeHistoryExample, genericUser.MyExchangesHistory[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionExchangeHistoryManagement))]
        public void GivenAExchangeWhereItsDateHasBeenAlreadyUsed_ShouldThrowException()
        {
            genericUser.AddExchangeHistory(exchangeHistoryExample);

            ExchangeHistory exchangeWithDateUsed = new ExchangeHistory(CurrencyEnum.USA, 45.9M, date);
            genericUser.AddExchangeHistory(exchangeWithDateUsed);
        }

        #endregion

        #region Setting Id

        [TestMethod]
        public void GivenAnExchange_ShouldBePossibleToSetAnId()
        {
            genericUser.AddExchangeHistory(exchangeHistoryExample);
            Assert.AreEqual(exchangeHistoryExample.ExchangeHistoryId, genericUser.MyExchangesHistory.Count);
        }

        #endregion

        [TestMethod]
        public void ShouldBePossibleToReturnListOfExchanges()
        {
            Assert.AreEqual(genericUser.MyExchangesHistory, genericUser.GetExchangesHistory());
        }
    }
}
