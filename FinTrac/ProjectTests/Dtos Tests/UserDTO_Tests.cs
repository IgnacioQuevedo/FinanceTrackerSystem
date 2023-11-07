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

        #region Password
        [TestMethod]
        public void GivenPassword_ShouldBeSetted()
        {
            string password = "Pass.Ignacio2023";
            UserDTO.Password = password;
            Assert.AreEqual(password,UserDTO.Password);
        }
        #endregion
        
        #region Email
        [TestMethod]
        public void GivenEmail_ShouldBeSetted()
        {
            string email = "nachitoquevedo@gmail.com";
            UserDTO.Email = email;

            Assert.AreEqual(email,UserDTO.Email);
        }
        #endregion

        #region Adress
        [TestMethod]
        public void GivenAddress_ShouldBeSetted()
        {
            string address = "Av Brasil 1215";

            UserDTO.Address = address;

            Assert.AreEqual(address,UserDTO.Address);

        }
        #endregion
        
        #region Setting an Id
        
        [TestMethod]
        public void GivenId_ShouldBeSetted()
        {
            int givenId = 1;
            UserDTO.UserId = 1;
            
            Assert.AreEqual(givenId, UserDTO.UserId);
        }
        #endregion
        
        #region Constructor
        [TestMethod]
        public void GivenValues_ShouldBePossibleToCreateAUserDTO()
        {
            string firstName = "Gonzalo";
            string lastName = "Camejo";
            string email = "gonchi@gmail.com";
            string password = "Pass.202304!";
            string address = "";

            UserDTO genericUserDTO = new UserDTO(firstName, lastName, email, password, address);

            Assert.AreEqual(firstName,genericUserDTO.FirstName);
            Assert.AreEqual(lastName,genericUserDTO.LastName);
            Assert.AreEqual(email,genericUserDTO.Email);
            Assert.AreEqual(address,genericUserDTO.Address);
        }
        #endregion
      
    }
}