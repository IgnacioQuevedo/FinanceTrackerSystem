
using BusinessLogic.Report_Components;

namespace BusinessLogic.Dtos_Components
{
	public class MovementInXDaysDTO
	{
		public decimal[] Spendings { get; set; }
		public decimal[] Incomes { get; set; }
		public RangeOfDatesDTO RangeOfDates { get; set; }
	}
}