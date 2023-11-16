using BusinessLogic.Dtos_Components;

namespace Controller.IControllers
{
    public interface IMonetaryAccount
    {
        public void CreateMonetaryAccount(MonetaryAccountDTO dtoToAdd);
        public MonetaryAccountDTO FindMonetaryAccount(int idOfAccountToFind, int idUserConnected);
        public void UpdateMonetaryAccount(MonetaryAccountDTO monetaryDtoWithUpdates);
        public void DeleteMonetaryAccount(MonetaryAccountDTO monetaryAccountToDelete);
        public List<MonetaryAccountDTO> GetAllMonetaryAccounts(int userConnectedId);


    }
}