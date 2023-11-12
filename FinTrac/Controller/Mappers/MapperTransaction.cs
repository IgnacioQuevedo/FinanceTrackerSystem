using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.Transaction_Components;

namespace Controller.Mappers;

public abstract class MapperTransaction
{
    public static Transaction ToTransaction(TransactionDTO transactionDto)
    {
        Transaction transactionToTransform =
            new Transaction(transactionDto.Title, transactionDto.Amount, transactionDto.CreationDate,
                (CurrencyEnum)transactionDto.Currency, (TypeEnum)transactionDto.Type,
                MapperCategory.ToCategory(transactionDto.TransactionCategory));
        
        transactionToTransform.TransactionId = transactionDto.TransactionId;
        transactionToTransform.AccountId = transactionDto.AccountId;

        return transactionToTransform;
    }
}