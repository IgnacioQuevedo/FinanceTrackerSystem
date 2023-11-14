namespace BusinessLogic.Dtos_Components;

public class AccountDTO 
{
    public int AccountId { get; set; }
    public bool isMonetary { get; set; }
    public string Name { get; set; }
    public CurrencyEnumDTO Currency { get; set; }
    public DateTime CreationDate { get; set; }
    public int UserId { get; set; }


    public AccountDTO()
    {
        
    }
    public AccountDTO(int accountId, bool b, string name, CurrencyEnumDTO currency, DateTime creationDate, int userId)
    {
        throw new NotImplementedException();
    }
}