using MyShopCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> ProductCategories;
        public ProductCategoryRepository()
        {
            ProductCategories = cache["ProductCategories"] as List<ProductCategory>;
            if (ProductCategories == null)
            {
                ProductCategories = new List<ProductCategory>();

            }
        }
        public void commit()
        {
            cache["ProductCategories"] = ProductCategories;
        }
        public void Insert(ProductCategory p)
        {
            ProductCategories.Add(p);
        }

        public void Update(ProductCategory productCategory)
        {
            ProductCategory productCategoryToUpdate = ProductCategories.Find(p => p.ID == productCategory.ID);
            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = ProductCategories.Find(p => p.ID == Id);
            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product not found");
            }


        }
        public IQueryable<ProductCategory> Collection()
        {
            return ProductCategories.AsQueryable();
        }
        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = ProductCategories.Find(p => p.ID == Id);
            if (productCategoryToDelete != null)
            {
                ProductCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }

        }

    }
}
