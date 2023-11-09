using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Exceptions;
using Mappers;

namespace Controller.Mappers;

public static class MapperCategory
{
    public static CategoryDTO ToCategoryDTO(Category categoryToConvert)
    {
        CategoryDTO categoryDTO =
            new CategoryDTO(categoryToConvert.Name, categoryToConvert.Status, categoryToConvert.Type,categoryToConvert.UserId);
        categoryDTO.Id = categoryToConvert.CategoryId;

        return categoryDTO;
    }

    public static Category ToCategory(CategoryDTO categoryDTO_ToConvert)
    {
        try
        {
            Category categoryConverted =
                new Category(categoryDTO_ToConvert.Name, categoryDTO_ToConvert.Status, categoryDTO_ToConvert.Type);
            categoryConverted.UserId = categoryDTO_ToConvert.CategoryUserId;

            categoryConverted.CategoryId = categoryDTO_ToConvert.Id;

            return categoryConverted;
        }
        catch (ExceptionValidateCategory Exception)
        {
            throw new ExceptionMapper(Exception.Message);
        }
    }
}