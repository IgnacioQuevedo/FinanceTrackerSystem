using BusinessLogic.Dtos_Components;
using BusinessLogic.ExchangeHistory_Components;

namespace Controller.Mappers;

public abstract class MapperExchangeHistory
{
    public static ExchangeHistory ToExchangeHistory(ExchangeHistoryDTO exchangeHistoryDto)
    {
        ExchangeHistory exchangeHistory =
            new ExchangeHistory(exchangeHistoryDto.Currency, exchangeHistoryDto.Value, exchangeHistoryDto.ValueDate);

        exchangeHistory.UserId = exchangeHistoryDto.UserId;

        return exchangeHistory;
    }
}