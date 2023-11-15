using BusinessLogic.Dtos_Components;
using BusinessLogic.Report_Components;

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
            decimal[] incomes = new decimal[31];

            movements.Incomes = incomes;
            Assert.AreEqual(incomes, movements.Incomes);
        }

        [TestMethod]
        public void GivenRangeOfDatesDTO_ShouldBeSet()
        {
            RangeOfDatesDTO rangeOfDatesDto = new RangeOfDatesDTO(new DateTime(2023,12,1).Date,
                new DateTime(2023,12,31).Date);

            movements.RangeOfDates = rangeOfDatesDto;
            
            Assert.AreEqual(rangeOfDatesDto,movements.RangeOfDates);
        }
        
        [TestMethod]
        public void GivenCorrectData_ShouldBePossibleToCreateAnMovementInXDays()
        {
            RangeOfDatesDTO rangeOfDatesDto = new RangeOfDatesDTO(new DateTime(2023,12,1).Date,
                new DateTime(2023,12,31).Date);
            decimal[] spendings = new decimal[31];
            decimal[] incomes = new decimal[31];
            
            MovementInXDaysDTO movements = new MovementInXDaysDTO(rangeOfDatesDto);
            
            Assert.AreEqual(rangeOfDatesDto,movements.RangeOfDates);
            Assert.AreEqual(spendings,movements.Spendings);
            Assert.AreEqual(incomes,movements.Incomes);
 
        }
        
    }
}