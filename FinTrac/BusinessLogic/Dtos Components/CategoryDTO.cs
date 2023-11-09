using BusinessLogic.Enums;

namespace BusinessLogic.Dtos_Components
{
	public class CategoryDTO
	{
		public int Id { get; set; }
		public string Name { get; set; } = "";
		public StatusEnum Status { get; set; }
		public TypeEnum Type { get; set; }
		


		public CategoryDTO()
		{

		}
		public CategoryDTO(string name, StatusEnum status, TypeEnum type)
		{
			Name = name;
			Status = status;
			Type = type;
		}
	}
}