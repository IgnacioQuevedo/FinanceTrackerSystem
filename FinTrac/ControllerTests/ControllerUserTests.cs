using BusinessLogic.Dtos_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Goal_Components;
using BusinessLogic.User_Components;
using Controller;
using DataManagers;

namespace ControllerTests
{
    [TestClass]
    public class ControllerUserTests
    {
        #region Initialize

        private GenericController _controller;
        private SqlContext _testDb;
        private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();

        private UserRepositorySql _userRepo;
        private UserDTO _userDTO;
        private UserDTO _userConnected;
        private UserLoginDTO _userLoginDTO;

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

            _userLoginDTO = new UserLoginDTO(1, "jhonnie@gmail.com", "Jhoooniee123");


            _controller.RegisterUser(_userConnected);
            _controller.SetUserConnected(_userConnected.UserId);
        }

        #endregion

        #region Cleanup

        [TestCleanup]
        public void CleanUp()
        {
            _testDb.Database.EnsureDeleted();
        }

        #endregion

        #region FindUser

        [TestMethod]
        public void GivenUserDTO_ShouldBePossibleToFindUserRelatedInDb()
        {
            UserDTO userFound = _controller.FindUser(_userDTO.UserId);

            Assert.AreEqual(userFound.Email, _userDTO.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GivenUserDTOThatUserIsNotRegistered_ShouldReturnNULL()
        {
            _userDTO.UserId = 1000000;
            UserDTO userFound = _controller.FindUser(_userDTO.UserId);

            Assert.IsNull(userFound);
        }

        #endregion

        #region Register

        [TestMethod]
        public void RegisterMethod_ShouldAddNewUserIntoDb()
        {
            UserDTO userToAdd = new UserDTO("Kenny", "Dock", "kennies@gmail.com",
                "KennieDock222", "North Av");
            userToAdd.UserId = 2;

            _controller.RegisterUser(userToAdd);


            UserDTO userInDb = _controller.FindUser(userToAdd.UserId);
            Assert.AreEqual(userToAdd.Email, userInDb.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegisterUserAlreadyRegistered_ShouldThrowException()
        {
            UserDTO userToAdd = new UserDTO("Kenny", "Dock", "kennies@gmail.com",
                "KennieDock222", "North Av");
            _controller.RegisterUser(userToAdd);
            _controller.RegisterUser(userToAdd);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegisteringUserWithSameEmailButDifferentUpperCase_ShouldThrowException()
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
            string passwordRepeated = _userDTO.Password;
            _controller.PasswordMatch(_userDTO.Password, passwordRepeated);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GivenUserToCreateWithDifferentPasswords_ShouldThrowException()
        {
            string passwordIncorrect = "passwordIncorrect";
            _controller.PasswordMatch(_userDTO.Password, passwordIncorrect);
        }

        #endregion

        #region Update

        [TestMethod]
        public void GivenUserToUpdate_ShouldBeUpdatedInDb()
        {
            _controller.UpdateUser(_userDTO);

            UserDTO userInDb = _controller.FindUser(_userDTO.UserId);
            Assert.AreEqual(userInDb.FirstName, _userDTO.FirstName);
            Assert.AreEqual(userInDb.LastName, _userDTO.LastName);
            Assert.AreEqual(userInDb.Password, _userDTO.Password);
            Assert.AreEqual(userInDb.Address, _userDTO.Address);
            Assert.AreEqual(userInDb.Email, _userDTO.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GivenUserToUpdateButWithoutAnyChanges_ShouldThrowException()
        {
            UserDTO newDtoWithoutChanges = new UserDTO("Jhon", "Sans", "jhonnie@gmail.com", "Jhoooniee123!", "");
            ;
            newDtoWithoutChanges.UserId = _userConnected.UserId;

            _controller.UpdateUser(newDtoWithoutChanges);
        }

        #endregion

        #region Login

        [TestMethod]
        public void GivenUserDTO_ShouldBePossibleToLogin()
        {
            _userLoginDTO.Email = "jhonnie@gmail.com";
            _userLoginDTO.Password = "Jhoooniee123!";

            Assert.IsTrue(_controller.LoginUser(_userLoginDTO));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GivenUserNotRegisteredWhenTryingToLogin_ShouldThrowException()
        {
            _controller.LoginUser(_userLoginDTO);
        }

        #endregion

        #region SettingUserConected

        [TestMethod]
        public void GivenUserDTO_ShouldSetUserConnected()
        {
            UserDTO userToConnect = new UserDTO("Ignacio", "Quevedo",
                "nachitoquevedo@gmail.com", "Nacho200304!", "");

            _controller.SetUserConnected(userToConnect.UserId);
        }

        #endregion
    }
}