using System.Diagnostics.CodeAnalysis;
using BusinessLogic.Category_Components;
using BusinessLogic.Dtos_Components;
using BusinessLogic.Enums;
using BusinessLogic.Report_Components;
using BusinessLogic.User_Components;
using Controller;
using DataManagers;
using Controller.Mappers;
using Mappers;

namespace ControllerTests
{
    [TestClass]
    public class MapperMovementInXDaysTests
    {
        #region Initialize

        private GenericController _controller;
        private SqlContext _testDb;
        private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();

        private UserRepositorySql _userRepo;


        [TestInitialize]
        public void Initialize()
        {
            _testDb = _contextFactory.CreateDbContext();
            _userRepo = new UserRepositorySql(_testDb);
            _controller = new GenericController(_userRepo);
            
        }

        #endregion

        #region Cleanup

        [TestCleanup]
        public void CleanUp()
        {
            _testDb.Database.EnsureDeleted();
        }

        #endregion


        [TestMethod]
        public void GivenMovementInXDaysDTO_ShouldBePossibleToConvertItToMovementInXDays()
        {
            RangeOfDatesDTO rangeOfDatesDto = 
                new RangeOfDatesDTO(new DateTime(2023, 12, 1), new DateTime(2023, 12, 31));
            
            MovementInXDaysDTO movementsDTO = new MovementInXDaysDTO(rangeOfDatesDto);

            MovementInXDays movements = MapperMovementInXDays.ToMovement(movementsDTO);

        }
      
    }
}