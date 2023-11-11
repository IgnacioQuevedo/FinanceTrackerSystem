using System.Security.Cryptography.X509Certificates;
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

            _controller.CreateExchangeHistory(exchangeHistoryToCreate);
            
            Assert.AreEqual(1,_testDb.Users.First().MyExchangesHistory.Count);
            
        }
        
    }
}