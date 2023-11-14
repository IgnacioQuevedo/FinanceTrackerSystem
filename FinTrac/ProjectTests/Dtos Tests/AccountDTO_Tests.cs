using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;

namespace TestProject1
{
    [TestClass]
    public class AccountDTO_Tests
    {
        #region Initialize

        private AccountDTO accountDTO;

        [TestInitialize]
        public void Initialize()
        {
            accountDTO = new AccountDTO();
        }

        #endregion

        #region Setting an Id

        [TestMethod]
        public void GivenAccountId_ShouldBeSetted()
        {
            int accountId = 1;
            accountDTO.AccountId = accountId;

            Assert.AreEqual(accountDTO.AccountId, accountId);
        }

        #endregion
        
        #region Name

        [TestMethod]
        public void GivenName_ShouldBeSetted()
        {
            string name = "Brou";
            accountDTO.Name = name;

            Assert.AreEqual(accountDTO.Name, name);
        }

        #endregion

        #region Currency

        [TestMethod]
        public void GivenCurrency_ShouldBeSetted()
        {
            CurrencyEnumDTO currency = CurrencyEnumDTO.UY;
            accountDTO.Currency = currency;

            Assert.AreEqual(accountDTO.Currency, currency);
        }

        #endregion

        #region CreationDate

        [TestMethod]
        public void GivenCreationDate_ShouldBeSetted()
        {
            DateTime creationDate = DateTime.Now;
            accountDTO.CreationDate = creationDate;

            Assert.AreEqual(accountDTO.CreationDate, creationDate);
        }

        #endregion

        #region Setting User id

        [TestMethod]
        public void GivenUserIdToSet_ShouldBeSetted()
        {
            int userId = 1;
            accountDTO.UserId = userId;
            Assert.AreEqual(accountDTO.UserId, userId);
        }

        #endregion

        [TestMethod]
        public void GivenValuesToCreate_AccountDTOShouldBeCreated()
        {
            int accountId = 1;
            string name = "Brou";
            CurrencyEnumDTO currency = CurrencyEnumDTO.UY;
            DateTime creationDate = DateTime.Now.Date;
            int userId = 1;

            AccountDTO accountDTO = new AccountDTO(name, currency, creationDate, userId);
            
            Assert.AreEqual(accountDTO.AccountId,accountId);
            Assert.AreEqual(accountDTO.Name,name);
            Assert.AreEqual(accountDTO.Currency,currency);
            Assert.AreEqual(accountDTO.CreationDate,creationDate);
        }

        [TestMethod]
        public void GivenAMonetaryAccount_ValidateThatItCanBeAppliedAsAPolimorficObject()
        {
            bool isASubClassFromAccount = false;
            
            MonetaryAccountDTO monetaryPolimorfed = new MonetaryAccountDTO("Brou", 1000, CurrencyEnumDTO.UY,
                DateTime.Now, 1);

            if (monetaryPolimorfed is AccountDTO)
            {
                isASubClassFromAccount = true;
            }
            Assert.IsTrue(isASubClassFromAccount);
        }
        [TestMethod]
        public void GivenACreditCardAccount_ValidateThatItCanBeAppliedAsAPolimorficObject()
        {
            bool isASubClassFromAccount = false;
            DateTime closingDate = DateTime.MaxValue;
            CreditCardAccountDTO creditCardAccountDTO = new CreditCardAccountDTO("Itau volar", CurrencyEnumDTO.UY,
                DateTime.Now, "Itau", "1234", 1000, closingDate, 1);

            if (creditCardAccountDTO is AccountDTO)
            {
                isASubClassFromAccount = true;
            }
            Assert.IsFalse(isASubClassFromAccount);
        }
    }
}