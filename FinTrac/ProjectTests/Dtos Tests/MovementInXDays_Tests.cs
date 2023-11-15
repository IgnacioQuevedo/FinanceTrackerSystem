using BusinessLogic.Dtos_Components;

namespace BusinessLogicTests.Dto_Components
{
    [TestClass]
    public class MovementInXDays_Tests
    {
        #region Initialize
        private UserLoginDTO UserLoginDTO;
        private string genericEmail;
        private string genericPassword;
        private int genericId;

        [TestInitialize]
        public void Initialize()
        {
            UserLoginDTO = new UserLoginDTO();
            genericEmail = "someone@example.com";
            genericPassword = "ABCDE12345678";
            genericId = 1;
        }
        #endregion

        [TestMethod]
        public void GivenSpendingArray_ShouldBeSet()
        {
            int[] spendings = new int[31];
            MovementInXDaysDTO movements = new MovementInXDaysDTO();
            movements.Spendings = spendings;
            
            Assert.AreEqual(spendings,movements.Spendings);

        }


    }
}
