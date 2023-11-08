using BusinessLogic.Dto_Components;
using BusinessLogic.User_Components;
using Controller;
using DataManagers;
using Controller.Mappers;

namespace ControllerTests
{
    [TestClass]
    public class MapperUserTests
    {
        #region Initialize

        private GenericController _controller;
        private SqlContext _testDb;
        private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();
        
        private UserRepositorySql _userRepo;
        private UserDTO _userDTO;
        private UserDTO _userConnected;

        [TestInitialize]
        public void Initialize()
        {
            _testDb = _contextFactory.CreateDbContext();
            _userRepo = new UserRepositorySql(_testDb);
            _controller = new GenericController(_userRepo);
            
            _userDTO = new UserDTO("Jhon", "Sans", "jhonnie@gmail.com", "Jhoooniee123", "");
            _userConnected = new UserDTO("Jhon", "Sans", "jhonnie@gmail.com", "Jhoooniee123!", "");
            _userConnected.UserId = 1;
            _userDTO.UserId = _userConnected.UserId;
            
            _controller.RegisterUser(_userConnected);
            _controller.SetUserConnected(_userConnected);
        }

        #endregion

        #region Cleanup

        [TestCleanup]
        public void CleanUp()
        {
            _testDb.Database.EnsureDeleted();
        }

        #endregion

        #region To User

        [TestMethod]
        public void GivenUserDTO_ShouldBePossibleToConvertItToUser()
        {
            User userConverted = MapperUser.ToUser(_userDTO);

            Assert.AreEqual(_userDTO.FirstName, userConverted.FirstName);
            Assert.AreEqual(_userDTO.LastName, userConverted.LastName);
            Assert.AreEqual(_userDTO.Email, userConverted.Email);
            Assert.AreEqual(_userDTO.Password, userConverted.Password);
            Assert.AreEqual(_userDTO.Address, userConverted.Address);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionController))]
        public void GivenUserDTOWithIncorrectData_ShoulThrowException()
        {
            UserDTO DtoWithBadValues = new UserDTO("Jhon", "Sans", "", "Jhoooniee123", "");
            User userConverted = MapperUser.ToUser(DtoWithBadValues);
        }

        #endregion

        #region To UserDTO

        [TestMethod]
        public void GivenUser_ShouldBePossibleToConvertItToUserDTO()
        {
            User userReceived = new User("Jhon", "Sans", "jhonnie@gmail.com", "Jhoooniee123", "");
            UserDTO userDto = MapperUser.ToUserDTO(userReceived);

            Assert.AreEqual(userDto.FirstName, userReceived.FirstName);
            Assert.AreEqual(userDto.LastName, userReceived.LastName);
            Assert.AreEqual(userDto.Email, userReceived.Email);
            Assert.AreEqual(userDto.Password, userReceived.Password);
            Assert.AreEqual(userDto.Address, userReceived.Address);
        }

        #endregion


        
    }
}