using BusinessLogic.Account_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.Transaction_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.Report_Components;
using BusinessLogic.User_Components;
using Controller;
using Controller.Mappers;
using DataManagers;
using BusinessLogic.Report_Components;
using BusinessLogic.Exceptions;

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

            _transaction1 = new TransactionDTO("hola", new DateTime(2023, 11, 15), 200, CurrencyEnumDTO.USA, TypeEnumDTO.Outcome,
                _exampleCategory, 1);
            _transaction2 = new TransactionDTO("Nueva", new DateTime(2020, 05, 20), 500, CurrencyEnumDTO.USA,
                TypeEnumDTO.Outcome,
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

        #region Monthly Report

        [TestMethod]
        public void GivenUserDTO_ShouldReturnMonthlyReportGoal()
        {
            List<CategoryDTO> categoriesOfGoal = new List<CategoryDTO>();

            categoriesOfGoal.Add(_controller.FindCategory(_exampleCategory.CategoryId, _userConnected.UserId));

            GoalDTO myGoalDTO = new GoalDTO("Eat Less Food", 500, CurrencyEnumDTO.UY, categoriesOfGoal, 1);

            TransactionDTO myTransaction = new TransactionDTO("Spend2", DateTime.Now, 100, CurrencyEnumDTO.EUR, TypeEnumDTO.Outcome, categoriesOfGoal[0], 1);

            ExchangeHistoryDTO myExchange = new ExchangeHistoryDTO(CurrencyEnumDTO.USA, 10, DateTime.Now, 1);

            ExchangeHistoryDTO myExchange2 = new ExchangeHistoryDTO(CurrencyEnumDTO.EUR, 20, DateTime.Now, 1);

            _controller.CreateExchangeHistory(myExchange);
            myExchange.ExchangeHistoryId = 1;

            _controller.CreateExchangeHistory(myExchange2);
            myExchange2.ExchangeHistoryId = 1;

            _controller.CreateTransaction(myTransaction);
            myTransaction.TransactionId = 3;

            _controller.CreateGoal(myGoalDTO);
            myGoalDTO.GoalId = 1;

            List<ResumeOfGoalReportDTO> resumeOfGoalReportsDTO = _controller.GiveMonthlyReportPerGoal(_userConnected);

            Assert.AreEqual(resumeOfGoalReportsDTO[0].TotalSpent, 4000);
            Assert.AreEqual(resumeOfGoalReportsDTO[0].AmountDefined, 500);
            Assert.AreEqual(resumeOfGoalReportsDTO[0].GoalAchieved, false);
            Assert.AreEqual(resumeOfGoalReportsDTO.Count, 1);
        }

        #endregion

        #region Spendings Report Per Category Detailed

        [TestMethod]
        public void GivenUserDTOAndMonth_ShouldReturnReportOfCategorySpendingsDTO()
        {
            ExchangeHistoryDTO myExchange = new ExchangeHistoryDTO(CurrencyEnumDTO.USA, 10, new DateTime(2023, 11, 15), 1);

            _controller.CreateExchangeHistory(myExchange);
            myExchange.ExchangeHistoryId = 1;

            List<ResumeOfCategoryReportDTO> myResumeList = _controller.GiveAllSpendingsPerCategoryDetailed(_userConnected, MonthsEnumDTO.November);

            Assert.AreEqual(2000, myResumeList[0].TotalSpentInCategory);
            Assert.AreEqual(100, myResumeList[0].PercentajeOfTotal);
            Assert.AreEqual(_exampleCategory.Name, myResumeList[0].CategoryRelated.Name);
        }

        #endregion

        #region Give All Outcome Transaction

        [TestMethod]
        public void GivenUser_ShoulReturnListOfAllOutComeTransactionsDTO()
        {
            List<TransactionDTO> allOutcomeTransactions = _controller.GiveAllOutcomeTransactions(_userConnected);

            Assert.AreEqual(2, allOutcomeTransactions.Count);
        }
        #endregion

        #region Report Of Spendings Per Card

        [TestMethod]
        public void GivenUser_ShouldReturnSpendingsPerCard()
        {
            CreditCardAccountDTO myCreditCard = new CreditCardAccountDTO("Itau Credits", CurrencyEnumDTO.UY, new DateTime(2023, 11, 15), "Itau", "1122", 2000, new DateTime(2023, 11, 16), 1);

            _controller.CreateCreditAccount(myCreditCard);
            myCreditCard.AccountId = 2;

            TransactionDTO transactionDTO1 = new TransactionDTO("Spend1", new DateTime(2023, 11, 15), 1000, CurrencyEnumDTO.EUR, TypeEnumDTO.Outcome, _exampleCategory, 2);

            TransactionDTO transactionDTO2 = new TransactionDTO("Spend2", new DateTime(2023, 11, 15), 2000, CurrencyEnumDTO.USA, TypeEnumDTO.Outcome, _exampleCategory, 2);

            _controller.CreateTransaction(transactionDTO1);
            transactionDTO1.TransactionId = 3;
            _controller.CreateTransaction(transactionDTO2);
            transactionDTO2.TransactionId = 4;

            List<TransactionDTO> reportPerCard = _controller.ReportOfSpendingsPerCard(myCreditCard);

            Assert.AreEqual(2, reportPerCard.Count);
        }

        #endregion

        #region Filtering Lists

        [TestMethod]
        public void GivenListOfTransactionAndRangeOfDates_ShouldBeFiltered()
        {
            List<TransactionDTO> transactionUserListDTO =
                MapperTransaction.ToListOfTransactionsDTO(_testDb.Users.First().MyAccounts.First().MyTransactions);

            RangeOfDatesDTO myRangeDTO = new RangeOfDatesDTO(new DateTime(2023, 01, 01), new DateTime(2024, 01, 01));

            List<TransactionDTO> filteredListDTO =
                _controller.FilterListByRangeOfDate(transactionUserListDTO, myRangeDTO);

            Assert.AreEqual(filteredListDTO[0].Title, _transaction1.Title);
            Assert.AreEqual(filteredListDTO.Count, 1);
        }


        [TestMethod]
        public void GivenListOfTransactionAndCategoryName_ShouldBeFiltered()
        {
            Category unWantedCategory = new Category("Party", StatusEnum.Enabled, TypeEnum.Outcome);

            string categoryName = "Food";

            Transaction transaction3 = new Transaction("Losses", 200, DateTime.Now.Date, CurrencyEnum.USA, TypeEnum.Outcome, unWantedCategory);

            _testDb.Users.First().MyAccounts.First().MyTransactions.Add(transaction3);
            _testDb.SaveChanges();

            List<TransactionDTO> transactionUserListDTO =
                MapperTransaction.ToListOfTransactionsDTO(_testDb.Users.First().MyAccounts.First().MyTransactions);

            List<TransactionDTO> filteredListDTO =
                _controller.FilterListByNameOfCategory(transactionUserListDTO, categoryName);

            Assert.AreEqual(filteredListDTO[0].Title, _transaction1.Title);
            Assert.AreEqual(filteredListDTO[1].Title, _transaction2.Title);
            Assert.AreEqual(filteredListDTO.Count, 2);
        }

        [TestMethod]
        public void GivenMonetaryAccountDTO_ShouldFilterListByAccountAndType()
        {
            TransactionDTO transaction3 = new TransactionDTO("Losses", DateTime.Now.Date, 200, CurrencyEnumDTO.USA,
                TypeEnumDTO.Outcome, _exampleCategory, 2);

            CategoryDTO myCategory2 = new CategoryDTO("Sugars", StatusEnumDTO.Enabled, TypeEnumDTO.Income, 1);

            _controller.CreateCategory(myCategory2);
            myCategory2.CategoryId = 2;

            TransactionDTO transaction4 = new TransactionDTO("Wins", DateTime.Now.Date, 1000, CurrencyEnumDTO.USA,
                TypeEnumDTO.Income, myCategory2, 1);

            MonetaryAccount exampleAccount2 = new MonetaryAccount("Brou2", 3000, CurrencyEnum.USA, DateTime.Now);
            exampleAccount2.UserId = 1;

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
          
        #region Get Movement In X Days

        [TestMethod]
        public void GivenRangeOfDatesOfOneMonth_ShouldBePossibleToCalculateMovementsOfTransactionsPerDay()
        {
            RangeOfDatesDTO rangeOfDatesDTO = new RangeOfDatesDTO(new DateTime(2020, 5, 20).Date,
                new DateTime(2020, 5, 24));

            TransactionDTO _transaction3 = new TransactionDTO("Party", new DateTime(2020, 05, 23),
                1000, CurrencyEnumDTO.USA, TypeEnumDTO.Outcome, _exampleCategory, 1);

            _controller.CreateTransaction(_transaction3);

            MovementInXDaysDTO movements = _controller.GetMovementsOfTransactionsInXDays(1, rangeOfDatesDTO,MonthsEnumDTO.May);

            Assert.AreEqual(500, movements.Spendings[19]);
            Assert.AreEqual(1000, movements.Spendings[22]);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GivenMonthSelectDifferentOfTheMonthOfTheDates_ShouldThrowException()
        {
            MonthsEnumDTO monthsEnumDto = MonthsEnumDTO.April;
            
            RangeOfDatesDTO rangeOfDatesDTO = new RangeOfDatesDTO(new DateTime(2020, 5, 20).Date,
                new DateTime(2020, 5, 24));

            TransactionDTO _transaction3 = new TransactionDTO("Party", new DateTime(2020, 05, 23),
                1000, CurrencyEnumDTO.USA, TypeEnumDTO.Outcome, _exampleCategory, 1);

            _controller.CreateTransaction(_transaction3);

            MovementInXDaysDTO movements = _controller.GetMovementsOfTransactionsInXDays(1, rangeOfDatesDTO,monthsEnumDto);


        }

        #endregion
    }
}