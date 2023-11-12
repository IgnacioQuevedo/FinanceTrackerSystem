using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using System.Transactions;

namespace TestProject1
{
    [TestClass]
    public class CreditCardAccountDTO_Tests
    {

        #region Initialize

        private CreditCardAccountDTO _creditCardAccountDTO;
        private int _creditCardAccountId;
        private string _nameToSet;
        private CurrencyEnumDTO _currencyToSet;
        private DateTime _creationDateToSet;
        private string _issuingBankToSet;
        private string _last4Digits;
        private decimal _availableCreditToSet;
        private DateTime _closingDateToSet;
        private int _userId;

        [TestInitialize]
        public void Initialize()
        {
            _creditCardAccountDTO = new CreditCardAccountDTO();
            _creditCardAccountId = 1;
            _nameToSet = "Itau volar";
            _currencyToSet = CurrencyEnumDTO.EUR;
            _creationDateToSet = DateTime.Now.Date;
            _issuingBankToSet = "Prex";
            _last4Digits = "1234";
            _availableCreditToSet = 2000;
            _closingDateToSet = new DateTime(2023, 11, 12);
        }

        #endregion

        #region Credit Card Account Id

        [TestMethod]
        public void GivenCredtiCardAccountId_ShouldBeSetted()
        {
            _creditCardAccountDTO.CreditCardAccountId = _creditCardAccountId;

            Assert.AreEqual(_creditCardAccountDTO.CreditCardAccountId, _creditCardAccountId);

        }

        #endregion

        #region Name

        [TestMethod]
        public void GivenName_ShouldBeSetted()
        {
            _creditCardAccountDTO.Name = _nameToSet;

            Assert.AreEqual(_creditCardAccountDTO.Name, _nameToSet);
        }

        #endregion

        #region Currency

        [TestMethod]
        public void GivenCurrency_ShouldBeSetted()
        {
            _creditCardAccountDTO.Currency = _currencyToSet;

            Assert.AreEqual(_creditCardAccountDTO.Currency, _currencyToSet);
        }

        #endregion

        #region Creation Date

        [TestMethod]
        public void GivenCreationDate_ShouldBeSetted()
        {
            _creditCardAccountDTO.CreationDate = _creationDateToSet;

            Assert.AreEqual(_creditCardAccountDTO.CreationDate, _creationDateToSet);
        }

        #endregion

        #region Issuing bank

        [TestMethod]
        public void GivenIssuingBank_ShouldBeSetted()
        {
            _creditCardAccountDTO.IssuingBank = _issuingBankToSet;

            Assert.AreEqual(_creditCardAccountDTO.IssuingBank, _issuingBankToSet);
        }

        #endregion

        #region Last4Digits

        [TestMethod]
        public void GivenLast4Digits_ShouldBeSetted()
        {
            _creditCardAccountDTO.Last4Digits = _last4Digits;

            Assert.AreEqual(_creditCardAccountDTO.Last4Digits, _last4Digits);
        }

        #endregion

        #region Available credit

        [TestMethod]
        public void GivenAvailableCredit_ShouldBeSetted()
        {
            _creditCardAccountDTO.AvailableCredit = _availableCreditToSet;

            Assert.AreEqual(_creditCardAccountDTO.AvailableCredit, _availableCreditToSet);
        }

        #endregion

        #region Closing Date

        [TestMethod]
        public void GivenClosingDate_ShouldBeSetted()
        {
            _creditCardAccountDTO.ClosingDate = _closingDateToSet;

            Assert.AreEqual(_creditCardAccountDTO.ClosingDate, _closingDateToSet);
        }

        #endregion

        #region User Id

        [TestMethod]
        public void GivenUserId_ShouldBeSetted()
        {
            _creditCardAccountDTO.UserId = _userId;

            Assert.AreEqual(_creditCardAccountDTO.UserId, _userId);
        }

        #endregion

    }
}