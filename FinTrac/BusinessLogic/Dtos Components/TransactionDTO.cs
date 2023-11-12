namespace BusinessLogic.Dtos_Components;

public class TransactionDTO
{
    public int TransactionId { get; set; }
    public string Title { get; set; }
    public DateTime CreationDate { get; set; }
    public decimal Amount { get; set; }
    public CurrencyEnumDTO Currency { get; set; }
    public TypeEnumDTO Type { get; set; }
    public CategoryDTO TransactionCategory { get; set; }
    
    public int? AccountId { get; set; }
    
    public TransactionDTO()
    {
        
    }
    
    public TransactionDTO(int transactionId,string title, DateTime creationDate, decimal amount, 
        CurrencyEnumDTO currencyEnumDto, TypeEnumDTO typeEnumDto, CategoryDTO transactionCategory, int? accountId)
    {
        TransactionId = transactionId;
        Title = title;
        CreationDate = creationDate;
        Amount = amount;
        Currency = currencyEnumDto;
        Type = typeEnumDto;
        TransactionCategory = transactionCategory;
        AccountId = accountId;
    }
    
}