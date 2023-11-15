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

        private TransactionDTO _transaction1;
        private TransactionDTO _transaction2;
        private MonetaryAccountDTO _exampleAccount;
        private CategoryDTO _exampleCategory;

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

            _exampleAccount = new MonetaryAccountDTO("Brou", 3000, CurrencyEnumDTO.USA, DateTime.Now, 1);
            _exampleCategory = new CategoryDTO("Food", StatusEnumDTO.Enabled, TypeEnumDTO.Outcome, 1);

            _transaction1 = new TransactionDTO("hola", DateTime.Now.Date, 200, CurrencyEnumDTO.USA, TypeEnumDTO.Outcome,
                _exampleCategory, 1);
            _transaction2 = new TransactionDTO("Nueva", new DateTime(2020, 05, 20), 500, CurrencyEnumDTO.USA, TypeEnumDTO.Outcome,
                _exampleCategory, 1);

            _controller.CreateCategory(_exampleCategory);
            _exampleCategory.CategoryId = 1;
            _controller.CreateMonetaryAccount(_exampleAccount);
            _exampleAccount.AccountId = 1;
            _controller.CreateTransaction(_transaction1);
            _transaction1.TransactionId = 1;
            _controller.CreateTransaction(_transaction2);
            _transaction1.TransactionId = 2;
        }

        #endregion

        #region Cleanup

        [TestCleanup]
        public void CleanUp()
        {
            _testDb.Database.EnsureDeleted();
        }

        #endregion

        [TestMethod]
        public void GivenAccountDTOListAndGoalDTOLisr_ShouldReturnMonthlyReportGoal()
        {
            List<CategoryDTO> categoriesOfGoal = new List<CategoryDTO>();

            categoriesOfGoal.Add(_controller.FindCategory(_exampleCategory.CategoryId, _userConnected.UserId));

            GoalDTO myGoalDTO = new GoalDTO("Eat Less Food", 500, CurrencyEnumDTO.UY, categoriesOfGoal, 1);

            _controller.CreateGoal(myGoalDTO);
            myGoalDTO.GoalId = 1;

            List<GoalDTO> myGoalDTOList = MapperGoal.ToListOfGoalDTO(_testDb.Users.First().MyGoals);

            //List<AccountDTO> myAccountsDTO = MapperAccount


            //List<ResumeOfGoalReportDTO> resumeOfGoalReports = _controller.GiveMonthlyReportPerGoal(_testDb.Users.First().MyAccounts, myGoalDTOList);

            //Assert.AreEqual()
            Assert.AreEqual(0, 1);
        }

        #region Filtering Lists

        [TestMethod]
        public void GivenListOfTransactionAndRangeOfDates_ShouldBeFiltered()
        {
            List<TransactionDTO> transactionUserListDTO = MapperTransaction.ToListOfTransactionsDTO(_testDb.Users.First().MyAccounts.First().MyTransactions);

            RangeOfDatesDTO myRangeDTO = new RangeOfDatesDTO(new DateTime(2023, 01, 01), new DateTime(2024, 01, 01));

            List<TransactionDTO> filteredListDTO = _controller.FilterListByRangeOfDate(transactionUserListDTO, myRangeDTO);

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

            List<TransactionDTO> filteredListDTO = _controller.FilterListByNameOfCategory(transactionUserListDTO, categoryName);

            Assert.AreEqual(filteredListDTO[0].Title, _transaction1.Title);
            Assert.AreEqual(filteredListDTO[1].Title, _transaction2.Title);
            Assert.AreEqual(filteredListDTO.Count, 2);

        }

        [TestMethod]
        public void GivenListAndMonetaryAccountDTOAndUserLoggedDTO_ShouldFilterListByAccount()
        {
            TransactionDTO transaction3 = new TransactionDTO("Losses", DateTime.Now.Date, 200, CurrencyEnumDTO.USA, TypeEnumDTO.Outcome, _exampleCategory, 2);

            CategoryDTO myCategory2 = new CategoryDTO("Sugars", StatusEnumDTO.Enabled, TypeEnumDTO.Income, 1);

            _controller.CreateCategory(myCategory2);
            myCategory2.CategoryId = 2;

            TransactionDTO transaction4 = new TransactionDTO("Wins", DateTime.Now.Date, 1000, CurrencyEnumDTO.USA, TypeEnumDTO.Income, myCategory2, 1);

            MonetaryAccount exampleAccount2 = new MonetaryAccount("Brou2", 3000, CurrencyEnum.USA, DateTime.Now);

            MonetaryAccountDTO exampleAccountDTO2 = MapperMonetaryAccount.ToMonetaryAccountDTO(exampleAccount2);

            _controller.CreateMonetaryAccount(exampleAccountDTO2);
            _controller.CreateTransaction(transaction3);
            _controller.CreateTransaction(transaction4);

            List<TransactionDTO> filteredListDTO = _controller.FilterByAccountAndTypeOutcome(_exampleAccount);

            Assert.AreEqual(filteredListDTO[0].Title, _transaction1.Title);
            Assert.AreEqual(filteredListDTO[1].Title, _transaction2.Title);
            Assert.AreEqual(filteredListDTO.Count, 2);
        }
        #endregion

        #region Monetary Account Balance

        [TestMethod]
        public void GivenMonetaryAccountDTO_ShouldReturnAccountBalance()
        {
            CategoryDTO myCategory2 = new CategoryDTO("Sugars", StatusEnumDTO.Enabled, TypeEnumDTO.Income, 1);

            _controller.CreateCategory(myCategory2);
            myCategory2.CategoryId = 2;

            TransactionDTO transaction4 = new TransactionDTO("Wins", DateTime.Now.Date, 1000, CurrencyEnumDTO.USA, TypeEnumDTO.Income, myCategory2, 1);

            _controller.CreateTransaction(transaction4);

            decimal balanceExpected = 2700;
            decimal accountBalance = _controller.GiveAccountBalance(_exampleAccount);

            Assert.AreEqual(balanceExpected, accountBalance);
        }

        #endregion
    }
}