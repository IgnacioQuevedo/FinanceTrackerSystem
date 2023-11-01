using BusinessLogic.Dto_Components;

namespace TestProject1
{
    [TestClass]
    public class UserDTO_Tests
    {
        #region Initialize
        private UserDTO UserDTO;

        [TestInitialize]
        public void Initialize()
        {
            UserDTO = new UserDTO();
        }
        #endregion
        
        #region FirstName
        [TestMethod]
        public void GivenFirstName_ShouldBeSetted()
        {
            string firstName = "Ignacio";
            UserDTO.FirstName = firstName;

            Assert.AreEqual(firstName, UserDTO.FirstName);
        }
        #endregion

        #region LastName
        [TestMethod]
        public void GivenLastName_ShouldBeSetted()
        {
            string lastName = "Quevedo";
            UserDTO userDto = new UserDTO();
            userDto.LastName = lastName;
            
            Assert.AreEqual(lastName,userDto.LastName);
        }
        #endregion
       
        #region Email

        [TestMethod]
        public void GivenEmail_ShouldBeSetted()
        {
            string email = "nachitoquevedo@gmail.com";
            UserDTO.Email = email;

            Assert.AreEqual(email.UserDTO.Email);
        }

        #endregion
    }
}