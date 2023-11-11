using BusinessLogic.Dtos_Components;
using BusinessLogic.User_Components;
using Controller;
using DataManagers;
using Controller.Mappers;
using Mappers;

namespace ControllerTests
{
	[TestClass]
	public class ExchangeHistoryMapper
	{
		#region Initialize

		private GenericController _controller;
		private SqlContext _testDb;
		private readonly IAppContextFactory _contextFactory = new InMemoryAppContextFactory();

		private UserRepositorySql _userRepo;
		private UserDTO _userDTO;
		private UserDTO _userConnected;

		[TestInitialize]
		public void Initialize()
		{
			_testDb = _contextFactory.CreateDbContext();
			_userRepo = new UserRepositorySql(_testDb);
			_controller = new GenericController(_userRepo);
			
			_userConnected = new UserDTO("Jhon", "Sans", "jhonnie@gmail.com", "Jhoooniee123!", "");
			_userConnected.UserId = 1;
			
			_controller.RegisterUser(_userConnected);
			_controller.SetUserConnected(_userConnected.UserId);
		}

		#endregion

		#region Cleanup

		[TestCleanup]
		public void CleanUp()
		{
			_testDb.Database.EnsureDeleted();
		}

		#endregion

		
		
		
		

	}
}