using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;

namespace TestProject1
{
    [TestClass]
    public class MonetaryAccountDTO_Tests
    {
        #region Name

        [TestMethod]

        public void GivenName_ShouldBeSetted()
        {
            MonetaryAccountDTO myMonetaryAccountDTO = new MonetaryAccountDTO();
            string nameToSet = "Brou";

            myMonetaryAccountDTO.Name = "Brou";

            Assert.AreEqual(myMonetaryAccountDTO.Name, nameToSet);
        }

        #endregion

        #region Amount

        [TestMethod]
        public void GivenAmount_ShouldBeSetted()
        {
            MonetaryAccountDTO myMonetaryAccountDTO = new MonetaryAccountDTO();

            decimal amountToSet = 1000;
            myMonetaryAccountDTO.Amount = amountToSet;

            Assert.AreEqual(myMonetaryAccountDTO.Amount, amountToSet);
        }

        #endregion

        [TestMethod]
        public void GivenCurrency_ShouldBeSetted()
        {
            MonetaryAccountDTO myMonetaryAccountDTO = new MonetaryAccountDTO();

            myMonetaryAccountDTO.Currency = CurrencyEnumDTO.EUR;

            Assert.AreEqual(myMonetaryAccountDTO.Currency, CurrencyEnumDTO.EUR);
        }
    }
}