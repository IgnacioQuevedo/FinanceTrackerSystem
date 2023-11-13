using BusinessLogic.Account_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.Transaction_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.User_Components;
using Controller;
using Controller.Mappers;
using DataManagers;

namespace ControllerTests
{
    [TestClass]
    public class ControllerCreditAccountTests
    {
        #region Initialize

        private GenericController _controller;
        private SqlContext _testDb;
        private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();

        private UserRepositorySql _userRepo;
        private UserDTO _userConnected;

        private CreditCardAccountDTO _creditToCreateDTO1;

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

            _creditToCreateDTO1 = new CreditCardAccountDTO("Prex", CurrencyEnumDTO.UY, DateTime.Now.Date, "Prex", "1233", 1900, new DateTime(2024, 12, 12), _userConnected.UserId);
            _creditToCreateDTO1.CreditCardAccountId = 1;
        }

        #endregion

        #region Cleanup

        [TestCleanup]
        public void CleanUp()
        {
            _testDb.Database.EnsureDeleted();
        }

        #endregion

        #region Create Credit Card Account

        [TestMethod]
        public void GivenCreditAccountDTOToCreate_ShouldBeCreated()
        {
            CreditCardAccountDTO creditToCreateDTO2 = new CreditCardAccountDTO("Itau Volar", CurrencyEnumDTO.EUR, DateTime.Now.Date, "Itau", "1235", 2000, new DateTime(2024, 12, 12), _userConnected.UserId);
            creditToCreateDTO2.CreditCardAccountId = 2;

            _controller.CreateCreditAccount(_creditToCreateDTO1);
            _controller.CreateCreditAccount(creditToCreateDTO2);

            List<Account> myAccountsDb = _testDb.Users.First().MyAccounts;

            Assert.IsNotNull(myAccountsDb[0].AccountUser);
            Assert.AreEqual(myAccountsDb[0].UserId, _creditToCreateDTO1.UserId);
            Assert.AreEqual(myAccountsDb[0].Name, _creditToCreateDTO1.Name);
            Assert.AreEqual(myAccountsDb[0].AccountId, _creditToCreateDTO1.CreditCardAccountId);
            Assert.AreEqual(myAccountsDb[0].Currency, (CurrencyEnum)_creditToCreateDTO1.Currency);
            Assert.AreEqual(typeof(CreditCardAccount), myAccountsDb[0].GetType());

            Assert.AreEqual(2, _testDb.Users.First().MyAccounts.Count);
        }

        #endregion

        #region Find Credit Account

        [TestMethod]
        public void GivenCreditAccountIdAndUserId_ShouldReturnCreditAccountDTOFound_OnDb()
        {
            _controller.CreateCreditAccount(_creditToCreateDTO1);

            CreditCardAccountDTO CreditAccountFound = _controller.FindCreditAccount(_creditToCreateDTO1.CreditCardAccountId, _userConnected.UserId);

            Assert.AreEqual(CreditAccountFound.CreditCardAccountId, _creditToCreateDTO1.CreditCardAccountId);
            Assert.AreEqual(CreditAccountFound.UserId, _creditToCreateDTO1.UserId);
        }

        [TestMethod]
        public void GivenCreditAccountDTO_ShouldReturnExactlyTheCreditAccountFound_OnDb()
        {
            _controller.CreateCreditAccount(_creditToCreateDTO1);

            CreditCardAccount creditAccountFound = _controller.FindCreditAccountInDb(_creditToCreateDTO1);

            Assert.AreEqual(creditAccountFound.AccountId, _creditToCreateDTO1.CreditCardAccountId);
            Assert.AreEqual(creditAccountFound.UserId, _creditToCreateDTO1.UserId);
        }

        #endregion

        [TestMethod]
        public void GivenCreditDTOToUpdate_ShouldBeUpdatedInDb()
        {
            _controller.CreateCreditAccount(_creditToCreateDTO1);

            CreditCardAccountDTO accountDTOWithUpdates = new CreditCardAccountDTO("Itau Volar", CurrencyEnumDTO.EUR, DateTime.Now.Date, "Itau", "1235", 2000, new DateTime(2024, 12, 12), _userConnected.UserId);
            accountDTOWithUpdates.CreditCardAccountId = 1;

            _controller.UpdateCreditAccount(accountDTOWithUpdates);

            CreditCardAccount accountInDbWithSupossedChanges = _controller.FindCreditAccountInDb(_creditToCreateDTO1);

            Assert.AreEqual(accountInDbWithSupossedChanges.AccountId, accountDTOWithUpdates.CreditCardAccountId);
            Assert.AreEqual(accountInDbWithSupossedChanges.Name, accountDTOWithUpdates.Name);
            Assert.AreEqual(accountInDbWithSupossedChanges.IssuingBank, accountDTOWithUpdates.IssuingBank);
            Assert.AreEqual(accountInDbWithSupossedChanges.Last4Digits, accountDTOWithUpdates.Last4Digits);
            Assert.AreEqual(accountInDbWithSupossedChanges.ClosingDate, accountDTOWithUpdates.ClosingDate);
            Assert.AreEqual(accountInDbWithSupossedChanges.AvailableCredit, accountDTOWithUpdates.AvailableCredit);
            Assert.AreEqual(accountInDbWithSupossedChanges.Currency, (CurrencyEnum)accountDTOWithUpdates.Currency);
            Assert.AreEqual(accountInDbWithSupossedChanges.UserId, accountDTOWithUpdates.UserId);
        }

    }
}