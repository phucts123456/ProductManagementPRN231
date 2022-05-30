using BusinessObject;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        void Add(Category Category);
        void Update(Category Category);
        void Delete(int id);
        Category GetCategoryById(int id);
    }
}
