using BusinessLogic.Dtos_Components;

namespace BusinessLogicTests.Dto_Components
{
    [TestClass]
    public class UserLoginDTO_Tests
    {
        #region Initialize
        private UserLoginDTO UserLoginDTO;
        private string genericEmail;

        [TestInitialize]
        public void Initialize()
        {
            UserLoginDTO = new UserLoginDTO();
            genericEmail = "someone@example.com";
        }
        #endregion

        #region Email

        [TestMethod]
        public void GivenEmail_ShouldBeSetted()
        {
            UserLoginDTO.Email = genericEmail;
            Assert.AreEqual(genericEmail, UserLoginDTO.Email);
        }

        #endregion

    }
}
