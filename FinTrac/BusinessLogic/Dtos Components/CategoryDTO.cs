
namespace BusinessLogic.Dtos_Components
{
	public class CategoryDTO
	{
		public int CategoryId { get; set; }
		public string Name { get; set; } = "";
		public StatusEnumDTO Status { get; set; }
		public TypeEnumDTO Type { get; set; }
		
		public DateTime CreationDate { get; set; } = DateTime.Now.Date;
		public int UserId { get; set; }
		


		public CategoryDTO()
		{

		}
		public CategoryDTO(string name, StatusEnumDTO status, TypeEnumDTO type, int userId)
		{
			Name = name;
			Status = status;
			Type = type;
			UserId = userId;
		}
	}
}