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

        [TestInitialize]
        public void Initialize()
        {
            _creditCardAccountDTO = new CreditCardAccountDTO();
            _creditCardAccountId = 1;
            _nameToSet = "Itau volar";
            _currencyToSet = CurrencyEnumDTO.EUR;
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
    }
}