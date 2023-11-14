namespace BusinessLogic.Dtos_Components;

public class AccountDTO 
{
    public int AccountId { get; set; }
    public string Name { get; set; }
    public CurrencyEnumDTO Currency { get; set; }
    public DateTime CreationDate { get; set; }
    public int? UserId { get; set; }


    public AccountDTO()
    {
        
    }
    public AccountDTO(string name, CurrencyEnumDTO currency, DateTime creationDate, int? userId)
    {
        Name = name;
        Currency = currency;
        CreationDate = creationDate;
        UserId = userId;

    }
}