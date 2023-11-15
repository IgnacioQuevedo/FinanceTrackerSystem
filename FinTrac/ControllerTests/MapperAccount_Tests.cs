using System.Diagnostics.CodeAnalysis;
using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.User_Components;
using Controller;
using DataManagers;
using Controller.Mappers;
using Mappers;
using BusinessLogic.Account_Components;
using Microsoft.Identity.Client;

namespace ControllerTests
{
    [TestClass]
    public class MapperAccount_Tests
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

        [TestMethod]
        public void GivenListAccountDTO_ShouldBeConvertedToLIstOfAccount()
        {
            List<AccountDTO> givenAccountDTOList = new List<AccountDTO>();
            MonetaryAccountDTO monetAccDTO = new MonetaryAccountDTO("Brou", 1000, CurrencyEnumDTO.UY, DateTime.Now, 1);
            givenAccountDTOList.Add(monetAccDTO);

            List<Account> accountConvertedList = MapperAccount.ToListAccount(givenAccountDTOList);

            Assert.AreEqual(accountConvertedList[0].Name, monetAccDTO.Name);
        }
    }
}