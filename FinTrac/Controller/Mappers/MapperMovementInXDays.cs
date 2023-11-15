using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Exceptions;
using BusinessLogic.Enums;
using BusinessLogic.Goal_Components;
using Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Report_Components;

namespace Controller.Mappers
{
    public abstract class MapperMovementInXDays
    {
        public static MovementInXDays ToMovement(MovementInXDaysDTO movementsDto)
        {
            RangeOfDates rangeOfDates = new RangeOfDates(movementsDto.RangeOfDates.InitialDate,
                movementsDto.RangeOfDates.FinalDate);
            
            MovementInXDays movements = new MovementInXDays(rangeOfDates);
            
            return movements;
        }

        public static MovementInXDaysDTO ToMovementDTO(MovementInXDays movements)
        {

            RangeOfDatesDTO rangeOfDatesDto =
                new RangeOfDatesDTO(movements.RangeOfDates.InitialDate, movements.RangeOfDates.FinalDate);

            MovementInXDaysDTO movementInXDaysDto = new MovementInXDaysDTO(rangeOfDatesDto);

            return movementInXDaysDto;
        }
    }
}
