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

        [TestInitialize]
        public void Initialize()
        {
            _goalDTO = new GoalDTO();
        }

        [TestMethod]
        public void GivenTitle_ShouldBeSetted()
        {
            string goalDTOTitle = "Less Party";
            _goalDTO.Title = goalDTOTitle;
            Assert.AreEqual(goalDTOTitle, _goalDTO.Title);
        }
    }
}
