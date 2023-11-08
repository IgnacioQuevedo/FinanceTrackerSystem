using BusinessLogic.Dtos_Components;

namespace BusinessLogicTests.Dto_Components
{
    [TestClass]
    public class UserLoginDTO_Tests
    {
        #region Initialize
        private UserLoginDTO UserLoginDTO;
        private string genericEmail;
        private string genericPassword;

        [TestInitialize]
        public void Initialize()
        {
            UserLoginDTO = new UserLoginDTO();
            genericEmail = "someone@example.com";
            genericPassword = "ABCDE12345678";
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

        #region Password

        [TestMethod]
        public void GivenPassword_ShouldBeSetted()
        {
            UserLoginDTO.Password = genericPassword;
            Assert.AreEqual(genericPassword, UserLoginDTO.Password);
        }

        #endregion


        [TestMethod]
        public void GivenValues_ShouldCreateUserLoginDTO()
        {
            string email = "someone@example.com";
            string password = "12233242ADADSAsd";
            UserLoginDTO = new UserLoginDTO(email, password);
            Assert.AreEqual(email, UserLoginDTO.Email);
            Assert.AreEqual("A", UserLoginDTO.Password);
        }



    }
}
