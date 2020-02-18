using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.DataAccess.InMemory;
using MyShopCore.Models;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        // GET: ProductCategory
        // GET: ProductManager
        ProductCategoryRepository context;
        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepository();
        }
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }
        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                context.Insert(productCategory);
                context.commit();
                return RedirectToAction("Index");
            }

        }
        public ActionResult Edit(string ID)
        {
            ProductCategory productCategory = context.Find(ID);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string ID)
        {
            ProductCategory productCategoryToEdit = context.Find(ID);
            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }


                productCategoryToEdit.Category = productCategory.Category;
              
                context.commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string ID)
        {

            ProductCategory productCategoryToDelete = context.Find(ID);

            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoryToDelete);

            }

        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult confirmDelete(string ID)
        {
            ProductCategory productCategoryToDelete = context.Find(ID);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(ID);
                context.commit();
                return RedirectToAction("Index");

            }
        }



    }


}
