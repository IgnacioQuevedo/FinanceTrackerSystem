using BusinessLogic;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;
using BusinessLogic.Account_Components;
using BusinessLogic.Transaction_Components;
using BusinessLogic.Category_Components;
using System.Net.Security;
using BusinessLogic.User_Components;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;

namespace TestProject1;

[TestClass]
public class CategoryTests
{
    #region initializingAspects
    private Category genericCategory;
    private User genericUser;

    [TestInitialize]
    public void TestInitialize()
    {
        string name = "Outcomes";
        StatusEnum status = (StatusEnum)1;
        TypeEnum type = TypeEnum.Income;
        genericCategory = new Category(name, status, type);

        string firstName = "Austin";
        string lastName = "Ford";
        string email = "austinFord@gmail.com";
        string password = "AustinF2003";
        string address = "NW 2nd Ave";
        genericUser = new User(firstName, lastName, email, password, address);
    }
    #endregion

    #region Validate Category

    [TestMethod]
    public void GivenCorrectName_ShouldReturnTrue()
    {
        Assert.AreEqual(true, genericCategory.ValidateCategory());
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateCategory))]
    public void GivenEmptyName_ShouldThrowException()
    {
        Category myCategory = new Category();
        myCategory.Name = "";
        myCategory.ValidateCategory();
    }

    [TestMethod]
    public void GivenCategory_ShouldReturnDate()
    {
        DateTime dateNow = DateTime.Now.Date;
        Assert.AreEqual(dateNow, genericCategory.CreationDate);
    }

    [TestMethod]
    public void GivenStatus_ShouldBelongToStatusEnum()
    {
        bool belongsToEnum = Enum.IsDefined(typeof(StatusEnum), genericCategory.Status);
        Assert.IsTrue(belongsToEnum);
    }

    [TestMethod]
    public void GivenType_ShouldBelongToTypeEnum()
    {
        bool belongsToEnum = Enum.IsDefined(typeof(TypeEnum), genericCategory.Type);
        Assert.IsTrue(belongsToEnum);
    }

    [TestMethod]
    public void GivenCorrectValuesToCreateCategory_ShouldBeEqualToProperties()
    {
        string categoryName = "Food";
        StatusEnum categoryStatus = (StatusEnum)1;
        TypeEnum categoryType = (TypeEnum)1;
        Category myCategory = new Category(categoryName, categoryStatus, categoryType);

        Assert.AreEqual(categoryName, myCategory.Name);
        Assert.AreEqual(categoryStatus, myCategory.Status);
        Assert.AreEqual(categoryType, myCategory.Type);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateCategory))]
    public void GivenWrongValueToCreateCategory_ShouldThrowException()
    {
        string categoryName = "";
        StatusEnum categoryStatus = (StatusEnum)1;
        TypeEnum categoryType = (TypeEnum)1;
        Category myCategory = new Category(categoryName, categoryStatus, categoryType);
    }

    #endregion

    #region Category Management

    [TestMethod]
    public void GivenCorrectCategoryToAdd_ShouldAddCategory()
    {
        int numberOfCategoriesAddedBefore = genericUser.MyCategories.Count;
        genericUser.AddCategory(genericCategory);
        Assert.AreEqual(numberOfCategoriesAddedBefore + 1, genericUser.MyCategories.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionCategoryManagement))]
    public void GivenAlreadyRegisteredCategoryToAdd_ShouldThrowException()
    {
        genericUser.AddCategory(genericCategory);
        genericUser.AddCategory(genericCategory);
    }

    [TestMethod]
    public void GivenCategoryToAdd_ShouldAssignId()
    {
        genericUser.AddCategory(genericCategory);
        Assert.AreEqual(genericUser.MyCategories.Count - 1, genericCategory.CategoryId);
    }

    [TestMethod]
    public void GivenNothing_ShouldReturnList()
    {
        Assert.AreEqual(genericUser.MyCategories, genericUser.GetCategories());
    }

    [TestMethod]
    public void GivenCategoryToUpdate_ShouldBeModifiedCorrectly()
    {
        genericUser.AddCategory(genericCategory);

        string nameOfSecondCategory = "Fooding";
        StatusEnum statusOfSecondCategory = StatusEnum.Enabled;
        TypeEnum typeOfSecondCategory = TypeEnum.Income;
        Category anotherCategory = new Category(nameOfSecondCategory, statusOfSecondCategory, typeOfSecondCategory);

        anotherCategory.CategoryId = genericCategory.CategoryId;
        genericUser.ModifyCategory(anotherCategory);
        int indexOfCategoryToUpdate = genericCategory.CategoryId;
        
        Assert.AreEqual(anotherCategory.CategoryId, genericUser.MyCategories[indexOfCategoryToUpdate].CategoryId);
        Assert.AreEqual(anotherCategory.Name, genericUser.MyCategories[indexOfCategoryToUpdate].Name);
        Assert.AreEqual(anotherCategory.Status, genericUser.MyCategories[indexOfCategoryToUpdate].Status);
        Assert.AreEqual(anotherCategory.Type, genericUser.MyCategories[indexOfCategoryToUpdate].Type);
    }

    [TestMethod]
    public void GivenCategoryToDelete_ShouldDelete()
    {
        genericUser.AddCategory(genericCategory);
        int idOfCategoryDeleted = genericCategory.CategoryId;
        genericUser.DeleteCategory(genericCategory);

        Assert.IsNull(genericUser.MyCategories[idOfCategoryDeleted]);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionCategoryManagement))]
    public void GivenCategoryToDeleteThatHasAssociatedTransactions_ShoulThrowException()
    {
        MonetaryAccount myMonetaryAccount = new MonetaryAccount("Saving Bank", 1000, CurrencyEnum.UY, DateTime.Now);
        Transaction myTransaction = new Transaction("Payments", 10000, DateTime.Now, CurrencyEnum.UY, TypeEnum.Income, genericCategory);

        genericUser.AddMonetaryAccount(myMonetaryAccount);
        genericUser.MyAccounts[0].AddTransaction(myTransaction);
        genericUser.DeleteCategory(genericCategory);

    }


    #endregion
}