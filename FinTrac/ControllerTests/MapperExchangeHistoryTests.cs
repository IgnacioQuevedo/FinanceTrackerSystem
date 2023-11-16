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
                new ExchangeHistoryDTO(CurrencyEnumDTO.USA, 38.5M, DateTime.Now, _userConnected.UserId);

            ExchangeHistory exchangeHistoryGenerated = MapperExchangeHistory.ToExchangeHistory(exchangeHistoryDTO);


            Assert.IsInstanceOfType(exchangeHistoryGenerated, typeof(ExchangeHistory));
            Assert.AreEqual(exchangeHistoryGenerated.Currency, (CurrencyEnum)exchangeHistoryDTO.Currency);
            Assert.AreEqual(exchangeHistoryGenerated.Value, exchangeHistoryDTO.Value);
            Assert.AreEqual(exchangeHistoryGenerated.ValueDate, exchangeHistoryDTO.ValueDate);
            Assert.AreEqual(exchangeHistoryGenerated.UserId, exchangeHistoryDTO.UserId);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceptionMapper))]
        public void GivenExchangeHistoryDTOWithIncorrectData_ShouldThrowException()
        {
            ExchangeHistoryDTO exchangeHistoryDTO =
                new ExchangeHistoryDTO(CurrencyEnumDTO.USA, -3, DateTime.Now, _userConnected.UserId);

            MapperExchangeHistory.ToExchangeHistory(exchangeHistoryDTO);
        }

        #endregion

        #region ToExchangeHistoryDTO

        [TestMethod]
        public void GivenExchangeHistory_ShouldBePossibleToConvertToExchangeHistoryDTO()
        {
            ExchangeHistory exchangeHistory = new ExchangeHistory(CurrencyEnum.USA, 30.8M, DateTime.Now.Date);
            exchangeHistory.UserId = 1;

            ExchangeHistoryDTO exchangeHistoryDTOCreated = MapperExchangeHistory.ToExchangeHistoryDTO(exchangeHistory);

            Assert.IsInstanceOfType(exchangeHistoryDTOCreated, typeof(ExchangeHistoryDTO));
            Assert.AreEqual(exchangeHistoryDTOCreated.ExchangeHistoryId, exchangeHistory.ExchangeHistoryId);
            Assert.AreEqual((CurrencyEnum)exchangeHistoryDTOCreated.Currency, exchangeHistory.Currency);
            Assert.AreEqual(exchangeHistoryDTOCreated.Value, exchangeHistory.Value);
            Assert.AreEqual(exchangeHistoryDTOCreated.ValueDate, exchangeHistory.ValueDate);
            Assert.AreEqual(exchangeHistoryDTOCreated.UserId, exchangeHistory.UserId);
        }

        #endregion

        #region ToListOfExchangeHistoryDTO

        [TestMethod]
        public void GivenExchangeHistoryList_ShouldConvertItToExchangeHistoryDTOList()
        {
            ExchangeHistory exchangeHistory1 = new ExchangeHistory(CurrencyEnum.USA, 38.5M, DateTime.Now.Date);
            ExchangeHistory exchangeHistory2 = new ExchangeHistory(CurrencyEnum.USA, 40, DateTime.Now.Date);

            List<ExchangeHistory> exchangeHistoryList = new List<ExchangeHistory>();

            exchangeHistoryList.Add(exchangeHistory1);
            exchangeHistoryList.Add(exchangeHistory2);

            List<ExchangeHistoryDTO> exchangeHistoryDTOList =
                MapperExchangeHistory.ToListOfExchangeHistoryDTO(exchangeHistoryList);

            Assert.IsInstanceOfType(exchangeHistoryDTOList[0], typeof(ExchangeHistoryDTO));
            Assert.IsInstanceOfType(exchangeHistoryDTOList[1], typeof(ExchangeHistoryDTO));

            Assert.AreEqual(exchangeHistory1.UserId, exchangeHistoryDTOList[0].UserId);
            Assert.AreEqual((CurrencyEnumDTO)exchangeHistory1.Currency, exchangeHistoryDTOList[0].Currency);
            Assert.AreEqual(exchangeHistory1.Value, exchangeHistoryDTOList[0].Value);
            Assert.AreEqual(exchangeHistory1.ValueDate, exchangeHistoryDTOList[0].ValueDate);
            Assert.AreEqual(exchangeHistory1.ExchangeHistoryId, exchangeHistoryDTOList[0].ExchangeHistoryId);
        }

        #endregion

        #region ToListOfExchangeHistory

        [TestMethod]
        public void GivenExchangeHistoryDTOToList_ShouldConvertItToExchangeHistoryList()
        {
            ExchangeHistoryDTO exchangeHistory1 = new ExchangeHistoryDTO(CurrencyEnumDTO.USA, 38.5M, DateTime.Now.Date, 1);
            ExchangeHistoryDTO exchangeHistory2 = new ExchangeHistoryDTO(CurrencyEnumDTO.USA, 40, DateTime.Now.Date, 1);


            List<ExchangeHistoryDTO> exchangeHistoryDTOList = new List<ExchangeHistoryDTO>();
            exchangeHistoryDTOList.Add(exchangeHistory1);
            exchangeHistoryDTOList.Add(exchangeHistory2);

            List<ExchangeHistory> exchangeHistories = MapperExchangeHistory.ToListOfExchangeHistory(exchangeHistoryDTOList);

            Assert.IsInstanceOfType(exchangeHistories[0], typeof(ExchangeHistory));
            Assert.IsInstanceOfType(exchangeHistories[1], typeof(ExchangeHistory));
        }



        #endregion
    }
}