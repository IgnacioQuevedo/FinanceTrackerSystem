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

namespace ControllerTests
{
    [TestClass]
    public class MapperMonetaryAccountTests
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

        #region To MonetaryAccountDTO

        [TestMethod]
        public void GivenMonetaryAccount_ShouldConvertItToMonetaryAccountDTO()
        {
            MonetaryAccount givenMonetaryAccount = new MonetaryAccount("Brou", 1000, CurrencyEnum.UY, DateTime.Now.Date);

            MonetaryAccountDTO accountConverted = MapperMonetaryAccount.ToMonetaryAccountDTO(givenMonetaryAccount);

            Assert.AreEqual(typeof(MonetaryAccountDTO), accountConverted.GetType());
            Assert.AreEqual(givenMonetaryAccount.Name, accountConverted.Name);
            Assert.AreEqual(givenMonetaryAccount.Amount, accountConverted.Amount);
            Assert.AreEqual((CurrencyEnumDTO)givenMonetaryAccount.Currency, accountConverted.Currency);
            Assert.AreEqual(givenMonetaryAccount.AccountId, accountConverted.MonetaryAccountId);
            Assert.AreEqual(givenMonetaryAccount.UserId, accountConverted.UserId);
            Assert.AreEqual(givenMonetaryAccount.CreationDate, accountConverted.CreationDate);
        }

        #endregion

        #region To ToListOfMonetaryAccountDTO

        [TestMethod]
        public void GivenListOfMonetaryAccount_ShouldConvertToListOfMonetaryAccountDTO()
        {
            MonetaryAccount givenMonetaryAccount1 = new MonetaryAccount("Brou", 1000, CurrencyEnum.UY, DateTime.Now.Date);

            List<MonetaryAccount> monetAccounts = new List<MonetaryAccount>();
            monetAccounts.Add(givenMonetaryAccount1);

            List<MonetaryAccountDTO> listConverted = MapperMonetaryAccount.ToListOfMonetaryAccountDTO(monetAccounts);

            Assert.AreEqual(1, listConverted.Count);
            Assert.AreEqual(monetAccounts[0].Name, listConverted[0].Name);
            Assert.AreEqual(monetAccounts[0].AccountId, listConverted[0].MonetaryAccountId);
            Assert.AreEqual(monetAccounts[0].Amount, listConverted[0].Amount);
            Assert.AreEqual(monetAccounts[0].UserId, listConverted[0].UserId);
            Assert.AreEqual(monetAccounts[0].CreationDate, listConverted[0].CreationDate);
            Assert.AreEqual(monetAccounts[0].Currency, (CurrencyEnum)listConverted[0].Currency);

        }

        #endregion

        #region ToMonetaryAccount

        [TestMethod]
        public void GivenMonetaryAccountDTO_ShouldConvertToMonetaryAccount()
        {
            MonetaryAccountDTO givenMonetAccountDTO = new MonetaryAccountDTO("Brou", 1000, CurrencyEnumDTO.UY, DateTime.Now.Date, 1);

            MonetaryAccount accountConverted = MapperMonetaryAccount.ToMonetaryAccount(givenMonetAccountDTO);

            Assert.AreEqual(typeof(MonetaryAccount), accountConverted.GetType());
            Assert.AreEqual(givenMonetAccountDTO.Name, accountConverted.Name);
            Assert.AreEqual(givenMonetAccountDTO.Amount, accountConverted.Amount);
            Assert.AreEqual((CurrencyEnum)givenMonetAccountDTO.Currency, accountConverted.Currency);
            Assert.AreEqual(givenMonetAccountDTO.MonetaryAccountId, accountConverted.AccountId);
            Assert.AreEqual(givenMonetAccountDTO.UserId, accountConverted.UserId);
            Assert.AreEqual(givenMonetAccountDTO.CreationDate, accountConverted.CreationDate);
        }

        #endregion 

    }
}