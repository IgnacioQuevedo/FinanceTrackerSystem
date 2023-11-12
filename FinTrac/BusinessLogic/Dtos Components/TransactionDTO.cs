namespace BusinessLogic.Dtos_Components;

public class TransactionDTO
{
    public string Title { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now.Date;
    public decimal Amount { get; set; }
    public CurrencyEnumDTO Currency { get; set; }
    public TypeEnumDTO Type { get; set; }
}