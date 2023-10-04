using BusinessLogic;
using BusinessLogic.User_Components;
using BusinessLogic.Category_Components;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;
using BusinessLogic.Goal_Components;
using BusinessLogic.Account_Components;
using Microsoft.VisualBasic;
using System.Timers;
using System.Xml;

namespace TestProject1;

[TestClass]
public class GoalManagementTests
{

    #region Init Section

    private User genericUser;
    private int numberOfGoalsBeforeAdding;

    [TestInitialize]

    public void Init()
    {
        genericUser = new User("Austin", "Ford", "austinFord@gmail.com", "AustinF2003", "NW 2nd Ave");
        numberOfGoalsBeforeAdding = 0;
    }

    #endregion


    #region Add Goal
    [TestMethod]
    public void GivenCorrectGoal_ShouldBePossibleToAddIt()
    {
        Category Food = new Category("Food", StatusEnum.Enabled, TypeEnum.Outcome);
        List<Category> categoriesOfGoal = new List<Category> {Food};

        numberOfGoalsBeforeAdding = genericUser.MyGoals.Count;

        Goal myGoal = new Goal("Less Night",5000, categoriesOfGoal);
        genericUser.AddGoal(myGoal);

        Assert.AreEqual(numberOfGoalsBeforeAdding + 1, genericUser.MyGoals.Count);
    }

    #endregion

    #region Setting An Id

    [TestMethod]
    public void WhenAddingAGoal_ShouldSetAnId()
    {
        Category Food = new Category("Food", StatusEnum.Enabled, TypeEnum.Outcome);
        List<Category> categoriesOfGoal = new List<Category> { Food };
        Goal myGoal = new Goal("Less Night", 5000, categoriesOfGoal);

        genericUser.AddGoal(myGoal);

        Assert.AreEqual(myGoal.GoalId, genericUser.MyGoals.Count);

    }

    #endregion

    #region Return Goals

    [TestMethod]
    public void ShouldBePossibleToReturnList()
    {
        Assert.AreEqual(genericUser.MyGoals, genericUser.GetGoals());
    }
    #endregion

}