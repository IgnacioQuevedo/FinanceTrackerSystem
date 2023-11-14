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
        
        public List<TransactionDTO> GetAllTransactions(AccountDTO accountWithTransactions);

        public AccountDTO FindAccountById(int? accountId, int? userId);
        
        public List<CategoryDTO> GetAllCategories(int userConnectedId);
        public CategoryDTO FindCategory(int idOfCategoryToFind, int idUserConnected);

    }
}