
using BusinessLogic.Report_Components;

namespace BusinessLogic.Dtos_Components
{
	public class MovementInXDaysDTO
	{
		public decimal[] Spendings { get; set; }
		public decimal[] Income { get; set; }
		public RangeOfDatesDTO RangeOfDates { get; set; }
	}
}