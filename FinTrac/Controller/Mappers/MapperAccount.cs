using BusinessLogic.Account_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using BusinessLogic.ExchangeHistory_Components;
using Mappers;

namespace Controller.Mappers;

public abstract class MapperAccount
{
    public static List<Account> ToListAccount(List<AccountDTO> myAccountsDTO)
    {
        throw new NotImplementedException();
    }
}