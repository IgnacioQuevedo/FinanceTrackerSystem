using BusinessLogic;
using BusinessLogic.User_Components;
using BusinessLogic.Category_Components;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;
using BusinessLogic.Goal_Components;
using BusinessLogic.Account_Components;


namespace TestProject1;

[TestClass]
public class GoalTests
{

    private string goalTitle;
    private int maxAmountToSpend;
    private Goal myGoal;
    private User genericUser;
    private Category Food;

    [TestInitialize]

    public void Init()
    {
        myGoal = new Goal();

        goalTitle = "Less night";
        maxAmountToSpend = 0;
        myGoal.Title = goalTitle;

        genericUser = new User("Austin", "Ford", "austinFord@gmail.com", "AustinF2003", "NW 2nd Ave");
        Food = new Category("Food", StatusEnum.Enabled, TypeEnum.Outcome);
        genericUser.AddCategory(Food);

    }

    #region Goal Title
    [TestMethod]

    public void GivenCorrectTitle_ShouldBeSetted()
    {
        Assert.AreEqual(goalTitle, myGoal.Title);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateGoal))]

    public void GivenEmptyTitle_ShouldThrownException()
    {
        goalTitle = "";
        myGoal.Title = goalTitle;
        myGoal.ValidateGoal();
    }

    #endregion

    #region Max Ammount To Spent

    [TestMethod]
    public void GivenAMaxAmmountToSpend_ShouldBeSetted()
    {
        int maxAmmount = 1000;
        myGoal.maxAmountToSpend = maxAmmount;

        Assert.AreEqual(maxAmmount, myGoal.maxAmountToSpend);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateGoal))]
    public void SettingAMaxAmmountToSpendNegative_ShouldThrowException()
    {
        maxAmountToSpend = -999;
        myGoal.maxAmountToSpend = maxAmountToSpend;
        myGoal.ValidateGoal();
    }




    #endregion

    #region Categories Assigned to a Goal
    [TestMethod]

    public void GivenCategories_ShouldBePossibleToAsignThemAGoal()
    {
        List<Category> possibleCategoriesToUse = genericUser.GetCategories();

        myGoal.CategoriesOfGoal.Add(possibleCategoriesToUse[0]);

        Assert.AreEqual(Food, possibleCategoriesToUse[0]);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateGoal))]

    public void GivenNullCategories_ShouldThrowExceptionWhenTryingToApplyToOneOfThem()
    {
        
        List<Category> possibleCategoriesToUse = genericUser.GetCategories();
        genericUser.DeleteCategory(possibleCategoriesToUse[0]);

        myGoal.CategoriesOfGoal = possibleCategoriesToUse;

        myGoal.ValidateGoal();
    }

    #endregion

    [TestMethod]
    public void GivenUyEnum_ShouldBeSetted()
    {

        bool belong = Enum.IsDefined(typeof(CurrencyEnum), myGoal.Currency);
        Assert.IsTrue(belong);

    }

}