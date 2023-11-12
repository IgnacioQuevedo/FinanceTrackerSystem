using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.Transaction_Components;
using Controller;
using Controller.Mappers;
using DataManagers;
using Mappers;

namespace ControllerTests
{
    [TestClass]
    public class MapperTransactionTests
    {
        #region Initialize

        private GenericController _controller;
        private SqlContext _testDb;
        private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();

        private UserRepositorySql _userRepo;


        [TestInitialize]
        public void Initialize()
        {
            _testDb = _contextFactory.CreateDbContext();
            _userRepo = new UserRepositorySql(_testDb);
            _controller = new GenericController(_userRepo);
        }

        #endregion

        #region Cleanup

        [TestCleanup]
        public void CleanUp()
        {
            _testDb.Database.EnsureDeleted();
        }

        #endregion

        #region To Transaction

        [TestMethod]
        public void GivenTransactionDTOWithCorrectData_ShouldBePossibleToConvertItToTransaction()
        {
            CategoryDTO exampleCategory = new CategoryDTO("Food", StatusEnumDTO.Enabled, TypeEnumDTO.Income, 1);

            TransactionDTO transactionDTO = new TransactionDTO("Spent on food", DateTime.Now.Date, 200,
                CurrencyEnumDTO.USA, TypeEnumDTO.Income, exampleCategory, 1);
            transactionDTO.TransactionId = 1;

            Transaction transactionGenerated = MapperTransaction.ToTransaction(transactionDTO);

            Assert.AreEqual(transactionGenerated.TransactionId, transactionDTO.TransactionId);
            Assert.AreEqual(transactionGenerated.CreationDate, transactionDTO.CreationDate);
            Assert.AreEqual(transactionGenerated.Amount, transactionDTO.Amount);
            Assert.AreEqual(transactionGenerated.Currency, (CurrencyEnum)transactionDTO.Currency);
            Assert.AreEqual(transactionGenerated.Type, (TypeEnum)transactionDTO.Type);
            Assert.AreEqual(transactionGenerated.AccountId, transactionDTO.AccountId);
            Assert.IsInstanceOfType(transactionGenerated.TransactionCategory, typeof(Category));
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionMapper))]
        public void GivenTransactionDTOWithIncorrectData_ShouldThrowException()
        {
            CategoryDTO exampleCategory = new CategoryDTO("Food", StatusEnumDTO.Enabled, TypeEnumDTO.Income, 1);

            TransactionDTO transactionDTO = new TransactionDTO("", DateTime.Now.Date, -200,
                CurrencyEnumDTO.USA, TypeEnumDTO.Income, exampleCategory, 1);

            MapperTransaction.ToTransaction(transactionDTO);
        }

        #endregion

        #region To TransactionDTO

        [TestMethod]
        public void GivenTransaction_ShouldBePossibleToConvertToTransactionDTO()
        {
            Category exampleCategory = new Category("Food", StatusEnum.Enabled, TypeEnum.Income);
            exampleCategory.CategoryId = 1;
            
            Transaction transactionToConvert = new Transaction("Spent on food", 200, DateTime.Now.Date,
                CurrencyEnum.USA, TypeEnum.Income, exampleCategory);
            transactionToConvert.TransactionId = 1;

            TransactionDTO transactionDTO = MapperTransaction.ToTransactionDTO(transactionToConvert);
            
            Assert.IsInstanceOfType(transactionDTO, typeof(TransactionDTO));
            Assert.AreEqual(transactionDTO.TransactionId, transactionToConvert.TransactionId);
            Assert.AreEqual(transactionDTO.Title, transactionToConvert.Title);
            Assert.AreEqual(transactionDTO.Amount, transactionToConvert.Amount);
            Assert.AreEqual(transactionDTO.CreationDate, transactionToConvert.CreationDate);
            Assert.AreEqual((CurrencyEnum)transactionDTO.Currency, transactionToConvert.Currency);
            Assert.AreEqual((TypeEnum)transactionDTO.Type, transactionToConvert.Type);
            Assert.IsTrue(Helper.AreTheSameObject(transactionDTO.TransactionCategory,
                MapperCategory.ToCategoryDTO(transactionToConvert.TransactionCategory)));
        }

        #endregion
        
        #region ToListOfTransactionDTO

        [TestMethod]
        public void GivenTransactionList_ShouldConvertItToTransactionDTOList()
        {
            Category exampleCategory = new Category("Food", StatusEnum.Enabled, TypeEnum.Income);
            exampleCategory.CategoryId = 1;
            
            Transaction transaction1 = new Transaction("Spent on food", 200, DateTime.Now.Date,
                CurrencyEnum.USA, TypeEnum.Income, exampleCategory);
            transaction1.TransactionId = 1;

            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(transaction1);

            List<TransactionDTO> transactionsDTO =
                MapperTransaction.ToListOfTransactionsDTO(transactions);

            Assert.IsInstanceOfType(transactionsDTO[0], typeof(TransactionDTO));
            Assert.AreEqual(transaction1.TransactionId, transactionsDTO[0].TransactionId);
            Assert.AreEqual(transaction1.Title, transactionsDTO[0].Title);
            Assert.AreEqual(transaction1.CreationDate, transactionsDTO[0].CreationDate);
            Assert.AreEqual(transaction1.Amount, transactionsDTO[0].Amount);
            Assert.AreEqual((CurrencyEnumDTO)transaction1.Currency, transactionsDTO[0].Currency);
            Assert.AreEqual((TypeEnumDTO)transaction1.Type, transactionsDTO[0].Type);
            Assert.AreEqual(transaction1.AccountId,transactionsDTO[0].AccountId);
            Assert.IsTrue(Helper.AreTheSameObject(MapperCategory.ToCategoryDTO(transaction1.TransactionCategory),
                transactionsDTO[0].TransactionCategory));
            
        }
        
        #endregion
        
        #region ToListOfTransaction

        [TestMethod]
        public void GivenTransactionsDTO_ShouldConvertIntoTransactions()
        {
            CategoryDTO exampleCategory = new CategoryDTO("Food", StatusEnumDTO.Enabled, TypeEnumDTO.Income,1);
            exampleCategory.CategoryId = 1;
            
            TransactionDTO transaction1 = new TransactionDTO("Spent on food",DateTime.Now.Date, 200, 
                CurrencyEnumDTO.USA, TypeEnumDTO.Income, exampleCategory,1);
            transaction1.TransactionId = 1;

            List<TransactionDTO> transactionsDTO = new List<TransactionDTO>();
            transactionsDTO.Add(transaction1);

            List<Transaction> transactions = MapperTransaction.ToListOfTransactions(transactionsDTO);

            Assert.IsInstanceOfType(transactions[0], typeof(Transaction));
        }



        #endregion
        
    }
}