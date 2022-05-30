using BusinessObject;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        public void Add(Product Product) => DataAccessLayer.ProductDAO.Instance.AddNew(Product);



        public void Delete(int id)
       => DataAccessLayer.ProductDAO.Instance.Remove(id);

        public IEnumerable<Product> GetAll() => DataAccessLayer.ProductDAO.Instance.ProductList();

        public Product GetProductById(int id) => DataAccessLayer.ProductDAO.Instance.GetProductByID(id);

        public void Update(Product Product) => DataAccessLayer.ProductDAO.Instance.Update(Product);
    }
}
