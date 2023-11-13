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

        private CategoryDTO categoryDTO;

        [TestInitialize]
        public void Initialize()
        {
            _testDb = _contextFactory.CreateDbContext();
            _userRepo = new UserRepositorySql(_testDb);
            _controller = new GenericController(_userRepo);

            _userConnected = new UserDTO("Jhon", "Sans", "jhonnie@gmail.com", "Jhoooniee123!", "");
            _userConnected.UserId = 1;

            categoryDTO = new CategoryDTO("Food", StatusEnumDTO.Enabled, TypeEnumDTO.Income, 1);
            categoryDTO.CategoryId = 1;

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

        #region Filtering Lists

        [TestMethod]
        public void GivenListOfTransactionAndRangeOfDates_ShouldBeFiltered()
        {
            Account exampleAccount = new MonetaryAccount("Brou", 3000, CurrencyEnum.USA, DateTime.Now);
            Category exampleCategory = new Category("Hola", StatusEnum.Enabled, TypeEnum.Income);

            Transaction transaction1 = new Transaction("hola", 200, DateTime.Now.Date, CurrencyEnum.USA, TypeEnum.Income,
                exampleCategory);
            Transaction transaction2 = new Transaction("Nueva", 500, new DateTime(2020, 05, 20), CurrencyEnum.USA, TypeEnum.Income,
                exampleCategory);

            exampleCategory.CategoryId = 0;
            exampleAccount.AccountId = 0;
            transaction1.TransactionId = 0;
            transaction2.TransactionId = 0;

            _testDb.Users.First().MyCategories.Add(exampleCategory);
            _testDb.Users.First().MyAccounts.Add(exampleAccount);
            _testDb.Users.First().MyAccounts.First().MyTransactions.Add(transaction1);
            _testDb.Users.First().MyAccounts.First().MyTransactions.Add(transaction2);

            List<TransactionDTO> transactionUserListDTO = MapperTransaction.ToListOfTransactionsDTO(_testDb.Users.First().MyAccounts.First().MyTransactions);

            _testDb.SaveChanges();

            RangeOfDatesDTO myRangeDTO = new RangeOfDatesDTO(new DateTime(2023, 01, 01), new DateTime(2024, 01, 01));

            List<TransactionDTO> filteredListDTO = _controller.FilterListOfSpendingsByRangeOfDate(transactionUserListDTO, myRangeDTO);

            Assert.AreEqual(filteredListDTO[0].Title, transaction1.Title);
            Assert.AreEqual(filteredListDTO.Count, 1);
        }

        #endregion
    }
}