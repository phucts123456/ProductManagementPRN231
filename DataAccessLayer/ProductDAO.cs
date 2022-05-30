using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace DataAccessLayer
{
    public class ProductDAO
    {

        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }


            }
        }
        public IEnumerable<Product> ProductList()
        {
            List<Product> Products;
            try
            {
                var DBcontext = new MyStorePRN231Context();
                Products = DBcontext.Products.Include(p=> p.Category).ToList();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return Products;
        }
        public void AddNew(Product Product)
        {
            try
            {

                Product Cate = GetProductByID(Product.ProductId);
                if (Product == null)
                {
                    var myStockDB = new MyStorePRN231Context();
                    myStockDB.Products.Add(Product);
                    myStockDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The Product is already exist");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void Update(Product Product)
        {
            try
            {
                Product cate = GetProductByID(Product.ProductId);
                if (cate != null)
                {
                    var myStockDb = new MyStorePRN231Context();
                    myStockDb.Entry<Product>(Product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    myStockDb.SaveChanges();
                }
                else
                {
                    throw new Exception("the Product does not exist");

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public Product GetProductByID(int memID)
        {
            Product Product = null;
            try
            {
                var myStockDB = new MyStorePRN231Context();
                Product = myStockDB.Products.FirstOrDefault(m => m.ProductId == memID);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return Product;
        }
        public void Remove(int cateID)
        {
            try
            {
                Product Product = GetProductByID(cateID);

                if (Product != null)
                {
                    var myStockDb = new MyStorePRN231Context();
                    myStockDb.Products.Remove(Product);
                    myStockDb.SaveChanges();
                }
                else
                {
                    throw new Exception("the Product does not exist or already have an order.");

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}

