using BusinessLogic;
using BusinessLogic.User_Components;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;
using BusinessLogic.Goal_Components;

namespace TestProject1;

[TestClass]
public class GoalTests
{

    private string goalTitle;
    private Goal myGoal;

    [TestInitialize]

    public void Init()
    {
        myGoal = new Goal();
        goalTitle = string.Empty;
    }

    #region Goal Title
    [TestMethod]

    public void GivenCorrectTitle_ShouldBeSetted() 
    { 
        goalTitle = "Less night";
        myGoal.Title = goalTitle;

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
}