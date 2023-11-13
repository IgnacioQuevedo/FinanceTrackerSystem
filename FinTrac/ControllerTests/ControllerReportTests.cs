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
    public class ControllerReportTests
    {
        #region Initialize

        private GenericController _controller;
        private SqlContext _testDb;
        private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();

        private UserRepositorySql _userRepo;
        private UserDTO _userConnected;

        private Transaction _transaction1;
        private Transaction _transaction2;
        private MonetaryAccount _exampleAccount;
        private Category _exampleCategory;

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

            _exampleAccount = new MonetaryAccount("Brou", 3000, CurrencyEnum.USA, DateTime.Now);
            _exampleCategory = new Category("Food", StatusEnum.Enabled, TypeEnum.Income);

            _transaction1 = new Transaction("hola", 200, DateTime.Now.Date, CurrencyEnum.USA, TypeEnum.Income,
                _exampleCategory);
            _transaction2 = new Transaction("Nueva", 500, new DateTime(2020, 05, 20), CurrencyEnum.USA, TypeEnum.Income,
                _exampleCategory);

            _exampleCategory.CategoryId = 0;
            _exampleAccount.AccountId = 0;
            _transaction1.TransactionId = 0;
            _transaction2.TransactionId = 0;

            _testDb.Users.First().MyCategories.Add(_exampleCategory);
            _testDb.Users.First().MyAccounts.Add(_exampleAccount);
            _testDb.Users.First().MyAccounts.First().MyTransactions.Add(_transaction1);
            _testDb.Users.First().MyAccounts.First().MyTransactions.Add(_transaction2);

            _testDb.SaveChanges();
        }

        #endregion

        #region Cleanup

        [TestCleanup]
        public void CleanUp()
        {
            _testDb.Database.EnsureDeleted();
        }

        #endregion

        #region Filtering Lists

        [TestMethod]
        public void GivenListOfTransactionAndRangeOfDates_ShouldBeFiltered()
        {
            List<TransactionDTO> transactionUserListDTO = MapperTransaction.ToListOfTransactionsDTO(_testDb.Users.First().MyAccounts.First().MyTransactions);

            RangeOfDatesDTO myRangeDTO = new RangeOfDatesDTO(new DateTime(2023, 01, 01), new DateTime(2024, 01, 01));

            List<TransactionDTO> filteredListDTO = _controller.FilterListOfSpendingsByRangeOfDate(transactionUserListDTO, myRangeDTO);

            Assert.AreEqual(filteredListDTO[0].Title, _transaction1.Title);
            Assert.AreEqual(filteredListDTO.Count, 1);
        }


        [TestMethod]
        public void GivenListOfTransactionAndCategoryName_ShouldBeFiltered()
        {
            Category unWantedCategory = new Category("Party", StatusEnum.Enabled, TypeEnum.Outcome);

            string categoryName = "Food";

            Transaction transaction3 = new Transaction("Losses", 200, DateTime.Now.Date, CurrencyEnum.USA, TypeEnum.Outcome,
                unWantedCategory);

            _testDb.Users.First().MyAccounts.First().MyTransactions.Add(transaction3);
            _testDb.SaveChanges();

            List<TransactionDTO> transactionUserListDTO = MapperTransaction.ToListOfTransactionsDTO(_testDb.Users.First().MyAccounts.First().MyTransactions);

            List<TransactionDTO> filteredListDTO = _controller.FilterListOfSpendingsByNameOfCategory(transactionUserListDTO, categoryName);

            Assert.AreEqual(filteredListDTO[0].Title, _transaction1.Title);
            Assert.AreEqual(filteredListDTO[1].Title, _transaction2.Title);
            Assert.AreEqual(filteredListDTO.Count, 2);



        }

        #endregion
    }
}