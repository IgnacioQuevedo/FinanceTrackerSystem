using BusinessLogic.Dtos_Components;

namespace Controller.IControllers
{
    public interface IExchangeHistoryController
    {
        public void CreateExchangeHistory(ExchangeHistoryDTO exchangeHistoryToCreate);

        public ExchangeHistoryDTO FindExchangeHistory(int IdOfExchangeToFound,int idUserConnected);

        public void UpdateExchangeHistory(ExchangeHistoryDTO dtoWithUpdates);

        public void DeleteExchangeHistory(ExchangeHistoryDTO dtoToDelete);

        public List<ExchangeHistoryDTO> GetAllExchangeHistories(int userConnectedId);
    }
}