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

        [TestInitialize]
        public void Init()
        {
            firstName = "Austin";
            lastName = "Ford";
            email = "austinFord@gmail.com";
            password = "AustinF2003";
            address = "NW 2nd Ave";
            genericUser = new User(firstName, lastName, email, password, address);
        }

        #endregion

        #region Add Exchange History

        [TestMethod]
        public void GivenAExchangeHistory_ShouldBePossibleToAdd()
        {
            DateTime date = new DateTime(2023 / 10 / 4);
            ExchangeHistory exchangeHistoryExample = new ExchangeHistory(CurrencyEnum.USA, 38.5M, date);
            genericUser.AddExchangeHistory(exchangeHistoryExample);

            Assert.AreEqual(exchangeHistoryExample, genericUser.MyExchangesHistory[0]);
        }

        #endregion
    }
}
