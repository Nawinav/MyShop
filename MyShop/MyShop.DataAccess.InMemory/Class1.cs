using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShopCore;
using MyShopCore.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> Products;
        public ProductRepository()
        {
            Products = cache["Products"] as List<Product>;
            if (Products == null)
            {
                Products = new List<Product>();

            }
        }
        public void commit()
        {
            cache["Products"] = Products;
        }
        public void Insert(Product p)
        {
            Products.Add(p);
        }

        public void Update(Product Product)
        {
            Product productToUpdate = Products.Find(p => p.Id == Product.Id);
            if (productToUpdate != null)
            {
                productToUpdate = Product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        public Product Find(string Id)
        {
            Product product = Products.Find(p => p.Id == Id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }


        }
        public IQueryable<Product> Collection()
        {
            return Products.AsQueryable();
        }
        public void Delete(string Id)
        {
            Product productToDelete = Products.Find(p => p.Id == Id);
            if (productToDelete != null)
            {
                Products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }

        }


    }
}

