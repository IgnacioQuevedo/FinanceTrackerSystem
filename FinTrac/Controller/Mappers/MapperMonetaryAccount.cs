using BusinessLogic.Account_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using BusinessLogic.ExchangeHistory_Components;
using Mappers;

namespace Controller.Mappers;

public abstract class MapperMonetaryAccount
{
    #region To MonetaryAccountDTO

    public static MonetaryAccountDTO ToMonetaryAccountDTO(MonetaryAccount myMonetaryAccount)
    {
        MonetaryAccountDTO monetaryAccountDTO =
            new MonetaryAccountDTO(myMonetaryAccount.Name, myMonetaryAccount.Amount, (CurrencyEnumDTO)myMonetaryAccount.Currency, myMonetaryAccount.CreationDate, myMonetaryAccount.UserId);

        monetaryAccountDTO.MonetaryAccountId = myMonetaryAccount.AccountId;

        return monetaryAccountDTO;
    }

    public static List<MonetaryAccountDTO> ToListOfMonetaryAccountDTO(List<MonetaryAccount> myListOfMonetaryAccount)
    {
        List<MonetaryAccountDTO> listMonetaryAccountDTO = new List<MonetaryAccountDTO>();

        foreach (MonetaryAccount monetAccount in myListOfMonetaryAccount)
        {
            MonetaryAccountDTO monetAccountDTO = ToMonetaryAccountDTO(monetAccount);
            listMonetaryAccountDTO.Add(monetAccountDTO);
        }

        return listMonetaryAccountDTO;
    }



    #endregion  

}