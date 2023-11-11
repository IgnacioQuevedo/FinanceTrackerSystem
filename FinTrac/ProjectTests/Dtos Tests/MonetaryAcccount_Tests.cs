using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;

namespace TestProject1
{
    [TestClass]
    public class MonetaryAccountDTO_Tests
    {

        #region Initialize

        private MonetaryAccountDTO _monetaryAccountDTO;

        [TestInitialize]
        public void Initialize()
        {
            _monetaryAccountDTO = new MonetaryAccountDTO();
        }

        #endregion

        #region Id

        [TestMethod]
        public void GivenMonetaryId_ShouldBeSetted()
        {
            int idToSet = 1;
            _monetaryAccountDTO.MonetaryAccountId = idToSet;

            Assert.AreEqual(_monetaryAccountDTO.MonetaryAccountId, idToSet);
        }

        #endregion

        #region Name

        [TestMethod]

        public void GivenName_ShouldBeSetted()
        {
            string nameToSet = "Brou";
            _monetaryAccountDTO.Name = "Brou";

            Assert.AreEqual(_monetaryAccountDTO.Name, nameToSet);
        }

        #endregion

        #region Amount

        [TestMethod]
        public void GivenAmount_ShouldBeSetted()
        {
            decimal amountToSet = 1000;
            _monetaryAccountDTO.Amount = amountToSet;

            Assert.AreEqual(_monetaryAccountDTO.Amount, amountToSet);
        }

        #endregion

        #region Currency

        [TestMethod]
        public void GivenCurrency_ShouldBeSetted()
        {
            CurrencyEnumDTO currencyToSet = CurrencyEnumDTO.EUR;
            _monetaryAccountDTO.Currency = currencyToSet;

            Assert.AreEqual(_monetaryAccountDTO.Currency, currencyToSet);
        }

        #endregion

        #region Creation Date

        [TestMethod]
        public void GivenCreationDate_ShouldBeSetted()
        {
            MonetaryAccountDTO myMonetaryAccountDTO = new MonetaryAccountDTO();

            DateTime creationDateToSet = DateTime.Now.Date;
            myMonetaryAccountDTO.CreationDate = creationDateToSet;

            Assert.AreEqual(myMonetaryAccountDTO.CreationDate, creationDateToSet);
        }

        #endregion

        #region UserId

        [TestMethod]
        public void GivenUserId_ShouldBeSetted()
        {
            int userIdToSet = 1;
            _monetaryAccountDTO.UserId = userIdToSet;

            Assert.AreEqual(_monetaryAccountDTO.UserId, userIdToSet);
        }

        #endregion

    }
}