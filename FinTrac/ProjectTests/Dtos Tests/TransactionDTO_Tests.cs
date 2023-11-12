using BusinessLogic.Dtos_Components;

namespace BusinessLogicTests.Dtos_Tests
{
    [TestClass]
    public class TransactionDTO_Tests
    {

        [TestMethod]
        public void GivenTitleToSet_ShouldBeSetted()
        {

            string title = "Spent on popcorn";
            TransactionDTO transactionDTO = new TransactionDTO();

            transactionDTO.Title = title;
            
            Assert.AreEqual(transactionDTO.Title,title);
        }
    }
}