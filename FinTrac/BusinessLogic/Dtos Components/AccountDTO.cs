namespace BusinessLogic.Dtos_Components;

public class AccountDTO
{
    
    public int AccountId { get; set; }
    public bool isMonetary { get; set; }
    public string Name { get; set; }
    public CurrencyEnumDTO Currency { get; set; }
}