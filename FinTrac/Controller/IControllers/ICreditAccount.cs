using BusinessLogic.Dtos_Components;

namespace Controller.IControllers
{
    public interface ICreditAccount
    {
        public void CreateCreditAccount(CreditCardAccountDTO dtoToAdd);
        public CreditCardAccountDTO FindCreditAccount(int idOfAccountToFind, int idUserConnected);
        public void UpdateCreditAccount(CreditCardAccountDTO creditDtoWithUpdates);
        public void DeleteCreditAccount(CreditCardAccountDTO creditAccountToDelete);
        public List<CreditCardAccountDTO> GetAllCreditAccounts(int userConnectedId);


    }
}