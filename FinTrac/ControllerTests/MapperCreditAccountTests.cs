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
    public class MapperCreditAccountTests
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
        public void GivenMonetaryAccount_ShouldConvertItToMonetaryAccountDTO()
        {
            CreditCardAccount givenCreditAccount = new CreditCardAccount("Brou", CurrencyEnum.USA, DateTime.Now.Date, "Brou", "1233", 1000, new DateTime(2024, 11, 12));

            CreditCardAccountDTO accountConverted = MapperCreditAccount.ToCreditAccountDTO(givenCreditAccount);

            Assert.AreEqual(typeof(CreditCardAccountDTO), accountConverted.GetType());
            Assert.AreEqual(givenCreditAccount.Name, accountConverted.Name);
            Assert.AreEqual(givenCreditAccount.AvailableCredit, accountConverted.AvailableCredit);
            Assert.AreEqual((CurrencyEnumDTO)givenCreditAccount.Currency, accountConverted.Currency);
            Assert.AreEqual(givenCreditAccount.AccountId, accountConverted.CreditCardAccountId);
            Assert.AreEqual(givenCreditAccount.UserId, accountConverted.UserId);
            Assert.AreEqual(givenCreditAccount.CreationDate, accountConverted.CreationDate);
            Assert.AreEqual(givenCreditAccount.ClosingDate, accountConverted.ClosingDate);
            Assert.AreEqual(givenCreditAccount.IssuingBank, accountConverted.IssuingBank);
            Assert.AreEqual(givenCreditAccount.Last4Digits, accountConverted.Last4Digits);
        }


    }
}