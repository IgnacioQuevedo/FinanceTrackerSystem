using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;

namespace TestProject1
{
    [TestClass]
    public class AccountDTO_Tests
    {
        private AccountDTO accountDTO;

        #region Initialize

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

        [TestMethod]
        public void GivenName_ShouldBeSetted()
        {
            string name = "Brou";
            accountDTO.Name = name;

            Assert.AreEqual(accountDTO.Name,name);

        }
    }
}