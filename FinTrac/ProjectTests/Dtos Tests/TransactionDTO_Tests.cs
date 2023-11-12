using BusinessLogic.Dtos_Components;

namespace TestProject1
{
    [TestClass]
    public class TransactionDTO_Tests
    {

        [TestMethod]
        public void GivenTitleToSet_ShouldBeSetted()
        {

            string title = "Spent on popcorn";
            TransactionDTO transactionDTO = new TransactionDTO();

            transactionDTO.title = title;
            
            Assert.AreEqual(transactionDTO.title,title);
            
        }
    }
}