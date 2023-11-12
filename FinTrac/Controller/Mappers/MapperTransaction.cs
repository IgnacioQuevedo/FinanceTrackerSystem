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

    #region ToTransactionDTO

    public static TransactionDTO ToTransactionDTO(Transaction transactionToConvert)
    {
        TransactionDTO transactionDTO =
            new TransactionDTO(transactionToConvert.Title, transactionToConvert.CreationDate,
                transactionToConvert.Amount
                , (CurrencyEnumDTO)transactionToConvert.Currency, (TypeEnumDTO)transactionToConvert.Type,
                MapperCategory.ToCategoryDTO(transactionToConvert.TransactionCategory), transactionToConvert.AccountId);

        transactionDTO.TransactionId = transactionToConvert.TransactionId;

        return transactionDTO;
    }

    #endregion

    #region To list of transactionsDTO

    public static List<TransactionDTO> ToListOfTransactionsDTO(List<Transaction> transactions)
    {
        List<TransactionDTO> listTransactionsDTO = new List<TransactionDTO>();

        foreach (Transaction transaction in transactions)
        {
            TransactionDTO transactionDTO = ToTransactionDTO(transaction);
            listTransactionsDTO.Add(transactionDTO);
        }

        return listTransactionsDTO;
    }

    #endregion
    
    #region To list of Transactions
    public static List<Transaction> ToListOfTransactions(List<TransactionDTO> transactionsDTO)
    {
        List<Transaction> transactions = new List<Transaction>();

        foreach (TransactionDTO transactionDTO in transactionsDTO)
        {
            Transaction transaction = MapperTransaction.ToTransaction(transactionDTO);
            transactions.Add(transaction);
        }

        return transactions;
    }

    #endregion
}