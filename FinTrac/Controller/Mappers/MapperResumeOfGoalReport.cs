using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using BusinessLogic.ExchangeHistory_Components;
using BusinessLogic.Report_Components;
using Mappers;

namespace Controller.Mappers;

public abstract class MapperResumeOfGoalReport
{
    #region To Resume Of Goal Report DTO

    public static ResumeOfGoalReportDTO ToResumeOfGoalReportDTO(ResumeOfGoalReport resumeToConvert)
    {
        ResumeOfGoalReportDTO resumeDTO =
            new ResumeOfGoalReportDTO(resumeToConvert.AmountDefined, resumeToConvert.TotalSpent, resumeToConvert.GoalAchieved);

        return resumeDTO;
    }

    #endregion

    #region To resume of goal report

    public static ResumeOfGoalReport ToResumeOfGoalReport(ResumeOfGoalReportDTO resumeDTO_ToConvert)
    {
        ResumeOfGoalReport resume = new ResumeOfGoalReport(resumeDTO_ToConvert.AmountDefined, resumeDTO_ToConvert.TotalSpent, resumeDTO_ToConvert.GoalAchieved);

        return resume;
    }

    #endregion
}