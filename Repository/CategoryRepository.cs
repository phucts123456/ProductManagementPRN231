using BusinessObject;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public void Add(Category Category) => DataAccessLayer.CategoryDAO.Instance.AddNew(Category);

       

        public void Delete(int id)
       => DataAccessLayer.CategoryDAO.Instance.Remove(id);

        public IEnumerable<Category> GetAll() => DataAccessLayer.CategoryDAO.Instance.CategoryList();

        public Category GetCategoryById(int id) => DataAccessLayer.CategoryDAO.Instance.GetCategoryByID(id);

        public void Update(Category Category) => DataAccessLayer.CategoryDAO.Instance.Update(Category);
    }
}
