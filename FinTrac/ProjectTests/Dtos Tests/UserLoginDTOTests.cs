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

        #region Id

        [TestMethod]
        public void GivenId_ShouldBeSetted()
        {
            UserLoginDTO.UserId = genericId;
            Assert.AreEqual(genericId, UserLoginDTO.UserId);
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
