using BusinessLogic;
using BusinessLogic.User_Components;
using BusinessLogic.Category_Components;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;
using BusinessLogic.Goal_Components;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;

namespace TestProject1;

[TestClass]
public class GoalTests
{

    #region Init Section

    private string goalTitle;
    private int maxAmountToSpend;
    private Goal myGoal;
    private User genericUser;
    private Category Food;
    private List<Category> categoriesAsignedToGoal;

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

        categoriesAsignedToGoal = new List<Category>();

    }

    #endregion

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
        myGoal.MaxAmountToSpend = maxAmmount;

        Assert.AreEqual(maxAmmount, myGoal.MaxAmountToSpend);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateGoal))]
    public void SettingAMaxAmmountToSpendNegative_ShouldThrowException()
    {
        maxAmountToSpend = -999;
        myGoal.MaxAmountToSpend = maxAmountToSpend;
        myGoal.ValidateGoal();
    }




    #endregion

    #region Categories Assigned to a Goal
    [TestMethod]

    public void GivenCategories_ShouldBePossibleToAsignThemAGoal()
    {
        myGoal.CategoriesOfGoal = categoriesAsignedToGoal;
        categoriesAsignedToGoal.Add(Food);

        Assert.AreEqual(Food, categoriesAsignedToGoal[0]);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateGoal))]

    public void GivenNullCategories_ShouldThrowExceptionWhenTryingToApplyToOneOfThem()
    {

        List<Category> possibleCategoriesToUse = null;

        Goal myGoal = new Goal("Less night", 500, possibleCategoriesToUse);
        myGoal.ValidateGoal();
    }

    #endregion

    #region Currency Of Ammount
    [TestMethod]
    public void GivenUyEnum_ShouldBeSetted()
    {
        bool belongToEnum = Enum.IsDefined(typeof(CurrencyEnum), myGoal.CurrencyOfAmount);
        Assert.IsTrue(belongToEnum);
    }

    #endregion

    #region Creation of Goal

    [TestMethod]
    public void GivenValuesToCreateGoal_ShouldBeCreated()
    {
        string title = "Outcomes";
        int maxAmmount = 1000;
        categoriesAsignedToGoal.Add(Food);

        Goal goalExample = new Goal(title, maxAmmount, categoriesAsignedToGoal);


        Assert.AreEqual(goalExample.Title, title);
        Assert.AreEqual(goalExample.MaxAmountToSpend, maxAmmount);
    }

    [TestMethod]
    [ExpectedException(typeof(ExceptionValidateGoal))]

    public void GivenIncorrectValuesToCreate_ShouldThrowException()
    {

        string title = "Less hang outs";
        int maxAmmount = -5;
        categoriesAsignedToGoal.Add(Food);

        Goal goalExample = new Goal(title, maxAmmount, categoriesAsignedToGoal);

    }

    #endregion
}