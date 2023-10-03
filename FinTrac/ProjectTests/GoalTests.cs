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

    [TestMethod]

    public void GivenCorrectName_ShouldBeSetted() 
    { 
        goalTitle = "Less night";
        myGoal.Title = goalTitle;

        Assert.AreEqual(goalTitle, myGoal.Title);
    }



}