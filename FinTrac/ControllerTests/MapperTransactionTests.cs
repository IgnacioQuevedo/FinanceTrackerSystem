using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.Transaction_Components;
using Controller;
using Controller.Mappers;
using DataManagers;

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
        
        #region ToTransaction

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

        #endregion
        
        
        
    }
}