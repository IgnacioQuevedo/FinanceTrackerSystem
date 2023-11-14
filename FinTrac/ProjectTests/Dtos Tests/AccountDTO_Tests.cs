using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;

namespace TestProject1
{
    [TestClass]
    public class AccountDTO_Tests
    {

        #region Initialize
        
        [TestInitialize]
        public void Initialize()
        {
       
        }
        #endregion

        [TestMethod]
        public void GivenAccountId_ShouldBeSetted()
        {
            int accountId = 1;

            AccountDTO accountDTO = new AccountDTO();
            accountDTO.AccountId = accountId;
            
            Assert.AreEqual(accountDTO.AccountId,accountId);
            
            
            
        }
   
        
        
        
        

    }
