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

            movements.Income = income;
            Assert.AreEqual(income, movements.Income);
        }

        [TestMethod]
        public void GivenRangeOfDatesDTO_ShouldBeSet()
        {
            RangeOfDatesDTO rangeOfDatesDto = new RangeOfDatesDTO(new DateTime(2023,12,1).Date,
                new DateTime(2023,12,31).Date);

            movements.RangeOfDates = rangeOfDatesDto;
            
            Assert.AreEqual(rangeOfDatesDto,movements.RangeOfDates);
        }
    }
}