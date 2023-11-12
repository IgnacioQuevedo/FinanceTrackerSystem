namespace BusinessLogic.Dtos_Components;

public class TransactionDTO
{
    public string Title { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now.Date;
    public int Amount { get; set; }
}