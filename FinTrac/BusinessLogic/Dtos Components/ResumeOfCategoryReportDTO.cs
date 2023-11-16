using BusinessLogic.Category_Components;

namespace BusinessLogic.Dtos_Components;

public class ResumeOfCategoryReportDTO
{
    #region Properties

    public CategoryDTO CategoryRelated { get; set; }
    public decimal TotalSpentInCategory { get; set; }
    public decimal PercentajeOfTotal { get; set; }

    #endregion

    #region Constructors
    public ResumeOfCategoryReportDTO() { }
    public ResumeOfCategoryReportDTO(CategoryDTO categoryRelated, decimal totalSpent, decimal percentaje)
    {
        CategoryRelated = categoryRelated;
        TotalSpentInCategory = totalSpent;
        PercentajeOfTotal = percentaje;
    }

    #endregion
}