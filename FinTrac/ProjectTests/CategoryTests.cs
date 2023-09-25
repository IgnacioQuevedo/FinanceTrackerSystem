using BusinessLogic;
using BusinessLogic.User;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;

using BusinessLogic.Category;
namespace TestProject1;

[TestClass]
public class CategoryTests
{
    [TestMethod]
    public void GivenACorrectName_ShouldReturnTrue()
    {
        Category myCategory = new Category();
        myCategory.Name = "Clothes";
        Assert.AreEqual(true, Category.ValidateName());

    }
}
