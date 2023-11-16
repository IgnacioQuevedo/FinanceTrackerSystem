namespace BusinessLogic.Dtos_Components;

public class ResumeOfGoalReportDTO
{
    #region Properties

    public decimal AmountDefined { get; set; }
    public decimal TotalSpent { get; set; }
    public bool GoalAchieved { get; set; }
    public string GoalName { get; set; }

    #endregion

    #region Constructors

    public ResumeOfGoalReportDTO() { }

    public ResumeOfGoalReportDTO(decimal amount, decimal totalSpent, bool goalAchieved)
    {
        AmountDefined = amount;
        TotalSpent = totalSpent;
        GoalAchieved = goalAchieved;
    }

    #endregion
}