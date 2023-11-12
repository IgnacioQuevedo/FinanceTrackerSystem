namespace BusinessLogic.Dtos_Components;

public class TransactionDTO
{
    public TransactionDTO(int transactionId, string title, DateTime creationDate, decimal amount, CurrencyEnumDTO currencyEnumDto, TypeEnumDTO typeEnumDto, CategoryDTO transactionCategory)
    {
        throw new NotImplementedException();
    }

    public int TransactionId { get; set; }
    public string Title { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now.Date;
    public decimal Amount { get; set; }
    public CurrencyEnumDTO Currency { get; set; }
    public TypeEnumDTO Type { get; set; }
    public CategoryDTO TransactionCategory { get; set; }
    
}