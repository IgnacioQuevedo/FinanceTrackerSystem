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
        private TransactionDTO dtoToAdd;

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
            categoryOfTransactionDTO.CategoryId = 1;
            
            monetaryAccount = new MonetaryAccountDTO("Brou", 1000, CurrencyEnumDTO.UY,
                DateTime.Now, _userConnected.UserId);
            monetaryAccount.MonetaryAccountId = 1;

            _controller.CreateCategory(categoryOfTransactionDTO);
            _controller.CreateMonetaryAccount(monetaryAccount);
            
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
            dtoToAdd = new TransactionDTO("Spent on food", DateTime.Now.Date, 100, CurrencyEnumDTO.UY,
                TypeEnumDTO.Income, categoryOfTransactionDTO, 1);

            TransactionDTO dtoToAdd2 = new TransactionDTO("Spent on party", DateTime.Now.Date, 500, CurrencyEnumDTO.UY,
                TypeEnumDTO.Income, categoryOfTransactionDTO, 1);

            _controller.CreateTransaction(dtoToAdd);
            _controller.CreateTransaction(dtoToAdd2);

            dtoToAdd.TransactionId = 1;
            dtoToAdd2.TransactionId = 2;


            Transaction transactionInDb = _testDb.Users.First().MyAccounts.First().MyTransactions.First();
            Transaction transaction2InDb = _testDb.Users.First().MyAccounts.First().MyTransactions[1];
            
            Assert.AreEqual(dtoToAdd2.TransactionId, transaction2InDb.TransactionId);
            Assert.AreEqual((CurrencyEnum)dtoToAdd2.Currency, transaction2InDb.Currency);

            Assert.AreEqual(dtoToAdd.TransactionId, transactionInDb.TransactionId);
            Assert.AreEqual(dtoToAdd.Title, transactionInDb.Title);
            Assert.AreEqual(dtoToAdd.Amount, transactionInDb.Amount);
            Assert.AreEqual(dtoToAdd.CreationDate, transactionInDb.CreationDate);
            Assert.AreEqual((CurrencyEnum)dtoToAdd.Currency, transactionInDb.Currency);
            Assert.AreEqual((TypeEnum)dtoToAdd.Type, transactionInDb.Type);
            Assert.IsTrue(Helper.AreTheSameObject(dtoToAdd.TransactionCategory,
                MapperCategory.ToCategoryDTO(transactionInDb.TransactionCategory)));
            Assert.AreEqual(dtoToAdd.AccountId, transactionInDb.AccountId);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateMethodWithIncorrectData_ShouldAddTransactionToDb()
        {
            dtoToAdd = new TransactionDTO("", DateTime.Now.Date, 100, CurrencyEnumDTO.UY,
                TypeEnumDTO.Income, categoryOfTransactionDTO, 1);

            _controller.CreateTransaction(dtoToAdd);
        }

        #endregion

        #region Find Methods

        [TestMethod]
        public void GivenTransactionDTO_ShouldBePossibleToFindItOnDb()
        {
            TransactionDTO transactionToFind = new TransactionDTO("Spent on food", DateTime.Now.Date, 100,
                CurrencyEnumDTO.UY,
                TypeEnumDTO.Income, categoryOfTransactionDTO, 1);
            transactionToFind.TransactionId = 1;

            _controller.CreateTransaction(transactionToFind);

            Transaction transactionFound = _controller.FindTransactionInDb(transactionToFind.TransactionId,
                monetaryAccount.MonetaryAccountId, _userConnected.UserId);

            Assert.AreEqual(transactionToFind.TransactionId, transactionFound.TransactionId);
            Assert.AreEqual(transactionToFind.Title, transactionFound.Title);
            Assert.AreEqual(transactionToFind.CreationDate, transactionFound.CreationDate);
            Assert.AreEqual(transactionToFind.Amount, transactionFound.Amount);
            Assert.AreEqual(transactionToFind.Amount, transactionFound.Amount);
            Assert.AreEqual(transactionToFind.Amount, transactionFound.Amount);
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
            TransactionDTO transactionToCreate = new TransactionDTO("Spent on food", DateTime.Now.Date, 100,
                CurrencyEnumDTO.UY,
                TypeEnumDTO.Income, categoryOfTransactionDTO, 1);
            
            _controller.CreateTransaction(transactionToCreate);
            transactionToCreate.TransactionId = 1;

            TransactionDTO transactionFound = _controller.FindTransaction(
                transactionToCreate.TransactionId,monetaryAccount.MonetaryAccountId,_userConnected.UserId);
            
            Assert.AreEqual(transactionToCreate.TransactionId,transactionFound.TransactionId);
            Assert.AreEqual(transactionToCreate.Title,transactionFound.Title);
            Assert.AreEqual(transactionToCreate.CreationDate,transactionFound.CreationDate);
            Assert.AreEqual(transactionToCreate.Amount,transactionFound.Amount);
            Assert.AreEqual(transactionToCreate.Currency,transactionFound.Currency);
            Assert.AreEqual(transactionToCreate.Type,transactionFound.Type);
            Assert.AreEqual(transactionToCreate.AccountId,transactionFound.AccountId);
            
            Assert.IsTrue(Helper.AreTheSameObject(transactionToCreate.TransactionCategory,transactionFound.TransactionCategory));
            
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GivenTransactionIdThatIsNotInDbToFind_ShouldThrowException()
        {
            _controller.FindTransaction(-1, monetaryAccount.UserId,monetaryAccount.MonetaryAccountId);
        }
        
        
        
        #endregion
    }
}