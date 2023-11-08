using BusinessLogic.Category_Components;
using BusinessLogic.Dto_Components;
using BusinessLogic.Exceptions;
using Mappers;

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

    public static Category ToCategory(CategoryDTO categoryDTO_ToConvert)
    {
        try
        {
            Category categoryConverted =
                new Category(categoryDTO_ToConvert.Name, categoryDTO_ToConvert.Status, categoryDTO_ToConvert.Type);

            categoryConverted.CategoryId = categoryDTO_ToConvert.Id;

            return categoryConverted;
        }
        catch (ExceptionValidateCategory Exception)
        {
            throw new ExceptionMapper(Exception.Message);

        }
    }
}