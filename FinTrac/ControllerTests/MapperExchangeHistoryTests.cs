using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.ExchangeHistory_Components;
using Controller;
using Controller.Mappers;
using DataManagers;
using Mappers;


namespace ControllerTests
{
    [TestClass]
    public class ExchangeHistoryMapper
    {
        #region Initialize

        private GenericController _controller;
        private SqlContext _testDb;
        private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();

        private UserRepositorySql _userRepo;
        private UserDTO _userDTO;
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

        #region ToExchangeHistory

        [TestMethod]
        public void GivenExchangeHistoryDTOWithCorrectData_ShouldBePossibleToConvertItToExchangeHistory()
        {
            ExchangeHistoryDTO exchangeHistoryDTO =
                new ExchangeHistoryDTO(CurrencyEnum.USA, 38.5M, DateTime.Now, _userConnected.UserId);

            ExchangeHistory exchangeHistoryGenerated = MapperExchangeHistory.ToExchangeHistory(exchangeHistoryDTO);


            Assert.IsInstanceOfType(exchangeHistoryGenerated, typeof(ExchangeHistory));
            Assert.AreEqual(exchangeHistoryGenerated.Currency, exchangeHistoryDTO.Currency);
            Assert.AreEqual(exchangeHistoryGenerated.Value, exchangeHistoryDTO.Value);
            Assert.AreEqual(exchangeHistoryGenerated.ValueDate, exchangeHistoryDTO.ValueDate);
            Assert.AreEqual(exchangeHistoryGenerated.UserId, exchangeHistoryDTO.UserId);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionMapper))]
        public void GivenExchangeHistoryDTOWithIncorrectData_ShouldThrowException()
        {
            ExchangeHistoryDTO exchangeHistoryDTO =
                new ExchangeHistoryDTO(CurrencyEnum.USA, -3, DateTime.Now, _userConnected.UserId);

            MapperExchangeHistory.ToExchangeHistory(exchangeHistoryDTO);
        }

        #endregion

        [TestMethod]
        public void GivenExchangeHistory_ShouldBePossibleToConvertToExchangeHistoryDTO()
        {
            ExchangeHistory exchangeHistory = new ExchangeHistory(CurrencyEnum.USA, 30.8M, DateTime.Now.Date);
            exchangeHistory.UserId = 1;

            ExchangeHistoryDTO exchangeHistoryDTOCreated = MapperExchangeHistory.ToExchangeHistoryDTO(exchangeHistory);
            
            Assert.IsInstanceOfType(exchangeHistoryDTOCreated,typeof(ExchangeHistoryDTO));
            Assert.AreEqual(exchangeHistoryDTOCreated.ExchangeHistoryId,exchangeHistory.ExchangeHistoryId);
            Assert.AreEqual(exchangeHistoryDTOCreated.Currency,exchangeHistory.Currency);
            Assert.AreEqual(exchangeHistoryDTOCreated.Value,exchangeHistory.Value);
            Assert.AreEqual(exchangeHistoryDTOCreated.ValueDate,exchangeHistory.ValueDate);
            Assert.AreEqual(exchangeHistoryDTOCreated.UserId,exchangeHistory.UserId);
        }
        
    }
}