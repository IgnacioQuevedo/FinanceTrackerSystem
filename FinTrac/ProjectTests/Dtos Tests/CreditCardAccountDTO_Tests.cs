using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;

namespace TestProject1
{
    [TestClass]
    public class CreditCardAccountDTO_Tests
    {

        #region Initialize

        private CreditCardAccountDTO _creditCardAccountDTO;
        private int _creditCardAccountId;

        [TestInitialize]
        public void Initialize()
        {
            _creditCardAccountDTO = new CreditCardAccountDTO();
            _creditCardAccountId = 1;
        }

        #endregion

        #region Credit Card Account Id

        [TestMethod]
        public void GivenCredtiCardAccountId_ShouldBeSetted()
        {
            _creditCardAccountDTO.CreditCardAccountId = _creditCardAccountId;

            Assert.AreEqual(_creditCardAccountDTO.CreditCardAccountId, _creditCardAccountId);

        }

        #endregion  

        [TestMethod]
        public void GivenName_ShouldBeSetted()
        {
            _creditCardAccountDTO.Name = "Something";

            Assert.AreEqual(_creditCardAccountDTO.Name, "Something");
        }
    }
}