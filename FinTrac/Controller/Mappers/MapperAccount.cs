using BusinessLogic.Account_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using BusinessLogic.ExchangeHistory_Components;
using BusinessLogic.User_Components;

using Mappers;

namespace Controller.Mappers;

public abstract class MapperAccount
{
    #region To List Account
    public static List<Account> ToListAccount(List<AccountDTO> myAccountsDTO)
    {
        List<Account> myAccounts = new List<Account>();
        MonetaryAccountDTO possibleMonetAccount = new MonetaryAccountDTO();
        CreditCardAccountDTO possibleCredAccount = new CreditCardAccountDTO();

        foreach (AccountDTO accountDTO in myAccountsDTO)
        {
            if (accountDTO is MonetaryAccountDTO)
            {
                possibleMonetAccount = accountDTO as MonetaryAccountDTO;
                myAccounts.Add(MapperMonetaryAccount.ToMonetaryAccount(possibleMonetAccount));
            }
            else
            {
                possibleCredAccount = accountDTO as CreditCardAccountDTO;
                myAccounts.Add(MapperCreditAccount.ToCreditAccount(possibleCredAccount));
            }
        }

        return myAccounts;
    }

    #endregion

    #region To List Account DTO

    public static List<AccountDTO> ToListAccountDTO(List<Account> myAccounts)
    {
        List<AccountDTO> myAccountsDTO = new List<AccountDTO>();
        MonetaryAccount possibleMonetAccount = new MonetaryAccount();
        CreditCardAccount possibleCredAccount = new CreditCardAccount();

        foreach (Account account in myAccounts)
        {
            if (account is MonetaryAccount)
            {
                possibleMonetAccount = account as MonetaryAccount;
                myAccountsDTO.Add(MapperMonetaryAccount.ToMonetaryAccountDTO(possibleMonetAccount));
            }
            else
            {
                possibleCredAccount = account as CreditCardAccount;
                myAccountsDTO.Add(MapperCreditAccount.ToCreditAccountDTO(possibleCredAccount));
            }
        }

        return myAccountsDTO;
    }

    #endregion

	#region ToUser

	public static User ToAccount(UserDTO userDto)
	{
		try
		{
			User userConverted = new User(userDto.FirstName, userDto.LastName, userDto.Email, userDto.Password,
				userDto.Address);

			userConverted.UserId = userDto.UserId;
			FormatUserProperties(userConverted);
			return userConverted;
		}
		catch (ExceptionValidateUser Exception)
		{
			throw new ExceptionMapper(Exception.Message);
		}
	}

	#endregion

	#region ToUserDTO

	public static UserDTO ToUserDTO(User userToConvert)
	{
		UserDTO dtoOfUser = new UserDTO(userToConvert.FirstName, userToConvert.LastName,
			userToConvert.Email, userToConvert.Password, userToConvert.Address);
		dtoOfUser.UserId = userToConvert.UserId;

		return dtoOfUser;
	}

	#endregion


	#region AuxiliaryMethods

	private static void FormatUserProperties(User user)
	{
		user.Email = user.Email.ToLower();
		user.FirstName = char.ToUpper(user.FirstName[0]) + user.FirstName.Substring(1).ToLower();
		user.LastName = char.ToUpper(user.LastName[0]) + user.LastName.Substring(1).ToLower();
	}

	#endregion
}