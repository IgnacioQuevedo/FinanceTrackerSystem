using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;

namespace TestProject1
{
    [TestClass]
    public class MonetaryAccountDTO_Tests
    {

        #region Initialize

        private MonetaryAccountDTO _monetaryAccountDTO;
        private int _userIdToSet;
        private int _idToSet;
        private string _nameToSet;
        private decimal _amountToSet;
        private CurrencyEnumDTO _currencyToSet;
        private DateTime _creationDateToSet;



        [TestInitialize]
        public void Initialize()
        {
            _monetaryAccountDTO = new MonetaryAccountDTO();
            _userIdToSet = 1;
            _idToSet = 1;
            _nameToSet = "Brou";
            _amountToSet = 1000;
            _currencyToSet = CurrencyEnumDTO.EUR;
        }

        #endregion

        #region Id

        [TestMethod]
        public void GivenMonetaryId_ShouldBeSetted()
        {
            _monetaryAccountDTO.MonetaryAccountId = _idToSet;

            Assert.AreEqual(_monetaryAccountDTO.MonetaryAccountId, _idToSet);
        }

        #endregion

        #region Name

        [TestMethod]

        public void GivenName_ShouldBeSetted()
        {
            _monetaryAccountDTO.Name = _nameToSet;

            Assert.AreEqual(_monetaryAccountDTO.Name, _nameToSet);
        }

        #endregion

        #region Amount

        [TestMethod]
        public void GivenAmount_ShouldBeSetted()
        {
            _monetaryAccountDTO.Amount = _amountToSet;

            Assert.AreEqual(_monetaryAccountDTO.Amount, _amountToSet);
        }

        #endregion

        #region Currency

        [TestMethod]
        public void GivenCurrency_ShouldBeSetted()
        {
            _monetaryAccountDTO.Currency = _currencyToSet;

            Assert.AreEqual(_monetaryAccountDTO.Currency, _currencyToSet);
        }

        #endregion

        #region Creation Date

        [TestMethod]
        public void GivenCreationDate_ShouldBeSetted()
        {

            _monetaryAccountDTO.CreationDate = _creationDateToSet;

            Assert.AreEqual(_monetaryAccountDTO.CreationDate, _creationDateToSet);
        }

        #endregion

        #region UserId

        [TestMethod]
        public void GivenUserId_ShouldBeSetted()
        {
            _monetaryAccountDTO.UserId = _userIdToSet;

            Assert.AreEqual(_monetaryAccountDTO.UserId, _userIdToSet);
        }

        #endregion

        #region Constructor

        [TestMethod]
        public void GivenValues_ShouldCreateMonetaryAccountDTO()
        {
            MonetaryAccountDTO myMonetaryAccounntDTO = new MonetaryAccountDTO(_nameToSet, _amountToSet, _currencyToSet, _creationDateToSet, _userIdToSet);

            Assert.AreEqual(myMonetaryAccounntDTO.Name, _nameToSet);
            Assert.AreEqual(myMonetaryAccounntDTO.Amount, _amountToSet);
            Assert.AreEqual(myMonetaryAccounntDTO.Currency, _currencyToSet);
            Assert.AreEqual(myMonetaryAccounntDTO.CreationDate, _creationDateToSet);
            Assert.AreEqual(myMonetaryAccounntDTO.UserId, _userIdToSet);
        }

        #endregion
    }
}