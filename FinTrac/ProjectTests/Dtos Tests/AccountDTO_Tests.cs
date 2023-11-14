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

        #region IsMonetary or not

        [TestMethod]
        public void GivenBoolThatDeterminesIfTheAccountIsMonetary_ShouldBeSetted()
        {
            bool isMonetary = false;
            accountDTO.isMonetary = isMonetary;

            Assert.IsFalse(isMonetary);
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
            
        }
    }
}