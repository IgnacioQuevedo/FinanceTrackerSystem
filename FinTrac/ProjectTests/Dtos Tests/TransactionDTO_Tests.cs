using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;

namespace BusinessLogicTests.Dtos_Tests
{
    [TestClass]
    public class TransactionDTO_Tests
    {
        private TransactionDTO transactionDTO;

        [TestInitialize]
        public void Initialize()
        {
            transactionDTO = new TransactionDTO();
        }


        [TestMethod]
        public void GivenTitleToSet_ShouldBeSetted()
        {
            string title = "Spent on popcorn";
            transactionDTO.Title = title;

            Assert.AreEqual(transactionDTO.Title, title);
        }

        [TestMethod]
        public void GivenCreationDate_ShouldBeSetted()
        {
            DateTime creationDate = DateTime.Now.Date;

            transactionDTO.CreationDate = creationDate;

            Assert.AreEqual(transactionDTO.CreationDate, creationDate);
        }

        [TestMethod]
        public void GivenAmount_ShouldBeSetted()
        {
            decimal amount = 300.5M;

            transactionDTO.Amount = amount;

            Assert.AreEqual(transactionDTO.Amount, amount);
        }

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

        [TestMethod]
        public void GivenCurrency_ShouldBeSetted()
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

        [TestMethod]
        public void GivenCategoryDTO_ShouldBeSetted()
        {
            CategoryDTO transactionCategory = new CategoryDTO("Food", StatusEnumDTO.Enabled, TypeEnumDTO.Income, 1);

            transactionDTO.TransactionCategory = transactionCategory;
            
            Assert.AreEqual(transactionDTO.TransactionCategory,transactionCategory);

        }

        [TestMethod]
        public void GivenIdToSet_ShouldBeSetted()
        {
            int transactionId = 1;
            transactionDTO.Id = 1;
            Assert.AreEqual(transactionDTO.Id,transactionId);
        }
    }
}