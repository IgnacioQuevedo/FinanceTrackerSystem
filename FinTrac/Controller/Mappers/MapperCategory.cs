using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using BusinessLogic.ExchangeHistory_Components;
using Mappers;

namespace Controller.Mappers;

public abstract class MapperCategory
{
    public static CategoryDTO ToCategoryDTO(Category categoryToConvert)
    {
        CategoryDTO categoryDTO =
            new CategoryDTO(categoryToConvert.Name, (StatusEnumDTO)categoryToConvert.Status, (TypeEnumDTO)categoryToConvert.Type,
                categoryToConvert.UserId);

        categoryDTO.CategoryId = categoryToConvert.CategoryId;

        return categoryDTO;
    }

    public static Category ToCategory(CategoryDTO categoryDTO_ToConvert)
    {
        try
        {
            Category categoryConverted =
                new Category(categoryDTO_ToConvert.Name, (StatusEnum)categoryDTO_ToConvert.Status, (TypeEnum)categoryDTO_ToConvert.Type);

            categoryConverted.UserId = categoryDTO_ToConvert.UserId;

            categoryConverted.CategoryId = categoryDTO_ToConvert.CategoryId;
            
            return categoryConverted;
        }
        catch (ExceptionValidateCategory Exception)
        {
            throw new ExceptionMapper(Exception.Message);
        }
    }

    public static List<CategoryDTO> ToListOfCategoryDTO(List<Category> categoryList)
    {
        List<CategoryDTO> listCategoryDTO = new List<CategoryDTO>();

        foreach (Category category in categoryList)
        {
            CategoryDTO categoryDTO = ToCategoryDTO(category);
            listCategoryDTO.Add(categoryDTO);
        }

        return listCategoryDTO;
    }

    public static List<Category> ToListOfCategory(List<CategoryDTO> categoryDtoList)
    {
        List<Category> listOfCategories = new List<Category>();

        foreach (CategoryDTO categoryDTO in categoryDtoList)
        {
            Category category = ToCategory(categoryDTO);
            listOfCategories.Add(category);
        }
        return listOfCategories;
    }

}