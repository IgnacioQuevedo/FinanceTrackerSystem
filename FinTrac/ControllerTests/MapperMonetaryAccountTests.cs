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

        [TestMethod]
        public void GivenListOfMonetaryAccount_ShouldConvertToListOfMonetaryAccountDTO()
        {
            MonetaryAccount givenMonetaryAccount1 = new MonetaryAccount("Brou", 1000, CurrencyEnum.UY, DateTime.Now.Date);

            List<MonetaryAccount> givenListOfMonetaryAccounts = new List<MonetaryAccount>();
            givenListOfMonetaryAccounts.Add(givenMonetaryAccount1);

            List<MonetaryAccountDTO> listConverted = MapperMonetaryAccount.ToListOfMonetaryAccountDTO(givenListOfMonetaryAccounts);

            Assert.AreEqual(1, listConverted.Count);
            Assert.AreEqual(givenListOfMonetaryAccounts[0].Name, listConverted[0].Name);
            Assert.AreEqual(givenListOfMonetaryAccounts[0].AccountId, listConverted[0].MonetaryAccountId);
            Assert.AreEqual(givenListOfMonetaryAccounts[0].Amount, listConverted[0].Amount);
            Assert.AreEqual(givenListOfMonetaryAccounts[0].UserId, listConverted[0].UserId);
            Assert.AreEqual(givenListOfMonetaryAccounts[0].CreationDate, listConverted[0].CreationDate);
            Assert.AreEqual(givenListOfMonetaryAccounts[0].Currency, (CurrencyEnum)listConverted[0].Currency);

        }

    }
}