using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;

namespace TestProject1
{
    [TestClass]
    public class MonetaryAccountDTO_Tests
    {
        [TestMethod]

        public void GivenName_ShouldBeSetted()
        {
            MonetaryAccountDTO myMonetaryAccountDTO = new MonetaryAccountDTO();
            myMonetaryAccountDTO.Name = "Brou";

            Assert.AreEqual(myMonetaryAccountDTO.Name, "Brou");
        }
    }
}