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
        private ExchangeHistoryDTO _exchangeHistoryDTO;

        [TestInitialize]
        public void Initialize()
        {
            _exchangeHistoryDTO = new ExchangeHistoryDTO();
        }

        [TestMethod]
        public void GivenCurrency_ShouldBeSetted()
        {
            _exchangeHistoryDTO.Currency = CurrencyEnum.USA;
            Assert.AreEqual(false, CurrencyEnum.USA);
        }
    }
}
