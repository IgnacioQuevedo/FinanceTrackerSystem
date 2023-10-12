using BusinessLogic;
using BusinessLogic.User_Components;
using BusinessLogic.ExchangeHistory_Components;
using BusinessLogic.Transaction_Components;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;
using BusinessLogic.Category_Components;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;

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

            date = DateTime.Now.Date;
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
            Assert.AreEqual(exchangeHistoryExample.ExchangeHistoryId, genericUser.MyExchangesHistory.Count - 1);
        }

        #endregion

        #region Return Of Exchanges

        [TestMethod]
        public void ShouldBePossibleToReturnListOfExchanges()
        {
            Assert.AreEqual(genericUser.MyExchangesHistory, genericUser.GetExchangesHistory());
        }

        #endregion

        #region Delete Of Exchange

        [TestMethod]
        public void GivenExchangeToDelete_ShouldBePossibleToDelete()
        {
            genericUser.AddExchangeHistory(exchangeHistoryExample);
            int idOfExchangeHistoryToDelete = exchangeHistoryExample.ExchangeHistoryId;
            genericUser.DeleteExchangeHistory(exchangeHistoryExample);

            Assert.IsNull(genericUser.MyExchangesHistory[idOfExchangeHistoryToDelete]);
        }

        #endregion

        #region Modify Of Exchange

        [TestMethod]
        public void GivenNewValues_ShouldBePossibleToUpdateThemBecauseIsNotUsedOnATransaction()
        {
            genericUser.AddExchangeHistory(exchangeHistoryExample);

            decimal newValue = 45.7M;
            CurrencyEnum currencyOfExchange = exchangeHistoryExample.Currency;
            DateTime valueDateOfExchange = exchangeHistoryExample.ValueDate;
            int idUpdate = exchangeHistoryExample.ExchangeHistoryId;

            ExchangeHistory exchangeUpdated = new ExchangeHistory(currencyOfExchange, newValue, valueDateOfExchange);
            exchangeUpdated.ExchangeHistoryId = idUpdate;
            exchangeHistoryExample.ValidateApplianceExchangeOnTransaction();
            genericUser.ModifyExchangeHistory(exchangeUpdated);

            Assert.AreEqual(newValue, genericUser.MyExchangesHistory[idUpdate].Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionExchangeHistoryManagement))]

        public void GivenNewValuesButWrongIdToUpdate_ShouldThrowExceptionCauseByMistakenValues()
        {
            decimal newValue = 45.7M;
            CurrencyEnum currencyOfExchange = exchangeHistoryExample.Currency;
            DateTime valueDateOfExchange = exchangeHistoryExample.ValueDate;
            ExchangeHistory exchangeUpdated = new ExchangeHistory(currencyOfExchange, newValue, valueDateOfExchange);

            genericUser.AddExchangeHistory(exchangeHistoryExample);

            exchangeHistoryExample.ValidateApplianceExchangeOnTransaction();
            genericUser.ModifyExchangeHistory(exchangeUpdated);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionExchangeHistoryManagement))]

        public void GivenExchangeHistoryToUpdateThatIsAppliedOnATransaction_ShouldThrowException()
        {
            genericUser.AddExchangeHistory(exchangeHistoryExample);

            decimal newValue = 45.7M;
            CurrencyEnum currencyOfExchange = exchangeHistoryExample.Currency;
            ExchangeHistory exchangeThatWillBeApplied = new ExchangeHistory(currencyOfExchange, newValue, DateTime.Now.Date);
            exchangeThatWillBeApplied.ExchangeHistoryId = exchangeHistoryExample.ExchangeHistoryId;

            Category genericCategory = new Category("Clothes", StatusEnum.Enabled, TypeEnum.Outcome);
            Transaction transaction = new Transaction("Payment of food", 400, DateTime.Now.Date, CurrencyEnum.USA, TypeEnum.Outcome, genericCategory);
            Transaction.CheckExistenceOfExchange(transaction.CreationDate, genericUser.GetExchangesHistory());

            exchangeHistoryExample.ValidateApplianceExchangeOnTransaction();
        }

        #endregion
    }
}
