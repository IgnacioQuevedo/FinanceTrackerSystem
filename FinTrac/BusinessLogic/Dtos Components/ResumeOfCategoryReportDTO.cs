using BusinessLogic.Category_Components;

namespace BusinessLogic.Dtos_Components;

public class ResumeOfCategoryReportDTO
{
    public CategoryDTO CategoryRelated { get; set; }
    public decimal TotalSpentInCategory { get; set; }

    public ResumeOfCategoryReportDTO() { }
}