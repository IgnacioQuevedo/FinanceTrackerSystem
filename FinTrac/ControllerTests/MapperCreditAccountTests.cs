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

        #region To Credit Account

        [TestMethod]
        public void GivenCreditAccount_ShouldConvertItToCreditAccountDTO()
        {
            CreditCardAccount givenCreditAccount = new CreditCardAccount("Brou", CurrencyEnum.USA, DateTime.Now.Date, "Brou", "1233", 1000, new DateTime(2024, 11, 12));

            CreditCardAccountDTO accountConverted = MapperCreditAccount.ToCreditAccountDTO(givenCreditAccount);

            Assert.AreEqual(typeof(CreditCardAccountDTO), accountConverted.GetType());
            Assert.AreEqual(givenCreditAccount.Name, accountConverted.Name);
            Assert.AreEqual(givenCreditAccount.AvailableCredit, accountConverted.AvailableCredit);
            Assert.AreEqual((CurrencyEnumDTO)givenCreditAccount.Currency, accountConverted.Currency);
            Assert.AreEqual(givenCreditAccount.AccountId, accountConverted.AccountId);
            Assert.AreEqual(givenCreditAccount.UserId, accountConverted.UserId);
            Assert.AreEqual(givenCreditAccount.CreationDate, accountConverted.CreationDate);
            Assert.AreEqual(givenCreditAccount.ClosingDate, accountConverted.ClosingDate);
            Assert.AreEqual(givenCreditAccount.IssuingBank, accountConverted.IssuingBank);
            Assert.AreEqual(givenCreditAccount.Last4Digits, accountConverted.Last4Digits);
        }

        #endregion

        #region To ListOfCreditAccountDTO

        [TestMethod]
        public void GivenListOfCreditAccounts_ShouldConvertToListOfCreditAccountDTO()
        {
            CreditCardAccount givenCreditAccount = new CreditCardAccount("Brou", CurrencyEnum.UY, DateTime.Now.Date, "Brous", "1244", 1000, new DateTime(2024, 12, 11));

            List<CreditCardAccount> creditAccounts = new List<CreditCardAccount>();
            creditAccounts.Add(givenCreditAccount);

            List<CreditCardAccountDTO> listConverted = MapperCreditAccount.ToListOfCreditAccountDTO(creditAccounts);

            Assert.AreEqual(1, listConverted.Count);
            Assert.AreEqual(creditAccounts[0].Name, listConverted[0].Name);
            Assert.AreEqual(creditAccounts[0].AccountId, listConverted[0].AccountId);
            Assert.AreEqual(creditAccounts[0].AvailableCredit, listConverted[0].AvailableCredit);
            Assert.AreEqual(creditAccounts[0].UserId, listConverted[0].UserId);
            Assert.AreEqual(creditAccounts[0].CreationDate, listConverted[0].CreationDate);
            Assert.AreEqual(creditAccounts[0].ClosingDate, listConverted[0].ClosingDate);
            Assert.AreEqual(creditAccounts[0].Currency, (CurrencyEnum)listConverted[0].Currency);
            Assert.AreEqual(creditAccounts[0].IssuingBank, listConverted[0].IssuingBank);
            Assert.AreEqual(creditAccounts[0].Last4Digits, listConverted[0].Last4Digits);
        }

        #endregion

        #region To CreditAccount

        [TestMethod]
        public void GivenCreditCardAccountDTO_ShouldConvertToCreditCardAccount()
        {
            CreditCardAccountDTO givenCreditAccountDTO = new CreditCardAccountDTO("Brou", CurrencyEnumDTO.EUR, DateTime.Now.Date, "Brou", "1233", 1000, new DateTime(2024, 11, 12), 1);

            CreditCardAccount accountConverted = MapperCreditAccount.ToCreditAccount(givenCreditAccountDTO);

            Assert.AreEqual(typeof(CreditCardAccount), accountConverted.GetType());
            Assert.AreEqual(givenCreditAccountDTO.Name, accountConverted.Name);
            Assert.AreEqual(givenCreditAccountDTO.AvailableCredit, accountConverted.AvailableCredit);
            Assert.AreEqual(givenCreditAccountDTO.Currency, (CurrencyEnumDTO)accountConverted.Currency);
            Assert.AreEqual(givenCreditAccountDTO.AccountId, accountConverted.AccountId);
            Assert.AreEqual(givenCreditAccountDTO.UserId, accountConverted.UserId);
            Assert.AreEqual(givenCreditAccountDTO.CreationDate, accountConverted.CreationDate);
            Assert.AreEqual(givenCreditAccountDTO.ClosingDate, accountConverted.ClosingDate);
            Assert.AreEqual(givenCreditAccountDTO.IssuingBank, accountConverted.IssuingBank);
            Assert.AreEqual(givenCreditAccountDTO.Last4Digits, accountConverted.Last4Digits);
        }

        #endregion
    }
}