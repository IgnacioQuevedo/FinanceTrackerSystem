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
    }
}