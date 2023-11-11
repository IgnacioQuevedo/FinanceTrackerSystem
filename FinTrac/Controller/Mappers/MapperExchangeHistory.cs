using BusinessLogic.Dtos_Components;
using BusinessLogic.Exceptions;
using BusinessLogic.ExchangeHistory_Components;
using Mappers;

namespace Controller.Mappers;

public abstract class MapperExchangeHistory
{
    public static ExchangeHistory ToExchangeHistory(ExchangeHistoryDTO exchangeHistoryDto)
    {

        try
        {
            ExchangeHistory exchangeHistory =
                new ExchangeHistory(exchangeHistoryDto.Currency, exchangeHistoryDto.Value, exchangeHistoryDto.ValueDate);

            exchangeHistory.UserId = exchangeHistoryDto.UserId;

            return exchangeHistory;
        }
        catch (ExceptionExchangeHistory Exception)
        {
            throw new ExceptionMapper(Exception.Message);
        }
        
    }

    public static ExchangeHistoryDTO ToExchangeHistoryDTO(ExchangeHistory exchangeHistory)
    {
        ExchangeHistoryDTO exchangeHistoryDTO =
            new ExchangeHistoryDTO(exchangeHistory.Currency, exchangeHistory.Value, exchangeHistory.ValueDate,exchangeHistory.UserId);

        exchangeHistoryDTO.ExchangeHistoryId = exchangeHistory.ExchangeHistoryId;

        return exchangeHistoryDTO;
    }
}