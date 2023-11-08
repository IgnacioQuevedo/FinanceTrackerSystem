using BusinessLogic.Account_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.Transaction_Components;
using BusinessLogic.User_Components;
using DataManagers;


namespace DataManagersTests
{
    [TestClass]
    public class UserRepositorySqlTests
    {
        #region Initialize

        private UserRepositorySql _userRepo;
        private SqlContext _testDb;
        private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();
        private User _genericUser;
        private UserDTO _genericUserDTO;
        private UserLoginDTO _genericUserLoginDTO;


        [TestInitialize]
        public void TestInitialize()
        {
            _testDb = _contextFactory.CreateDbContext();
            _userRepo = new UserRepositorySql(_testDb);
            _genericUser = new User("Jhon", "Sans", "jhonny@gmail.com", "Jhooony12345", "");
            _genericUserDTO = new UserDTO("Jhon", "Sans", "jhonny@gmail.com", "Jhooony12345", "");
            _genericUserLoginDTO = new UserLoginDTO("jhonny@gmail.com", "Jhooony12345");
        }

        #endregion

        #region Cleanup

        [TestCleanup]
        public void CleanUp()
        {
            _testDb.Database.EnsureDeleted();
        }

        #endregion

        #region Creation and Necessary Validations

        [TestMethod]
        public void CreateMethodWithCorrectValues_ShouldAddNewUser()
        {
            User userToAdd = new User("Kenny", "Dock", "kennies@gmail.com", "KennieDock222", "North Av");
            _userRepo.Create(userToAdd);

            User userInDb = _testDb.Users.First();
            Assert.AreEqual(userToAdd, userInDb);
        }

        #endregion

        #region Email Mapping

        [TestMethod]
        public void GivenNotRegisteredEmail_ShouldBeAllGood()
        {
            UserDTO userToCheckEmail = new UserDTO("Kent", "Beck", "michsanta@gmail.com", "JohnBeck1961", "NW 3rd Ave");

            _userRepo.EmailUsed(userToCheckEmail.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionUserRepository))]
        public void GivenAlreadyRegisteredEmail_ShouldThrowException()
        {
            User userToAdd = new User("Mich", "Santa", "michsanta@gmail.com", "Mich123456789", "North Av 23");
            _userRepo.Create(userToAdd);

            UserDTO incorrectUser = new UserDTO("Kent", "Beck", "michsanta@gmail.com", "JohnBeck1961", "NW 3rd Ave");

            _userRepo.EmailUsed(incorrectUser.Email);
        }

        #endregion

        #region Login

        [TestMethod]
        public void GivenUserThatWantsToLogin_ShouldBePossibleToCheckIfHisLoginDataIsCorrect()
        {
            _userRepo.Create(_genericUser);

            Assert.IsTrue(_userRepo.Login(_genericUserLoginDTO));
        }

        [TestMethod]
        public void GivenUserThatWantsToLoginButIsNotRegistered_ShouldReturnFalse()
        {
            User userNotRegistered = new User("Jhon", "Camaleon", "jhonnya@gmail.com", "LittleJhonny123", "");
            _genericUserLoginDTO.Email = "jhonnya@gmail.com";
            _genericUserLoginDTO.Password = "LittleJhonny123";

            Assert.IsFalse(_userRepo.Login(_genericUserLoginDTO));
        }

        #endregion

        #region Update

        [TestMethod]
        public void GivenAspectsOfUserToUpdate_ShouldBeUpdated()
        {
            _userRepo.Create(_genericUser);

            User userUpdated = new User("Jhonnyx", "Sanz", "jhonny@gmail.com", "Jhooony12345", "NW 2nd Ave");
            userUpdated.UserId = _genericUser.UserId;

            _userRepo.Update(userUpdated);

            User userInDb = _testDb.Users.First();

            Assert.AreEqual(userUpdated.FirstName, userInDb.FirstName);
            Assert.AreEqual(userUpdated.LastName, userInDb.LastName);
            Assert.AreEqual(userUpdated.Password, userInDb.Password);
            Assert.AreEqual(userUpdated.Address, userInDb.Address);
        }

        #endregion

        #region Id settings

        [TestMethod]
        public void WhenUserIsCreated_AnIdMustBeAssigned()
        {
            _userRepo.Create(_genericUser);
            Assert.AreEqual(1, _testDb.Users.First().UserId);
        }

        #endregion

        #region Find

        [TestMethod]
        public void GivenAnId_UserShouldBeFound()
        {
            _userRepo.Create(_genericUser);

            User userFound = _userRepo.FindUserInDb(_genericUser.UserId);

            Assert.AreEqual(userFound, _genericUser);
        }

        #endregion

        [TestMethod]
        public void GivenUserConnected_ShouldInstanceUserLists()
        {
            _userRepo.Create(_genericUser);
            User userFromDb = _userRepo.FindUserInDb(_genericUser.UserId);
            userFromDb = _userRepo.InstanceLists(userFromDb);

            Category category = new Category("Food", StatusEnum.Enabled, TypeEnum.Income);
            Transaction transactionExample = new Transaction("Gonnak Restaurant", 100, DateTime.Now, CurrencyEnum.UY, TypeEnum.Income, category);
            MonetaryAccount monetaryAccount = new MonetaryAccount("Brou", 4000, CurrencyEnum.UY, DateTime.Now);

            userFromDb.AddCategory(category);
            userFromDb.AddMonetaryAccount(monetaryAccount);
            userFromDb.MyAccounts[0].AddTransaction(transactionExample);
            _userRepo.Update(userFromDb);

            userFromDb = _userRepo.InstanceLists(userFromDb);
            Assert.AreEqual(1, userFromDb.MyCategories.Count);
            Assert.AreEqual(1, userFromDb.MyAccounts.Count);
            Assert.AreEqual(0, userFromDb.MyGoals.Count);
            Assert.AreEqual(0, userFromDb.MyExchangesHistory.Count);
            Assert.AreEqual(1, userFromDb.MyAccounts[0].MyTransactions.Count);
        }
    }
}