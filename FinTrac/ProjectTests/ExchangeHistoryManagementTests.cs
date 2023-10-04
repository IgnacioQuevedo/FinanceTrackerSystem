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
        [TestMethod]
        public void GivenAExchangeHistory_ShouldBePossibleToAdd()
        {
            DateTime date = new DateTime(2023 / 10 / 4);
            ExchangeHistory exchangeHistoryExample = new ExchangeHistory(CurrencyEnum.USA, 38.5M, date);

            string firstName = "Austin";
            string lastName = "Ford";
            string email = "austinFord@gmail.com";
            string password = "AustinF2003";
            string address = "NW 2nd Ave";
            User genericUser = new User(firstName, lastName, email, password, address);

            genericUser.AddExchangeHistory(exchangeHistoryExample);
            Assert.AreEqual(exchangeHistoryExample, genericUser.MyExchangeHistory[0]);
        }
    }
}
