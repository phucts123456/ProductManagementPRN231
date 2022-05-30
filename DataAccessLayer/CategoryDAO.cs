using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer
{
    public class CategoryDAO
    {

        private static CategoryDAO instance = null;
        private static readonly object instanceLock = new object();
        private CategoryDAO() { }
        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }


            }
        }
        public IEnumerable<Category> CategoryList()
        {
            List<Category> Categorys;
            try
            {
                var DBcontext = new MyStorePRN231Context();
                Categorys = DBcontext.Categories.ToList();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return Categorys;
        }
        public void AddNew(Category Category)
        {
            try
            {

                Category Cate = GetCategoryByID(Category.CategoryId);
                if (Category == null)
                {
                    var myStockDB = new MyStorePRN231Context();
                    myStockDB.Categories.Add(Category);
                    myStockDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The Category is already exist");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void Update(Category Category)
        {
            try
            {
                Category cate = GetCategoryByID(Category.CategoryId);
                if (cate != null)
                {
                    var myStockDb = new MyStorePRN231Context();
                    myStockDb.Entry<Category>(Category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
        public Category GetCategoryByID(int memID)
        {
            Category Category = null;
            try
            {
                var myStockDB = new MyStorePRN231Context();
                Category = myStockDB.Categories.FirstOrDefault(m => m.CategoryId == memID);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return Category;
        }
        public void Remove(int cateID)
        {
            try
            {
                Category Category = GetCategoryByID(cateID);

                if (Category != null)
                {
                    var myStockDb = new MyStorePRN231Context();
                    myStockDb.Categories.Remove(Category);
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

