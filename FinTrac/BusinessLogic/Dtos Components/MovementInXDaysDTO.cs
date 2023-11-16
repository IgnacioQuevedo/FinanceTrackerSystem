
using BusinessLogic.Report_Components;

namespace BusinessLogic.Dtos_Components
{
	public class MovementInXDaysDTO
	{
		private int _amountOfDays;
		public decimal[] Spendings { get; set; }
		public decimal[] Incomes { get; set; }
		public RangeOfDatesDTO RangeOfDates { get; set; }

		public MovementInXDaysDTO()
		{
			
		}
		public MovementInXDaysDTO(RangeOfDatesDTO rangeOfDatesDto)
		{
			_amountOfDays = rangeOfDatesDto.FinalDate.Day - rangeOfDatesDto.InitialDate.Day + 1;
			Spendings = new decimal [31];
			Incomes = new decimal [31];
			RangeOfDates = rangeOfDatesDto;
		}
	}
}