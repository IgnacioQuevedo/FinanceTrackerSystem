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
            new CategoryDTO(categoryToConvert.Name, categoryToConvert.Status, categoryToConvert.Type,
                categoryToConvert.UserId);
        categoryDTO.CategoryId = categoryToConvert.CategoryId;

        return categoryDTO;
    }

    public static Category ToCategory(CategoryDTO categoryDTO_ToConvert)
    {
        try
        {
            Category categoryConverted =
                new Category(categoryDTO_ToConvert.Name, categoryDTO_ToConvert.Status, categoryDTO_ToConvert.Type);

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
            CategoryDTO categoryDTO = MapperCategory.ToCategoryDTO(category);
            listCategoryDTO.Add(categoryDTO);
        }

        return listCategoryDTO;
    }

    public static List<Category> ToListOfCategory(List<CategoryDTO> categoryDtoList)
    {
        List<Category> listOfCategories = new List<Category>();

        foreach (CategoryDTO categoryDTO in categoryDtoList)
        {
            Category category = MapperCategory.ToCategory(categoryDTO);
            listOfCategories.Add(category);
        }
        return listOfCategories;
    }
    
    
    
    
}