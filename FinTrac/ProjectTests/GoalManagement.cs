using BusinessLogic;
using BusinessLogic.User_Components;
using BusinessLogic.Category_Components;
using NuGet.Frameworks;
using System.Runtime.ExceptionServices;
using BusinessLogic.Goal_Components;
using BusinessLogic.Account_Components;
using Microsoft.VisualBasic;

namespace TestProject1;

[TestClass]
public class GoalManagementTests
{

    #region Init Section

    private User genericUser;


    [TestInitialize]

    public void Init()
    {
        genericUser = new User("Austin", "Ford", "austinFord@gmail.com", "AustinF2003", "NW 2nd Ave");

    }

    #endregion

    [TestMethod]

    public void GivenCorrectGoal_ShouldBePossibleToAddIt()
    {

        Category Food = new Category("Food", StatusEnum.Enabled, TypeEnum.Outcome);
        List<Category> categoriesOfGoal = new List<Category>();
        categoriesOfGoal.Add(Food); 

        Goal myGoal = new Goal("Less Night",5000, categoriesOfGoal);
        genericUser.AddGoal(myGoal);


    }
}