using BusinessLogic.Dtos_Components;

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
            
            Assert.AreEqual(transactionDTO.Title,title);
        }

        [TestMethod]
        public void GivenCreationDate_ShouldBeSetted()
        {
            DateTime creationDate = DateTime.Now.Date;

            transactionDTO.CreationDate = creationDate;
            
            Assert.AreEqual(transactionDTO.CreationDate,creationDate);

        }

        [TestMethod]

        public void GivenAmount_ShouldBeSetted()
        {
            decimal amount = 300.5M;

            transactionDTO.Amount = amount;
            
            Assert.AreEqual(transactionDTO.Amount,amount);
        }
        
        
    }
}