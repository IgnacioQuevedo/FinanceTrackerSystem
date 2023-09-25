using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Category
{
    public class Category
    {
        public string Name { get; set; } = "";
        public string CreationDate { get; } = DateTime.Now.ToString("dd/MM/yyyy");

        public StatusEnum Status { get; set; }
        public TypeEnum Type { get; set; }


        public Category()
        {
        }

        public Category(string name, StatusEnum status, TypeEnum type)
        {
        }

        public bool ValidateCategory()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(Name))
            {
                throw new ExceptionValidateCategory("ERROR ON NAME");
            }
            return isValid;
        }
    }
}
