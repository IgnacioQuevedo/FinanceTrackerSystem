using BusinessLogic.Account_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using BusinessLogic.ExchangeHistory_Components;
using Mappers;

namespace Controller.Mappers;

public abstract class MapperCreditAccount
{
    #region To CreditAccountDTO

    public static CreditCardAccountDTO ToCreditAccountDTO(CreditCardAccount myCreditAccount)
    {
        CreditCardAccountDTO creditAccountDTO =
          new CreditCardAccountDTO(myCreditAccount.Name, (CurrencyEnumDTO)myCreditAccount.Currency, myCreditAccount.CreationDate, myCreditAccount.IssuingBank, myCreditAccount.Last4Digits, myCreditAccount.AvailableCredit, myCreditAccount.ClosingDate, myCreditAccount.UserId);

        creditAccountDTO.CreditCardAccountId = myCreditAccount.AccountId;

        return creditAccountDTO;
    }

    #endregion
}