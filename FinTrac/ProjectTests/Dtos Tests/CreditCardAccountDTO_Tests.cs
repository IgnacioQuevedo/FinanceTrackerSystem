using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;

namespace TestProject1
{
    [TestClass]
    public class CreditCardAccountDTO_Tests
    {

        #region Initialize

        private CreditCardAccountDTO _creditCardAccountDTO;

        [TestInitialize]
        public void Initialize()
        {
            _creditCardAccountDTO = new CreditCardAccountDTO();
        }

        #endregion

        [TestMethod]
        public void GivenCredtiCardAccountId_ShouldBeSetted()
        {
            _creditCardAccountDTO.CreditCardAccountId = 1;

            Assert.AreEqual(_creditCardAccountDTO.CreditCardAccountId, 1);

        }



    }
}