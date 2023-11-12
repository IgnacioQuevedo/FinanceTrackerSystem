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
            
            Transaction transactionToConvert = new Transaction("Spent on food", 200,DateTime.Now.Date, 
                CurrencyEnum.USA, TypeEnum.Income, exampleCategory);
            transactionToConvert.TransactionId = 1;
            
            TransactionDTO transactionDTO = MapperTransaction.ToTransactionDTO(transactionToConvert);
            
            
            Assert.IsInstanceOfType(transactionDTO, typeof(TransactionDTO));
            Assert.AreEqual(transactionDTO.TransactionId, transactionToConvert.TransactionId);
            Assert.AreEqual(transactionDTO.Title, transactionToConvert.Title);
            Assert.AreEqual(transactionDTO.Amount, transactionToConvert.Amount);
            Assert.AreEqual(transactionDTO.CreationDate, transactionToConvert.CreationDate);
            Assert.AreEqual((CurrencyEnum)transactionDTO.Currency, transactionToConvert.Currency);
            Assert.AreEqual((TypeEnum) transactionDTO.Type, transactionToConvert.Type);
            Assert.IsTrue(Helper.AreTheSameObject(transactionDTO.TransactionCategory, 
                MapperCategory.ToCategoryDTO(transactionToConvert.TransactionCategory)));
        }

        
        #endregion
        
        
        
    }
}