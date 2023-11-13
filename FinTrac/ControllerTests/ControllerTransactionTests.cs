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
    public class ControllerTransactionTests
    {
        #region Initialize

        private GenericController _controller;
        private SqlContext _testDb;
        private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();

        private UserRepositorySql _userRepo;
        private UserDTO _userConnected;

        private CategoryDTO categoryDTO;

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
        }

        #endregion

        #region Cleanup

        [TestCleanup]
        public void CleanUp()
        {
            _testDb.Database.EnsureDeleted();
        }

        #endregion

        public void CreateMethodWithCorrectData_ShouldAddTransactionToDb()
        {
            CategoryDTO categoryOfTransactionDTO =
                new CategoryDTO("Food", StatusEnumDTO.Enabled, TypeEnumDTO.Income, _userConnected.UserId);

            MonetaryAccountDTO monetaryAccount = new MonetaryAccountDTO("Brou", 1000, CurrencyEnumDTO.UY, 
                DateTime.Now,_userConnected.UserId);
            
            monetaryAccount.MonetaryAccountId = 1;
            
            TransactionDTO dtoToAdd = new TransactionDTO("Spent on food", DateTime.Now.Date, 100, CurrencyEnumDTO.UY,
                TypeEnumDTO.Income, categoryOfTransactionDTO, 1);

            _controller.CreateMonetaryAccount(monetaryAccount);
            
            dtoToAdd.TransactionId = 0;
            dtoToAdd.AccountId = monetaryAccount.MonetaryAccountId;


            _controller.CreateTransaction(dtoToAdd);

            Transaction transactionInDb = _testDb.Users.First().MyAccounts.First().MyTransactions.First();

            Assert.AreEqual(dtoToAdd.Title, transactionInDb.Title);
            Assert.AreEqual(dtoToAdd.Amount, transactionInDb.Amount);
            Assert.AreEqual(dtoToAdd.CreationDate, transactionInDb.CreationDate);
            Assert.AreEqual(dtoToAdd.Currency, transactionInDb.Currency);
            Assert.AreEqual(dtoToAdd.Type, transactionInDb.Type);
            Assert.AreEqual(dtoToAdd.TransactionCategory, transactionInDb.TransactionCategory);
            Assert.AreEqual(dtoToAdd.AccountId, transactionInDb.AccountId);
        }
    }
}