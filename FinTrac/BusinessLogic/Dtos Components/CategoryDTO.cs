using BusinessLogic.Enums;

namespace BusinessLogic.Dtos_Components
{
	public class CategoryDTO
	{
		public int CategoryId { get; set; }
		public string Name { get; set; } = "";
		public StatusEnum Status { get; set; }
		public TypeEnum Type { get; set; }
		
		public int CategoryUserId { get; set; }


		public CategoryDTO()
		{

		}
		public CategoryDTO(string name, StatusEnum status, TypeEnum type, int categoryUserId)
		{
			Name = name;
			Status = status;
			Type = type;
			CategoryUserId = categoryUserId;
		}
	}
}