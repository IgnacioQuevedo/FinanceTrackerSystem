using BusinessLogic.Dtos_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.User_Components;


namespace Controller.IControllers
{
    public interface IExchangeHistoryController
    {
        public void CreateExchangeHistory(ExchangeHistoryDTO exchangeHistoryToCreate);

        public void FindExchangeHistory(int exchangeHistoryId);

        public void UpdateExchangeHistory(ExchangeHistoryDTO exchangeHistoryToUpdate);

        public void DeleteExchangeHistory(int exchangeHistoryId);

        public List<ExchangeHistoryDTO> GetAllExchangeHistories(int userId);
    }
}