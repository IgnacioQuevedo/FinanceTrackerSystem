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

        creditAccountDTO.AccountId = myCreditAccount.AccountId;

        return creditAccountDTO;
    }

    #endregion

    #region ToListOfCreditAccountDTO

    public static List<CreditCardAccountDTO> ToListOfCreditAccountDTO(List<CreditCardAccount> myListOfCreditAccount)
    {
        List<CreditCardAccountDTO> listCreditAccountDTO = new List<CreditCardAccountDTO>();

        foreach (CreditCardAccount creditAccount in myListOfCreditAccount)
        {
            CreditCardAccountDTO monetAccountDTO = ToCreditAccountDTO(creditAccount);
            listCreditAccountDTO.Add(monetAccountDTO);
        }

        return listCreditAccountDTO;
    }

    #endregion

    #region To CreditAccount

    public static CreditCardAccount ToCreditAccount(CreditCardAccountDTO myCreditAccountDTO)
    {
        try
        {
            CreditCardAccount creditAccount =
              new CreditCardAccount(myCreditAccountDTO.Name, (CurrencyEnum)myCreditAccountDTO.Currency, 
                  myCreditAccountDTO.CreationDate, myCreditAccountDTO.IssuingBank, myCreditAccountDTO.Last4Digits, myCreditAccountDTO.AvailableCredit, myCreditAccountDTO.ClosingDate);

            creditAccount.AccountId = myCreditAccountDTO.AccountId;
            creditAccount.UserId = myCreditAccountDTO.UserId;

            return creditAccount;
        }
        catch (ExceptionValidateAccount Exception)
        {
            throw new ExceptionMapper(Exception.Message);
        }
    }

    #endregion
}