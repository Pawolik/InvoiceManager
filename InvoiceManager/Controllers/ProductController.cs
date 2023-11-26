using InvoiceManager.Models.Domains;
using InvoiceManager.Models.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceManager.Controllers
{
    public class ProductController : Controller
    {
        private ProductRepository _productRepository = new ProductRepository();

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var products = _productRepository.GetProducts(userId);
            return View(products);
        }

        public ActionResult Create()
        {
            var product = new Product { UserId = User.Identity.GetUserId() };
            return View("ProductForm", product);
        }

        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var product = _productRepository.GetProduct(id, userId);

            if (product == null)
                return HttpNotFound();

            return View("ProductForm", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Product product)
        {
            if (!ModelState.IsValid)
                return View("ProductForm", product);

            if (product.Id == 0)
                _productRepository.AddProduct(product);
            else
                _productRepository.UpdateProduct(product);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _productRepository.DeleteProduct(id, userId);
            }
            catch (Exception exception)
            {
                //powinno tutaj być jeszcze logowanie błędu do pliku
                return Json(new { Success = false, Message = exception.Message });
            }

            return Json(new { Success = true });
        }
    }
}