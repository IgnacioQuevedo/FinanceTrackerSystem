using BusinessLogic.Category_Components;

namespace BusinessLogic.Dtos_Components;

public class ResumeOfCategoryReportDTO
{
    public CategoryDTO CategoryRelated { get; set; }
    public decimal TotalSpentInCategory { get; set; }
    public decimal PercentajeOfTotal { get; set; }

    public ResumeOfCategoryReportDTO() { }
    public ResumeOfCategoryReportDTO(CategoryDTO categoryRelated, decimal totalSpent, decimal percentaje)
    {
        throw new NotImplementedException();
    }


}