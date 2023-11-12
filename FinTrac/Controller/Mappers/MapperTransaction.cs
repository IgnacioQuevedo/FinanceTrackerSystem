using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using BusinessLogic.Transaction_Components;
using Mappers;

namespace Controller.Mappers;

public abstract class MapperTransaction
{
    #region ToTransaction

    public static Transaction ToTransaction(TransactionDTO transactionDto)
    {
        try
        {
            Transaction transactionToTransform =
                new Transaction(transactionDto.Title, transactionDto.Amount, transactionDto.CreationDate,
                    (CurrencyEnum)transactionDto.Currency, (TypeEnum)transactionDto.Type,
                    MapperCategory.ToCategory(transactionDto.TransactionCategory));

            transactionToTransform.TransactionId = transactionDto.TransactionId;
            transactionToTransform.AccountId = transactionDto.AccountId;

            return transactionToTransform;
        }
        catch (ExceptionValidateTransaction Exception)
        {
            throw new ExceptionMapper(Exception.Message);
        }
    }

    #endregion
}