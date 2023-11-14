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

        private CategoryDTO categoryOfTransactionDTO;
        private MonetaryAccountDTO monetaryAccount;
        private TransactionDTO transactionDtoToAdd;


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

            categoryOfTransactionDTO =
                new CategoryDTO("Food", StatusEnumDTO.Enabled, TypeEnumDTO.Income, _userConnected.UserId);

            monetaryAccount = new MonetaryAccountDTO("Brou", 1000, CurrencyEnumDTO.UY,
                DateTime.Now, _userConnected.UserId);

            categoryOfTransactionDTO.CategoryId = 1;
            monetaryAccount.MonetaryAccountId = 1;

            _controller.CreateCategory(categoryOfTransactionDTO);
            _controller.CreateMonetaryAccount(monetaryAccount);

            transactionDtoToAdd = new TransactionDTO("Spent on food", DateTime.Now.Date, 100, CurrencyEnumDTO.UY,
                TypeEnumDTO.Income, categoryOfTransactionDTO, 1);

            _controller.CreateTransaction(transactionDtoToAdd);
            transactionDtoToAdd.TransactionId = 1;
        }

        #endregion

        #region Cleanup

        [TestCleanup]
        public void CleanUp()
        {
            _testDb.Database.EnsureDeleted();
        }

        #endregion


        #region Create Transaction

        [TestMethod]
        public void CreateMethodWithCorrectData_ShouldAddTransactionToDb()
        {
            TransactionDTO dtoToAdd2 = new TransactionDTO("Spent on party", DateTime.Now.Date, 500, CurrencyEnumDTO.UY,
                TypeEnumDTO.Income, categoryOfTransactionDTO, 1);

            _controller.CreateTransaction(dtoToAdd2);
            dtoToAdd2.TransactionId = 2;


            Transaction transactionInDb = _testDb.Users.First().MyAccounts.First().MyTransactions.First();
            Transaction transaction2InDb = _testDb.Users.First().MyAccounts.First().MyTransactions[1];

            Assert.AreEqual(dtoToAdd2.TransactionId, transaction2InDb.TransactionId);
            Assert.AreEqual((CurrencyEnum)dtoToAdd2.Currency, transaction2InDb.Currency);
            Assert.AreEqual(transactionDtoToAdd.TransactionId, transactionInDb.TransactionId);
            Assert.AreEqual(transactionDtoToAdd.Title, transactionInDb.Title);
            Assert.AreEqual(transactionDtoToAdd.Amount, transactionInDb.Amount);
            Assert.AreEqual(transactionDtoToAdd.CreationDate, transactionInDb.CreationDate);
            Assert.AreEqual((CurrencyEnum)transactionDtoToAdd.Currency, transactionInDb.Currency);
            Assert.AreEqual((TypeEnum)transactionDtoToAdd.Type, transactionInDb.Type);
            Assert.AreEqual(transactionDtoToAdd.AccountId, transactionInDb.AccountId);

            Assert.IsTrue(Helper.AreTheSameObject(transactionDtoToAdd.TransactionCategory,
                MapperCategory.ToCategoryDTO(transactionInDb.TransactionCategory)));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateMethodWithIncorrectData_ShouldAddTransactionToDb()
        {
            TransactionDTO transactionWithBadData = new TransactionDTO("", DateTime.Now.Date, 100, CurrencyEnumDTO.UY,
                TypeEnumDTO.Income, categoryOfTransactionDTO, 1);

            _controller.CreateTransaction(transactionWithBadData);
        }

        #endregion

        #region Find Methods

        [TestMethod]
        public void GivenTransactionDTO_ShouldBePossibleToFindTheTransactionOnDb()
        {
            TransactionDTO transactionDtoToFind = transactionDtoToAdd;

            Transaction transactionFound = _controller.FindTransactionInDb(transactionDtoToFind.TransactionId,
                monetaryAccount.MonetaryAccountId, _userConnected.UserId);

            Assert.AreEqual(transactionDtoToFind.TransactionId, transactionFound.TransactionId);
            Assert.AreEqual(transactionDtoToFind.Title, transactionFound.Title);
            Assert.AreEqual(transactionDtoToFind.CreationDate, transactionFound.CreationDate);
            Assert.AreEqual(transactionDtoToFind.Amount, transactionFound.Amount);
            Assert.AreEqual(transactionDtoToFind.AccountId, transactionFound.AccountId);
            Assert.AreEqual((CurrencyEnum)transactionDtoToFind.Currency, transactionFound.Currency);
            Assert.AreEqual((TypeEnum)transactionDtoToFind.Type, transactionFound.Type);

            Assert.AreEqual(_controller.FindCategoryInDb(categoryOfTransactionDTO),
                transactionFound.TransactionCategory);

            Assert.AreEqual(_controller.FindAccountByIdInDb(monetaryAccount.MonetaryAccountId),
                transactionFound.TransactionAccount);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GivenTransactionDTONotInDb_ShouldThrowException()
        {
            _controller.FindTransactionInDb(-1, monetaryAccount.MonetaryAccountId, _userConnected.UserId);
        }

        [TestMethod]
        public void GivenTransactionId_ShouldBePossibleToFindTransaction_AndReturnItDTO()
        {
            TransactionDTO transactionFound = _controller.FindTransaction(
                transactionDtoToAdd.TransactionId, monetaryAccount.MonetaryAccountId, _userConnected.UserId);

            Assert.AreEqual(transactionDtoToAdd.TransactionId, transactionFound.TransactionId);
            Assert.AreEqual(transactionDtoToAdd.Title, transactionFound.Title);
            Assert.AreEqual(transactionDtoToAdd.CreationDate, transactionFound.CreationDate);
            Assert.AreEqual(transactionDtoToAdd.Amount, transactionFound.Amount);
            Assert.AreEqual(transactionDtoToAdd.Currency, transactionFound.Currency);
            Assert.AreEqual(transactionDtoToAdd.Type, transactionFound.Type);
            Assert.AreEqual(transactionDtoToAdd.AccountId, transactionFound.AccountId);

            Assert.IsTrue(Helper.AreTheSameObject(transactionDtoToAdd.TransactionCategory,
                transactionFound.TransactionCategory));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GivenTransactionIdThatIsNotInDbToFind_ShouldThrowException()
        {
            _controller.FindTransaction(-1, monetaryAccount.UserId, monetaryAccount.MonetaryAccountId);
        }


        [TestMethod]
        public void GivenAnAccountId_ShouldBePossibleToFindHimInDb()
        {

            Account accountToFound = _controller.FindAccountByIdInDb(monetaryAccount.MonetaryAccountId);
            monetaryAccount.AccountId = 1;
            
            Assert.AreEqual(monetaryAccount.AccountId,accountToFound.AccountId);
            Assert.AreEqual(monetaryAccount.CreationDate,accountToFound.CreationDate);
            Assert.AreEqual((CurrencyEnum) monetaryAccount.Currency,accountToFound.Currency);
            Assert.AreEqual(monetaryAccount.Name,accountToFound.Name);
            Assert.AreEqual(monetaryAccount.UserId,accountToFound.UserId);
        }
        [TestMethod]
        public void GivenAnAccountId_ShouldBePossibleToFindIt()
        {

            AccountDTO accountDtoToFound = _controller.FindAccountById(monetaryAccount.MonetaryAccountId,_userConnected.UserId);
            
            Assert.AreEqual(monetaryAccount.AccountId,accountDtoToFound.AccountId);
            Assert.AreEqual(monetaryAccount.CreationDate,accountDtoToFound.CreationDate);
            Assert.AreEqual(monetaryAccount.Currency,accountDtoToFound.Currency);
            Assert.AreEqual(monetaryAccount.Name,accountDtoToFound.Name);
            Assert.AreEqual(monetaryAccount.UserId,accountDtoToFound.UserId);
        }
        

        #endregion

        #region Update Transaction

        [TestMethod]
        public void GivenTransactionDTOWithCorrectUpdates_ShouldUpdateItInDb()
        {
            CategoryDTO categoryDTO2 =
                new CategoryDTO("party", StatusEnumDTO.Enabled, TypeEnumDTO.Income, _userConnected.UserId);


            _controller.CreateCategory(categoryDTO2);
            categoryDTO2.CategoryId = 2;


            TransactionDTO transactionToUpd = new TransactionDTO("Spent on party", DateTime.Now.Date, 500,
                CurrencyEnumDTO.UY,
                TypeEnumDTO.Income, categoryDTO2, 1);
            _controller.CreateTransaction(transactionToUpd);
            transactionToUpd.TransactionId = 2;

            TransactionDTO transactionDtoWithUpdates = transactionDtoToAdd;
            transactionDtoWithUpdates.Currency = CurrencyEnumDTO.EUR;
            transactionDtoWithUpdates.Amount = 3333;
            transactionDtoWithUpdates.TransactionCategory = categoryDTO2;

            transactionDtoWithUpdates.TransactionId = 2;
            _controller.UpdateTransaction(transactionDtoWithUpdates, monetaryAccount.UserId);

            Account accountInDb = _controller.FindAccountByIdInDb(monetaryAccount.MonetaryAccountId);
            Transaction transactionUpdatedInDb = _controller.FindTransactionInDb(
                transactionDtoWithUpdates.TransactionId, monetaryAccount.MonetaryAccountId, _userConnected.UserId);

            Assert.AreEqual(transactionDtoWithUpdates.Amount, transactionUpdatedInDb.Amount);
            Assert.AreEqual((CurrencyEnum)transactionDtoWithUpdates.Currency, transactionUpdatedInDb.Currency);
            Assert.IsTrue(Helper.AreTheSameObject(
                _controller.FindCategoryInDb(transactionDtoWithUpdates.TransactionCategory),
                transactionUpdatedInDb.TransactionCategory));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GivenTransactionDTOWithIncorrectDataToUpdate_ShouldThrowException()
        {
            TransactionDTO transactionToUpd = new TransactionDTO("", DateTime.Now.Date, 500, CurrencyEnumDTO.UY,
                TypeEnumDTO.Income, categoryOfTransactionDTO, 1);
            transactionToUpd.TransactionId = 1;


            _controller.UpdateTransaction(transactionToUpd, _userConnected.UserId);
        }

        #endregion

        #region Delete Transaction

        [TestMethod]
        public void GivenTransactionDTOToDelete_ShouldDeleteItFromDb()
        {
            int amountOfTransactionsPreDelete = _testDb.Users.First().MyAccounts[0].MyTransactions.Count;

            _controller.DeleteTransaction(transactionDtoToAdd);

            int amountOfTransactionsPostDelete = _testDb.Users.First().MyAccounts[0].MyTransactions.Count;

            Assert.AreEqual(amountOfTransactionsPostDelete, amountOfTransactionsPreDelete - 1);
        }

        #endregion

        #region GetAllTransactions

        [TestMethod]
        public void GetAllTransactionsMethod_ShouldReturnTransactionFromAnAccount()
        {
            TransactionDTO transaction2 = new TransactionDTO("Spent on dancing", DateTime.Now.Date, 1000,
                CurrencyEnumDTO.UY, TypeEnumDTO.Income, categoryOfTransactionDTO, 1);
            _controller.CreateTransaction(transaction2);

            transaction2.TransactionId = 2;
            List<TransactionDTO> transactions =
                _controller.GetAllTransactions(monetaryAccount.MonetaryAccountId);

            Assert.AreEqual(2, transactions.Count);
            Assert.AreEqual(transactions[1].TransactionId, transaction2.TransactionId);
            Assert.AreEqual(transactions[1].Title, transaction2.Title);
            Assert.AreEqual(transactions[1].CreationDate, transaction2.CreationDate);
            Assert.AreEqual(transactions[1].Amount, transaction2.Amount);
            Assert.AreEqual(transactions[1].Currency, transaction2.Currency);
            Assert.AreEqual(transactions[1].Type, transaction2.Type);
            Assert.AreEqual(transactions[1].AccountId, transaction2.AccountId);
            Assert.IsTrue(
                Helper.AreTheSameObject(transactions[1].TransactionCategory, transaction2.TransactionCategory));
        }

        #endregion
    }
}