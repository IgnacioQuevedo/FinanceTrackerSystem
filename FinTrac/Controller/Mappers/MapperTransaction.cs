

using BusinessLogic.Dtos_Components;
using BusinessLogic.Transaction_Components;

namespace Controller.Mappers;

public abstract class MapperTransaction
{
    public static Transaction ToTransaction(TransactionDTO transactionDto)
    {
        throw new NotImplementedException();
    }
}