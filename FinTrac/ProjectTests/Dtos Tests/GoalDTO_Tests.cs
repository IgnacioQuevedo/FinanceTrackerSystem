using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Dto_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.Goal_Components;

namespace BusinessLogicTests.Dtos_Tests
{
    [TestClass]
    public class GoalDTO_Tests
    {
        private GoalDTO _goalDTO;

        #region Initialize

        [TestInitialize]
        public void Initialize()
        {
            _goalDTO = new GoalDTO();
        }

        #endregion

        #region Title

        [TestMethod]
        public void GivenTitle_ShouldBeSetted()
        {
            string goalDTOTitle = "Less Party";
            _goalDTO.Title = "Less Party";

            Assert.AreEqual(goalDTOTitle, _goalDTO.Title);
        }

        #endregion

        #region Max amount to spend
        [TestMethod]
        public void GivenMaxAmountToSpend_ShouldBeSetted()
        {
            int maxAmountToSpendForDTO = 100;
            _goalDTO.MaxAmountToSpend = maxAmountToSpendForDTO;

            Assert.AreEqual(maxAmountToSpendForDTO, _goalDTO.MaxAmountToSpend);
        }

        #endregion

        [TestMethod]
        public void GivenCurrencyOfAmount_ShoulBeSetted()
        {
            _goalDTO.CurrencyOfAmount = CurrencyEnum.USA;
            bool belongToEnum = Enum.IsDefined(typeof(CurrencyEnum), _goalDTO.CurrencyOfAmount);
            Assert.IsTrue(belongToEnum);
        }




    }
}
