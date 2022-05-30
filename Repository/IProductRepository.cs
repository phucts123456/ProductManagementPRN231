using BusinessObject;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        void Add(Product Product);
        void Update(Product Product);
        void Delete(int id);
        Product GetProductById(int id);
    }
}
