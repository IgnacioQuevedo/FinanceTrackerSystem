using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Category_Components;
using BusinessLogic.Enums;
using BusinessLogic.Goal_Components;

namespace BusinessLogicTests.Dtos_Tests
{
    [TestClass]
    public class GoalDTO_Tests
    {
        #region Initialize

        private GoalDTO _goalDTO;
        private CategoryDTO genericCategoryDTO;
        private List<CategoryDTO> genericListOfCategories;
        private CurrencyEnum genericCurrencyEnum;
        private int genericMaxAmountOfGoal;
        private string genericTitleGoalDTO;
        private int genericUserId;

        [TestInitialize]
        public void Initialize()
        {
            _goalDTO = new GoalDTO();

            genericCategoryDTO = new CategoryDTO("Food", StatusEnum.Enabled, TypeEnum.Outcome, 1);

            List<CategoryDTO> myListOfCategories = new List<CategoryDTO>();
            myListOfCategories.Add(genericCategoryDTO);

            genericCurrencyEnum = CurrencyEnum.USA;

            genericMaxAmountOfGoal = 100;

            genericTitleGoalDTO = "Less Party";

            genericUserId = 1;
        }

        #endregion

        #region Title

        [TestMethod]
        public void GivenTitle_ShouldBeSetted()
        {
            _goalDTO.Title = genericTitleGoalDTO;

            Assert.AreEqual(genericTitleGoalDTO, _goalDTO.Title);
        }

        #endregion

        #region Max amount to spend
        [TestMethod]
        public void GivenMaxAmountToSpend_ShouldBeSetted()
        {
            _goalDTO.MaxAmountToSpend = genericMaxAmountOfGoal;

            Assert.AreEqual(genericMaxAmountOfGoal, _goalDTO.MaxAmountToSpend);
        }

        #endregion

        #region Currency of amount

        [TestMethod]
        public void GivenCurrencyOfAmount_ShoulBeSetted()
        {
            _goalDTO.CurrencyOfAmount = genericCurrencyEnum;

            bool belongToEnum = Enum.IsDefined(typeof(CurrencyEnum), _goalDTO.CurrencyOfAmount);
            Assert.IsTrue(belongToEnum);
        }

        #endregion

        #region List Of Categories

        [TestMethod]
        public void GivenListOfCategories_ShoulBeSetted()
        {
            _goalDTO.CategoriesOfGoalDTO = genericListOfCategories;

            Assert.AreEqual(genericListOfCategories, _goalDTO.CategoriesOfGoalDTO);
        }

        #endregion


        [TestMethod]
        public void GivenGoalId_ShouldBeSetted()
        {
            _goalDTO.GoalId = 1;
            Assert.AreEqual(1, _goalDTO.GoalId);
        }

        #region User Id

        [TestMethod]
        public void GivenUserId_ShouldBeSetted()
        {
            _goalDTO.UserId = genericUserId;

            Assert.AreEqual(genericUserId, _goalDTO.UserId);
        }

        #endregion

        #region Constructor

        [TestMethod]
        public void GivenValues_ShouldBePossibleToCreateAGoalDTO()
        {
            GoalDTO myGoalDTO = new GoalDTO(genericTitleGoalDTO, genericMaxAmountOfGoal, genericCurrencyEnum, genericListOfCategories, genericUserId);

            Assert.AreEqual(genericTitleGoalDTO, myGoalDTO.Title);
            Assert.AreEqual(genericMaxAmountOfGoal, myGoalDTO.MaxAmountToSpend);
            Assert.AreEqual(genericCurrencyEnum, myGoalDTO.CurrencyOfAmount);
            Assert.AreEqual(genericListOfCategories, myGoalDTO.CategoriesOfGoalDTO);
            Assert.AreEqual(genericUserId, myGoalDTO.UserId);
        }

        #endregion


    }
}
