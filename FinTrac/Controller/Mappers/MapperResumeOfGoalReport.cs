using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using BusinessLogic.ExchangeHistory_Components;
using BusinessLogic.Report_Components;
using Mappers;

namespace Controller.Mappers;

public abstract class MapperResumeOfGoaLReport
{
    #region To Resume Of Goal Report DTO

    public static ResumeOfGoalReportDTO ToResumeOfGoalReportDTO(ResumeOfGoalReport resumeToConvert)
    {
        ResumeOfGoalReportDTO resumeDTO =
            new ResumeOfGoalReportDTO(resumeToConvert.AmountDefined, resumeToConvert.TotalSpent, resumeToConvert.GoalAchieved);

        return resumeDTO;
    }

    #endregion

}