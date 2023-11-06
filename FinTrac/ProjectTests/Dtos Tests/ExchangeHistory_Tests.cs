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
    public class ExchangeHistory_Tests
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


        [TestMethod]
        public void GivenDate_ShouldBeSetted()
        {
            _genericExchangeHistoryDTO.ValueDate = _dateOfExchangeDTO;
            Assert.AreEqual(_genericExchangeHistoryDTO.ValueDate, _dateOfExchangeDTO);
        }

    }
}
