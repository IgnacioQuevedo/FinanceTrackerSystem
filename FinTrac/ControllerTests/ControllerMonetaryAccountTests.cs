using BusinessLogic.Account_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.Transaction_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.User_Components;
using Controller;
using Controller.Mappers;
using DataManagers;
using System.Xml.Schema;

namespace ControllerTests
{
    [TestClass]
    public class ControllerMonetaryAccountsTests
    {
        #region Initialize

        private GenericController _controller;
        private SqlContext _testDb;
        private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();

        private UserRepositorySql _userRepo;
        private UserDTO _userConnected;

        private MonetaryAccountDTO _monetToCreateDTO1;
        private MonetaryAccountDTO _monetToCreateDTO2;

        [TestInitialize]
        public void Initialize()
        {
            _testDb = _contextFactory.CreateDbContext();
            _userRepo = new UserRepositorySql(_testDb);
            _controller = new GenericController(_userRepo);

            _userConnected = new UserDTO("Jhon", "Sans", "jhonnie@gmail.com", "Jhoooniee123!", "");
            _userConnected.UserId = 1;

            _controller.RegisterUser(_userConnected);
            _controller.SetUserConnected(_userConnected.UserId);

            _monetToCreateDTO1 = new MonetaryAccountDTO("Brou", 1000, CurrencyEnumDTO.UY, DateTime.Now.Date,
                _userConnected.UserId);
            _monetToCreateDTO1.AccountId = 1;

            _monetToCreateDTO2 = new MonetaryAccountDTO("Itau", 1300, CurrencyEnumDTO.USA, DateTime.Now.Date,
                _userConnected.UserId);
        }

        #endregion

        #region Cleanup

        [TestCleanup]
        public void CleanUp()
        {
            _testDb.Database.EnsureDeleted();
        }

        #endregion

        #region Create Monetary Account

        [TestMethod]
        public void GivenMonetaryAccountToCreate_ShouldBeCreated()
        {
            _monetToCreateDTO1 = new MonetaryAccountDTO("Brou", 1000, CurrencyEnumDTO.UY, DateTime.Now.Date,
                _userConnected.UserId);
            _monetToCreateDTO1.AccountId = 1;
            _monetToCreateDTO2.AccountId = 2;

            _controller.CreateMonetaryAccount(_monetToCreateDTO1);
            _controller.CreateMonetaryAccount(_monetToCreateDTO2);

            List<Account> myAccountsDb = _testDb.Users.First().MyAccounts;

            Assert.IsNotNull(myAccountsDb[0].AccountUser);
            Assert.AreEqual(myAccountsDb[0].UserId, _monetToCreateDTO1.UserId);
            Assert.AreEqual(myAccountsDb[0].Name, _monetToCreateDTO1.Name);
            Assert.AreEqual(myAccountsDb[0].AccountId, _monetToCreateDTO1.AccountId);
            Assert.AreEqual(myAccountsDb[0].Currency, (CurrencyEnum)_monetToCreateDTO1.Currency);
            Assert.AreEqual(typeof(MonetaryAccount), myAccountsDb[0].GetType());
            Assert.IsInstanceOfType(myAccountsDb[0], typeof(MonetaryAccount));

            Assert.AreEqual(2, _testDb.Users.First().MyAccounts.Count);
        }

        #endregion

        #region Find Monetary Account

        [TestMethod]
        public void GivenMonetaryAccountIdAndUserId_ShouldReturnMonetaryAccountDTOFound_OnDb()
        {
            _controller.CreateMonetaryAccount(_monetToCreateDTO1);

            MonetaryAccountDTO monetAccountFound =
                _controller.FindMonetaryAccount(_monetToCreateDTO1.AccountId, _userConnected.UserId);

            Assert.AreEqual(_monetToCreateDTO1.AccountId, monetAccountFound.AccountId);
            Assert.AreEqual(_monetToCreateDTO1.UserId, monetAccountFound.UserId);
        }

        [TestMethod]
        public void GivenMonetaryAccountDTO_ShouldReturnExactlyTheMonetaryAccountFound_OnDb()
        {
            _controller.CreateMonetaryAccount(_monetToCreateDTO1);

            MonetaryAccount monetFound = _controller.FindMonetaryAccountInDb(_monetToCreateDTO1);

            Assert.AreEqual(_monetToCreateDTO1.AccountId, monetFound.AccountId);
            Assert.AreEqual(_monetToCreateDTO1.UserId, monetFound.UserId);
        }

        #endregion

        #region Update Monetary Account

        [TestMethod]
        public void GivenMonetaryDTOToUpdate_ShouldBeUpdatedInDb()
        {
            _controller.CreateMonetaryAccount(_monetToCreateDTO1);

            MonetaryAccountDTO accountDTOWithUpdates = _monetToCreateDTO2;
            accountDTOWithUpdates.AccountId = 1;

            _controller.UpdateMonetaryAccount(accountDTOWithUpdates);

            MonetaryAccount accountInDbWithSupossedChanges = _controller.FindMonetaryAccountInDb(_monetToCreateDTO1);

            Assert.AreEqual(accountInDbWithSupossedChanges.AccountId, accountDTOWithUpdates.AccountId);
            Assert.AreEqual(accountInDbWithSupossedChanges.Name, accountDTOWithUpdates.Name);
            Assert.AreEqual(accountInDbWithSupossedChanges.Amount, accountDTOWithUpdates.Amount);
            Assert.AreEqual(accountInDbWithSupossedChanges.Currency, (CurrencyEnum)accountDTOWithUpdates.Currency);
            Assert.AreEqual(accountInDbWithSupossedChanges.UserId, accountDTOWithUpdates.UserId);
        }

        #endregion

        #region Delete Monetary Account

        [TestMethod]
        public void GivenAMonetaryAccountDTOToDelete_ShouldBeDeletedOnDb()
        {
            _controller.CreateMonetaryAccount(_monetToCreateDTO1);
            _controller.CreateMonetaryAccount(_monetToCreateDTO2);

            int amountOfMonetaryAccountsPreDelete = _testDb.Users.First().MyAccounts.Count;
            _monetToCreateDTO1.AccountId = 1;
            _monetToCreateDTO2.AccountId = 2;
            
            _controller.DeleteMonetaryAccount(_monetToCreateDTO1);
            _controller.DeleteMonetaryAccount(_monetToCreateDTO2);
            
            int amountOfMonetaryAccountsPostDelete = _testDb.Users.First().MyAccounts.Count;

            Assert.AreEqual(amountOfMonetaryAccountsPostDelete,amountOfMonetaryAccountsPreDelete - 2);
            #endregion
        }

        #region Get All Monetary Accounts

        [TestMethod]
        public void GivenUserId_ShouldReturnListOfMonetaryAccountDTO()
        {
            _controller.CreateMonetaryAccount(_monetToCreateDTO1);
            _controller.CreateMonetaryAccount(_monetToCreateDTO2);

            CreditCardAccountDTO creditAccountDTO1 = new CreditCardAccountDTO("Prex", CurrencyEnumDTO.UY,
                DateTime.Now.Date, "Prex", "1233", 1900, new DateTime(2024, 12, 12), _userConnected.UserId);

            _controller.CreateCreditAccount(creditAccountDTO1);

            int previousLength = _testDb.Users.FirstOrDefault().MyAccounts.Count;

            List<MonetaryAccountDTO> listOfMonetaryAccounts =
                _controller.GetAllMonetaryAccounts(_userConnected.UserId);

            int lengthOfListReturned = listOfMonetaryAccounts.Count;

            Assert.AreEqual(previousLength - 1, lengthOfListReturned);
        }

        #endregion
    }
}