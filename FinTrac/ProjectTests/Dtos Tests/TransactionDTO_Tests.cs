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
    }
}