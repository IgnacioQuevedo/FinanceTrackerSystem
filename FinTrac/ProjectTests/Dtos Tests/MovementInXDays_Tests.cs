using BusinessLogic.Dtos_Components;

namespace BusinessLogicTests.Dto_Components
{
    [TestClass]
    public class MovementInXDays_Tests
    {
        #region Initialize

        private MovementInXDaysDTO movements;

        [TestInitialize]
        public void Initialize()
        {
            movements = new MovementInXDaysDTO();
        }

        #endregion

        [TestMethod]
        public void GivenSpendingArray_ShouldBeSet()
        {
            decimal[] spendings = new decimal[31];
            movements.Spendings = spendings;

            Assert.AreEqual(spendings, movements.Spendings);
        }

        [TestMethod]
        public void GivenIncomeArray_ShouldBeSet()
        {
            decimal[] income = new decimal[31];

            movements.Spendings = income;

            Assert.AreEqual(income, movements.Income);
        }
    }
}