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

        #region Initialize

        [TestInitialize]
        public void Initialize()
        {
            _genericExchangeHistoryDTO = new ExchangeHistoryDTO();
            _genericCurrencyDTO = CurrencyEnum.USA;
        }

        #endregion

        #region Currency

        [TestMethod]
        public void GivenCurrency_ShouldBeSetted()
        {
            _genericExchangeHistoryDTO.Currency = _genericCurrencyDTO;
            Assert.AreEqual(_genericExchangeHistoryDTO.Currency, _genericCurrencyDTO);
        }

        [TestMethod]
        public void GivenValue_ShouldBeSetted()
        {
            _genericExchangeHistoryDTO.Value = 41;
            Assert.AreEqual(_genericExchangeHistoryDTO.Value, 0);
        }

        #endregion
    }
}
