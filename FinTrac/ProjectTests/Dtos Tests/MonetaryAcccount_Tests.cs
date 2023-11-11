using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;

namespace TestProject1
{
    [TestClass]
    public class MonetaryAccountDTO_Tests
    {

        [TestMethod]
        public void GivenMonetaryId_ShouldBeSetted()
        {
            MonetaryAccountDTO myMonetaryAccountDTO = new MonetaryAccountDTO();

            myMonetaryAccountDTO.MonetaryAccountId = 1;

            Assert.AreEqual(myMonetaryAccountDTO.MonetaryAccountId, 0);
        }


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

        #region Currency

        [TestMethod]
        public void GivenCurrency_ShouldBeSetted()
        {
            MonetaryAccountDTO myMonetaryAccountDTO = new MonetaryAccountDTO();

            CurrencyEnumDTO currencyToSet = CurrencyEnumDTO.EUR;
            myMonetaryAccountDTO.Currency = currencyToSet;

            Assert.AreEqual(myMonetaryAccountDTO.Currency, currencyToSet);
        }

        #endregion
    }
}