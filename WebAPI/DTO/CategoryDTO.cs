using BusinessObject;
using System.Collections.Generic;

namespace WebAPI.DTO
{
    public class CategoryDTO
    {
        
            public CategoryDTO()
            {
                Products = new HashSet<Product>();
            }

            public int CategoryId { get; set; }
            public string CategoryName { get; set; }

            public virtual ICollection<Product> Products { get; set; }
        
    }
}
