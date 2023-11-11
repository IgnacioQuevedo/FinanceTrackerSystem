using BusinessLogic.Dtos_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Enums;

namespace BusinessLogicTests.Dtos_Tests
{
    [TestClass]
    public class ExchangeHistoryDTO_Tests
    {
        private ExchangeHistoryDTO _genericExchangeHistoryDTO;
        private CurrencyEnum _genericCurrencyDTO;
        private decimal genericValueDTO;
        private DateTime _dateOfExchangeDTO;

        #region Initialize

        [TestInitialize]
        public void Initialize()
        {
            _genericExchangeHistoryDTO = new ExchangeHistoryDTO();
            _genericCurrencyDTO = CurrencyEnum.USA;
            _dateOfExchangeDTO = new DateTime(2023, 05, 01);
        }

        #endregion

        #region Currency

        [TestMethod]
        public void GivenCurrency_ShouldBeSetted()
        {
            _genericExchangeHistoryDTO.Currency = _genericCurrencyDTO;
            Assert.AreEqual(_genericExchangeHistoryDTO.Currency, _genericCurrencyDTO);
        }

        #endregion

        #region Value of exchange

        [TestMethod]
        public void GivenValue_ShouldBeSetted()
        {
            _genericExchangeHistoryDTO.Value = genericValueDTO;
            Assert.AreEqual(_genericExchangeHistoryDTO.Value, genericValueDTO);
        }

        #endregion

        #region Date

        [TestMethod]
        public void GivenDate_ShouldBeSetted()
        {
            _genericExchangeHistoryDTO.ValueDate = _dateOfExchangeDTO;
            Assert.AreEqual(_genericExchangeHistoryDTO.ValueDate, _dateOfExchangeDTO);
        }

        #endregion
        
        #region User Id

        [TestMethod]
        public void GivenUserId_ShouldBeSetted()
        {
            int genericUserId = 1;
            _genericExchangeHistoryDTO.UserId = genericUserId;

            Assert.AreEqual(genericUserId,  _genericExchangeHistoryDTO.UserId);
        }

        #endregion

        #region Constructor

        [TestMethod]
        public void GivenValues_ShouldBePossibleToCreateExchangeHistoryDTO()
        {
            ExchangeHistoryDTO exchangeHistoryDTO = new ExchangeHistoryDTO(_genericCurrencyDTO, genericValueDTO, _dateOfExchangeDTO, 1);

            Assert.AreEqual(_genericCurrencyDTO, exchangeHistoryDTO.Currency);
            Assert.AreEqual(genericValueDTO, exchangeHistoryDTO.Value);
            Assert.AreEqual(_dateOfExchangeDTO, exchangeHistoryDTO.ValueDate);
            Assert.AreEqual(exchangeHistoryDTO.UserId,1);
        }

        #endregion

    }
}
