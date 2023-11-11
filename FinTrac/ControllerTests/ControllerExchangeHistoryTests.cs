using System.Security.Cryptography.X509Certificates;
using BusinessLogic.Account_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.Transaction_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.ExchangeHistory_Components;
using BusinessLogic.User_Components;
using Controller;
using Controller.Mappers;
using DataManagers;

namespace ControllerTests
{
    [TestClass]
    public class ControllerExchangeHistoryTests
    {
        #region Initialize

        private GenericController _controller;
        private SqlContext _testDb;
        private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();

        private UserRepositorySql _userRepo;
        private UserDTO _userConnected;

        private ExchangeHistoryDTO exchangeHistoryToCreate;
        private ExchangeHistoryDTO anotherExchangeHistory;
        private DateTime creationDate;
        
        

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
            
            
             exchangeHistoryToCreate =
                new ExchangeHistoryDTO(CurrencyEnum.USA, 39.5M, DateTime.Now, _userConnected.UserId);
            exchangeHistoryToCreate.UserId = 1;
            exchangeHistoryToCreate.ExchangeHistoryId = 1;

            creationDate = new DateTime(2023, 3, 2, 12, 12, 00);
            
             anotherExchangeHistory =
                new ExchangeHistoryDTO(CurrencyEnum.USA, 2.5M, creationDate, _userConnected.UserId);

            anotherExchangeHistory.UserId = 1;
            anotherExchangeHistory.ExchangeHistoryId = 1;

            _controller.CreateExchangeHistory(exchangeHistoryToCreate);
            _controller.CreateExchangeHistory(anotherExchangeHistory);
            
        }

        #endregion

        #region Cleanup

        [TestCleanup]
        public void CleanUp()
        {
            _testDb.Database.EnsureDeleted();
        }

        #endregion


        #region Create ExchangeHistory

        [TestMethod]
        public void CreateExchangeMethodWithCorrectData_ShoulAddExchangeHistoryToDb()
        {
            ExchangeHistory exchangeHistoryInDb = _testDb.Users.First().MyExchangesHistory[0];
            ExchangeHistory anotherExchangeHistoryInDb = _testDb.Users.First().MyExchangesHistory[1];

            Assert.AreEqual(2, _testDb.Users.First().MyExchangesHistory.Count);

            Assert.AreEqual(exchangeHistoryToCreate.ExchangeHistoryId, exchangeHistoryInDb.ExchangeHistoryId);
            Assert.AreEqual(exchangeHistoryToCreate.UserId, exchangeHistoryInDb.UserId);
            Assert.AreEqual(exchangeHistoryToCreate.Currency, exchangeHistoryInDb.Currency);
            Assert.AreEqual(exchangeHistoryToCreate.Value, exchangeHistoryInDb.Value);
            Assert.AreEqual(exchangeHistoryToCreate.ValueDate, exchangeHistoryInDb.ValueDate);

            Assert.AreEqual(anotherExchangeHistory.Currency, anotherExchangeHistoryInDb.Currency);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateExchangeMethodWithIncorrectData_ShoulThrowException()
        {
            exchangeHistoryToCreate.Value = -1;
            _controller.CreateExchangeHistory(exchangeHistoryToCreate);
        }
        
        
        
        #endregion
    }
}