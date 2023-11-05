using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Dto_Components;
using BusinessLogic.Dtos_Components;

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

        [TestMethod]

        public void GivenMaxAmountToSpend_ShouldBeSetted()
        {
            _goalDTO.MaxAmountToSpend = 100;
            Assert.AreEqual(0, _goalDTO.MaxAmountToSpend);
        }


    }
}
