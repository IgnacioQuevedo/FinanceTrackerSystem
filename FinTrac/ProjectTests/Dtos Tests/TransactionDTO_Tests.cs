using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;

namespace BusinessLogicTests.Dtos_Tests
{
    [TestClass]
    public class TransactionDTO_Tests
    {
        #region Initialize

        private TransactionDTO transactionDTO;

        [TestInitialize]
        public void Initialize()
        {
            transactionDTO = new TransactionDTO();
        }

        #endregion

        #region Title

        [TestMethod]
        public void GivenTitleToSet_ShouldBeSetted()
        {
            string title = "Spent on popcorn";
            transactionDTO.Title = title;

            Assert.AreEqual(transactionDTO.Title, title);
        }

        #endregion

        #region Creation Date

        [TestMethod]
        public void GivenCreationDate_ShouldBeSetted()
        {
            DateTime creationDate = DateTime.Now.Date;

            transactionDTO.CreationDate = creationDate;

            Assert.AreEqual(transactionDTO.CreationDate, creationDate);
        }

        #endregion

        #region Amount

        [TestMethod]
        public void GivenAmount_ShouldBeSetted()
        {
            decimal amount = 300.5M;

            transactionDTO.Amount = amount;

            Assert.AreEqual(transactionDTO.Amount, amount);
        }

        #endregion

        #region Currency

        [TestMethod]
        public void GivenCurrencyDTO_ShouldBeSetted()
        {
            CurrencyEnumDTO currencyEnumDTO = CurrencyEnumDTO.UY;
            transactionDTO.Currency = currencyEnumDTO;
            Assert.AreEqual(transactionDTO.Currency, currencyEnumDTO);

            currencyEnumDTO = CurrencyEnumDTO.USA;
            transactionDTO.Currency = currencyEnumDTO;
            Assert.AreEqual(transactionDTO.Currency, currencyEnumDTO);

            currencyEnumDTO = CurrencyEnumDTO.EUR;
            transactionDTO.Currency = currencyEnumDTO;
            Assert.AreEqual(transactionDTO.Currency, currencyEnumDTO);
        }

        #endregion

        #region Type

        [TestMethod]
        public void GivenType_ShouldBeSetted()
        {
            TypeEnumDTO typeEnumDTO = TypeEnumDTO.Income;
            transactionDTO.Type = typeEnumDTO;
            Assert.AreEqual(transactionDTO.Type, typeEnumDTO);

            typeEnumDTO = TypeEnumDTO.Outcome;
            transactionDTO.Type = typeEnumDTO;
            Assert.AreEqual(transactionDTO.Type, typeEnumDTO);
        }

        #endregion

        #region Category Of Transaction

        [TestMethod]
        public void GivenCategoryDTO_ShouldBeSetted()
        {
            CategoryDTO transactionCategory = new CategoryDTO("Food", StatusEnumDTO.Enabled, TypeEnumDTO.Income, 1);

            transactionDTO.TransactionCategory = transactionCategory;

            Assert.AreEqual(transactionDTO.TransactionCategory, transactionCategory);
        }

        #endregion

        #region Setting Id

        [TestMethod]
        public void GivenIdToSet_ShouldBeSetted()
        {
            int transactionId = 1;
            transactionDTO.TransactionId = 1;
            Assert.AreEqual(transactionDTO.TransactionId, transactionId);
        }

        #endregion

        [TestMethod]
        public void GivenAccountId_ShouldBeSetted()
        {
            int accountId = 1;
            transactionDTO.accountId = accountId;
            
            Assert.AreEqual(transactionDTO.accountId,accountId);
        }

        #region Constructor

        [TestMethod]
        public void GivenValuesToSet_ShouldBePossibleToDefineATransactionDTO()
        {
            int transactionId = 1;
            string title = "Spent on popcorn";
            DateTime creationDate = DateTime.Now.Date;
            decimal amount = 300.5M;
            CurrencyEnumDTO currency = CurrencyEnumDTO.UY;
            TypeEnumDTO type = TypeEnumDTO.Income;
            CategoryDTO transactionCategory = new CategoryDTO("Food", StatusEnumDTO.Enabled, TypeEnumDTO.Income, 1);

            TransactionDTO transactionDTO = new TransactionDTO(transactionId, title, creationDate, amount, currency,
                type, transactionCategory);

            Assert.AreEqual(transactionDTO.TransactionId, transactionId);
            Assert.AreEqual(transactionDTO.Title, title);
            Assert.AreEqual(transactionDTO.CreationDate, creationDate);
            Assert.AreEqual(transactionDTO.Amount, amount);
            Assert.AreEqual(transactionDTO.Currency, currency);
            Assert.AreEqual(transactionDTO.Type, type);
            Assert.AreEqual(transactionDTO.TransactionCategory, transactionCategory);
        }

        #endregion
    }
}