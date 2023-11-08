using BusinessLogic.Category_Components;
using BusinessLogic.Dto_Components;

namespace Controller.Mappers;

public abstract class MapperCategory
{

    public static CategoryDTO ToCategoryDTO(Category categoryToConvert)
    {
        CategoryDTO categoryDTO =
            new CategoryDTO(categoryToConvert.Name, categoryToConvert.Status, categoryToConvert.Type);
        categoryDTO.Id = categoryToConvert.CategoryId;
        
        return categoryDTO;
    }
    
    
    
}