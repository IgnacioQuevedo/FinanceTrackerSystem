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
        private SqlContext _testDb;
        private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();

        [TestInitialize]
        public void Initialize()
        {
            _testDb = _contextFactory.CreateDbContext();
            _controller = new Controller(_testDb);
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
            UserDTO userDto = new UserDTO("Jhon", "Sans", "jhonnie@gmail.com", "Jhoooniee123", "");

            User userConverted = _controller.ToUser(userDto);

            Assert.AreEqual(userDto.FirstName, userConverted.FirstName);
            Assert.AreEqual(userDto.LastName, userConverted.LastName);
            Assert.AreEqual(userDto.Email, userConverted.Email);
            Assert.AreEqual(userDto.Password, userConverted.Password);
            Assert.AreEqual(userDto.Address, userConverted.Address);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionController))]
        public void GivenUserDTOWithIncorrectData_ShoulThrowException()
        {
            UserDTO userDto = new UserDTO("Jhon", "Sans", "", "Jhoooniee123", "");
            User userConverted = _controller.ToUser(userDto);
        }

        #endregion

        #region To UserDTO

        [TestMethod]
        public void GivenUser_ShouldBePossibleToConvertItToUserDTO()
        {
            User userReceived = new User("Jhon", "Sans", "jhonnie@gmail.com", "Jhoooniee123", "");
            UserDTO userDto = _controller.ToDtoUser(userReceived);

            Assert.AreEqual(userDto.FirstName, userReceived.FirstName);
            Assert.AreEqual(userDto.LastName, userReceived.LastName);
            Assert.AreEqual(userDto.Email, userReceived.Email);
            Assert.AreEqual(userDto.Password, userReceived.Password);
            Assert.AreEqual(userDto.Address, userReceived.Address);
        }

        #endregion

        #region FindUser

        [TestMethod]
        public void GivenUserDTO_ShouldBePossibleToFindUserRelatedInDb()
        {
            UserDTO dtoToFound = new UserDTO("Jhon", "Sans", "jhonny@gmail.com", "Jhooony12345", "");
            _testDb.Add(_controller.ToUser(dtoToFound));
            _testDb.SaveChanges();

            User userFound = _controller.FindUser(_controller.ToUser(dtoToFound).Email);

            Assert.AreEqual(userFound.Email, dtoToFound.Email);
        }

        [TestMethod]
        public void GivenUserDTOThatUserIsNotRegitered_ShouldReturnNULL()
        {
            UserDTO dtoToFound = new UserDTO("Jhon", "Sans", "jhonny@gmail.com", "Jhooony12345", "");
            _testDb.Add(_controller.ToUser(dtoToFound));
            _testDb.SaveChanges();

            User userFound = _controller.FindUser(_controller.ToUser(dtoToFound).Password);

            Assert.IsNull(userFound);
        }

        #endregion

        #region Create

        [TestMethod]
        public void CreateUserMethod_ShouldAddNewUserIntoDb()
        {
            UserDTO userToAdd = new UserDTO("Kenny", "Dock", "kennies@gmail.com",
                "KennieDock222", "North Av");

            _controller.CreateUser(userToAdd);

            User userInDb = _controller.FindUser(userToAdd.Email);
            Assert.AreEqual(userToAdd.Email, userInDb.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionController))]
        public void CreateUserAlreadyRegistered_ShouldThrowException()
        {
            UserDTO userToAdd = new UserDTO("Kenny", "Dock", "kennies@gmail.com",
                "KennieDock222", "North Av");
            _controller.CreateUser(userToAdd);
            _controller.CreateUser(userToAdd);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ExceptionController))]
        public void CreatingUserWithSameEmailButDifferentUpperCase_ShouldReturnException()
        {
            UserDTO userAdded = new UserDTO("Kenny", "Dock", "kennies@gmail.com",
                "KennieDock222", "North Av");
            _controller.CreateUser(userAdded);
            
            UserDTO userWithSameEmail = new UserDTO("Kenny", "Dock", "keNNieS@gmail.com",
                "KennieDock222", "North Av");
            _controller.CreateUser(userWithSameEmail);
            
        }

        #endregion
        
    }
}