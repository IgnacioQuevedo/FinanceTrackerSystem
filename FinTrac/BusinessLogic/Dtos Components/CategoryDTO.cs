using BusinessLogic.Enums;

namespace BusinessLogic.Dto_Components;

public class CategoryDTO
{
    public string Name { get; set; } = "";
    public StatusEnum Status { get; set; }
    public TypeEnum Type { get; set; }
    
}