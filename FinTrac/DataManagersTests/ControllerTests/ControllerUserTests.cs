using BusinessLogic.Dto_Components;
using BusinessLogic.User_Components;
using DataManagers;


namespace DataManagersTests
{
    [TestClass]
    public class ControllerUserTests
    {

        private Controller _controller;
    
        [TestInitialize]

        public void Initialize()
        {
            _controller = new Controller();

        }

        [TestMethod]
        public void GivenUserDTO_ShouldBePossibleToConvertItToUser()
        {
            UserDTO userDto = new UserDTO("Jhon", "Sans", "jhonnie@gmail.com", "Jhoooniee123", "");

            User userConverted = _controller.toUser(userDto);
            
            Assert.AreEqual(userDto,userConverted);

        }
        
        
        
    }
}