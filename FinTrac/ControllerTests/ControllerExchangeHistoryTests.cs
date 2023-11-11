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
        public void CreateExchangeMethodWithCorrectData_ShoulAddExchangeHistoryToDb()
        {
            ExchangeHistoryDTO exchangeHistoryToCreate =
                new ExchangeHistoryDTO(CurrencyEnum.USA, 39.5M, DateTime.Now, _userConnected.UserId);
            exchangeHistoryToCreate.UserId = 1;
            exchangeHistoryToCreate.ExchangeHistoryId = 1;
            
            DateTime creationDate = new DateTime(2023, 3, 2, 12, 12, 00);
            
            ExchangeHistoryDTO anotherExchangeHistory =
                new ExchangeHistoryDTO(CurrencyEnum.USA, 2.5M, creationDate, _userConnected.UserId);

            anotherExchangeHistory.UserId = 1;
            anotherExchangeHistory.ExchangeHistoryId = 1;
            
            _controller.CreateExchangeHistory(exchangeHistoryToCreate);
            _controller.CreateExchangeHistory(anotherExchangeHistory);

            ExchangeHistory exchangeHistoryInDb = _testDb.Users.First().MyExchangesHistory[0];
            ExchangeHistory anotherExchangeHistoryInDb = _testDb.Users.First().MyExchangesHistory[1];

            Assert.AreEqual(2, _testDb.Users.First().MyExchangesHistory.Count);
            
            Assert.AreEqual(exchangeHistoryToCreate.ExchangeHistoryId, exchangeHistoryInDb.ExchangeHistoryId);
            Assert.AreEqual(exchangeHistoryToCreate.UserId, exchangeHistoryInDb.UserId);
            Assert.AreEqual(exchangeHistoryToCreate.Currency, exchangeHistoryInDb.Currency);
            Assert.AreEqual(exchangeHistoryToCreate.Value, exchangeHistoryInDb.Value);
            Assert.AreEqual(exchangeHistoryToCreate.ValueDate, exchangeHistoryInDb.ValueDate);
            
            Assert.AreEqual(anotherExchangeHistory.Currency,anotherExchangeHistoryInDb.Currency);
        }
    }
}