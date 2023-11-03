using BusinessLogic.Dto_Components;
using BusinessLogic.User_Components;
using DataManagers;


namespace DataManagersTests
{
    [TestClass]
    public class ControllerUserTests
    {

        #region Initialize
        
        private Controller _controller;
    
        [TestInitialize]

        public void Initialize()
        {
            _controller = new Controller();

        }
        
        #endregion

        #region ToUser

        

        
        [TestMethod]
        public void GivenUserDTO_ShouldBePossibleToConvertItToUser()
        {
            UserDTO userDto = new UserDTO("Jhon", "Sans", "jhonnie@gmail.com", "Jhoooniee123", "");

            User userConverted = _controller.toUser(userDto);
            
            Assert.AreEqual(userDto.FirstName,userConverted.FirstName);
            Assert.AreEqual(userDto.LastName,userConverted.LastName);
            Assert.AreEqual(userDto.Email,userConverted.Email);
            Assert.AreEqual(userDto.Password,userConverted.Password);
            Assert.AreEqual(userDto.Address,userConverted.Address);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionController))]
        public void GivenUserDTOWithIncorrectData_ShoulThrowException()
        {
            UserDTO userDto = new UserDTO("Jhon", "Sans", "", "Jhoooniee123", "");

            User userConverted = _controller.toUser(userDto);
            
            
        }
        
        
        
        #endregion
        
    }
}