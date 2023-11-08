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

        [TestMethod]
        public void GivenId_ShouldBeSetted()
        {
            UserLoginDTO.UserId = 1;
            Assert.AreEqual(3, UserLoginDTO.UserId);
        }

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

        #region Constructor

        [TestMethod]
        public void GivenValues_ShouldCreateUserLoginDTO()
        {
            UserLoginDTO = new UserLoginDTO(genericEmail, genericPassword);

            Assert.AreEqual(genericEmail, UserLoginDTO.Email);
            Assert.AreEqual(genericPassword, UserLoginDTO.Password);
        }

        #endregion

    }
}
