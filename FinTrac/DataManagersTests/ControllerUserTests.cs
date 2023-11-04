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
        private UserDTO userDto;

        [TestInitialize]
        public void Initialize()
        {
            _testDb = _contextFactory.CreateDbContext();
            _controller = new Controller(_testDb);
            userDto = new UserDTO("Jhon", "Sans", "jhonnie@gmail.com", "Jhoooniee123", "");
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
            UserDTO DtoWithBadValues = new UserDTO("Jhon", "Sans", "", "Jhoooniee123", "");
            User userConverted = _controller.ToUser(DtoWithBadValues);
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
            _testDb.Add(_controller.ToUser(userDto));
            _testDb.SaveChanges();

            User userFound = _controller.FindUser(_controller.ToUser(userDto).Email);

            Assert.AreEqual(userFound.Email, userDto.Email);
        }

        [TestMethod]
        public void GivenUserDTOThatUserIsNotRegitered_ShouldReturnNULL()
        {
            _testDb.Add(_controller.ToUser(userDto));
            _testDb.SaveChanges();

            User userFound = _controller.FindUser(_controller.ToUser(userDto).Password);

            Assert.IsNull(userFound);
        }

        #endregion

        #region Register

        [TestMethod]
        public void RegisterMethod_ShouldAddNewUserIntoDb()
        {
            UserDTO userToAdd = new UserDTO("Kenny", "Dock", "kennies@gmail.com",
                "KennieDock222", "North Av");

            _controller.RegisterUser(userToAdd);

            User userInDb = _controller.FindUser(userToAdd.Email);
            Assert.AreEqual(userToAdd.Email, userInDb.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionController))]
        public void RegisterUserAlreadyRegistered_ShouldThrowException()
        {
            UserDTO userToAdd = new UserDTO("Kenny", "Dock", "kennies@gmail.com",
                "KennieDock222", "North Av");
            _controller.RegisterUser(userToAdd);
            _controller.RegisterUser(userToAdd);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionController))]
        public void RegisteringUserWithSameEmailButDifferentUpperCase_ShouldReturnException()
        {
            UserDTO userAdded = new UserDTO("Kenny", "Dock", "kennies@gmail.com",
                "KennieDock222", "North Av");
            _controller.RegisterUser(userAdded);

            UserDTO userWithSameEmail = new UserDTO("Kenny", "Dock", "keNNieS@gmail.com",
                "KennieDock222", "North Av");
            _controller.RegisterUser(userWithSameEmail);
        }

        [TestMethod]
        public void GivenUserToCreate_PasswordsMustMatch()
        {
            string passwordRepeated = userDto.Password;
            bool passwordMatch = _controller.PasswordMatch(userDto.Password, passwordRepeated);
                
            Assert.IsTrue(passwordMatch);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ExceptionController))]
        public void GivenUserToCreateWithDifferentPasswords_ShouldThrowException()
        {
            string passwordIncorrect = "passwordIncorrect";
            _controller.PasswordMatch(userDto.Password, passwordIncorrect);

        }
        #endregion

        #region Update

        [TestMethod]
        public void GivenUserToUpdate_ShouldBeUpdatedInDb()
        {
            UserDTO dtoToAdd = new UserDTO("Kenny", "Dock", "kennies@gmail.com",
                "KennieDock222", "North Av");
            UserDTO dtoWithUpdates = new UserDTO("Jhonix", "Loxed", "kennies@gmail.com",
                "Jhonix2003!!", "South Av");

            _controller.RegisterUser(dtoToAdd);
            _controller.UpdateUser(dtoWithUpdates);
            User userInDb = _controller.FindUser(dtoToAdd.Email);

            Assert.AreEqual(userInDb.FirstName, dtoWithUpdates.FirstName);
            Assert.AreEqual(userInDb.LastName, dtoWithUpdates.LastName);
            Assert.AreEqual(userInDb.Password, dtoWithUpdates.Password);
            Assert.AreEqual(userInDb.Address, dtoWithUpdates.Address);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionController))]
        public void GivenUserToUpdateButWithoutAnyChanges_ShouldThrowException()
        {
            UserDTO dtoToAdd = new UserDTO("Kenny", "Dock", "kennies@gmail.com",
                "KennieDock222", "North Av");
            UserDTO newDtoWithoutChanges = new UserDTO("Kenny", "Dock", "kennies@gmail.com",
                "KennieDock222", "North Av");

            _controller.RegisterUser(dtoToAdd);
            _controller.UpdateUser(newDtoWithoutChanges);
        }

        #endregion

        #region Login
        
        [TestMethod]
        public void GivenUserInDb_ShouldBePossibleToLogin()
        {
            _controller.RegisterUser(userDto);
            Assert.IsTrue(_controller.LoginUser(userDto));
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionController))]
        public void GivenUserNotRegisteredWhenTryingToLogin_ShouldThrowException()
        {
            _controller.LoginUser(userDto);
        }
        
        #endregion
        
    }
}