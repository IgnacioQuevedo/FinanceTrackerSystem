using BusinessLogic.Account_Components;
using BusinessLogic.Dtos_Components;

namespace Controller.IControllers
{
    public interface ITransactionController
    {
        public void CreateTransaction(TransactionDTO dtoToAdd);

        public void UpdateTransaction(TransactionDTO dtoWithUpdates, int? UserId);

        public void DeleteTransaction(TransactionDTO dtoToDelete);

        public TransactionDTO FindTransaction(int idToFound, int? accountId, int? userId);
        
        public List<TransactionDTO> GetALlTransactions(int accountId);
        
    }
}