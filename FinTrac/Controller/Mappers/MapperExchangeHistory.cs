using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
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
                new ExchangeHistory((CurrencyEnum)exchangeHistoryDto.Currency, exchangeHistoryDto.Value,
                    exchangeHistoryDto.ValueDate);

            exchangeHistory.ExchangeHistoryId = exchangeHistoryDto.ExchangeHistoryId;
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
            new ExchangeHistoryDTO( (CurrencyEnumDTO)exchangeHistory.Currency, exchangeHistory.Value, exchangeHistory.ValueDate,
                exchangeHistory.UserId);

        exchangeHistoryDTO.ExchangeHistoryId = exchangeHistory.ExchangeHistoryId;

        return exchangeHistoryDTO;
    }

    public static List<ExchangeHistoryDTO> ToListOfExchangeHistoryDTO(List<ExchangeHistory> exchangeHistoryList)
    {
        List<ExchangeHistoryDTO> listExchangeHistoryDTO = new List<ExchangeHistoryDTO>();

        foreach (ExchangeHistory exchangeHistory in exchangeHistoryList)
        {
            ExchangeHistoryDTO exchangeHistoryDTO = ToExchangeHistoryDTO(exchangeHistory);
            listExchangeHistoryDTO.Add(exchangeHistoryDTO);
        }

        return listExchangeHistoryDTO;
    }

    public static List<ExchangeHistory> ToListOfExchangeHistory(List<ExchangeHistoryDTO> exchangeHistoryDtoList)
    {
        List<ExchangeHistory> listOfExchangeHistories = new List<ExchangeHistory>();

        foreach (ExchangeHistoryDTO exchangeHistoryDTO in exchangeHistoryDtoList)
        {
            ExchangeHistory exchangeHistory = MapperExchangeHistory.ToExchangeHistory(exchangeHistoryDTO);
            listOfExchangeHistories.Add(exchangeHistory);
        }

        return listOfExchangeHistories;
    }
}