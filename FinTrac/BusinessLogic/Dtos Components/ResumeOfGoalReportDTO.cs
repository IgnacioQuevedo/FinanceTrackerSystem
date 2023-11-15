namespace BusinessLogic.Dtos_Components;

public class ResumeOfGoalReportDTO
{
    public decimal AmountDefined { get; set; }
    public decimal TotalSpent { get; set; }
    public bool GoalAchieved { get; set; }

    public ResumeOfGoalReportDTO() { }

    public ResumeOfGoalReportDTO(decimal amount, decimal totalSpent, bool goalAchieved)
    {
        AmountDefined = amount;
        TotalSpent = totalSpent;
        GoalAchieved = goalAchieved;
    }
}