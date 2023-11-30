using InvoiceManager.Models.Domains;
using InvoiceManager.Models.Repositories;
using InvoiceManager.Models.ViewModels;
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

        public ActionResult Product()
        {
            var userId = User.Identity.GetUserId();
            var products = _productRepository.GetProducts(userId);
            return View(products);
        }

        public Product GetNewProduct(string userId)
        {
            return new Product
            {
                UserId = userId
            };
        }


        public ActionResult ProductForm(int id = 0)
        {
            var userId = User.Identity.GetUserId();

            var product = id == 0 ?
                GetNewProduct(userId) :
                _productRepository.GetProduct(id, userId);

            var vm = PrepareProductVm(product, userId);

            return View(vm);
        }

        private EditProductViewModel PrepareProductVm(Product product, string userId)
        {
            return new EditProductViewModel
            {
                Product = product,
                Heading = product.Id == 0 ? "Dodawanie nowego produktu" : "Produkt"
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Product product)
        {
            var userId = User.Identity.GetUserId();
            product.UserId = userId;

            if (!ModelState.IsValid)
                return View("ProductForm", product);

            if (product.Id == 0)
                _productRepository.AddProduct(product);
            else
                _productRepository.UpdateProduct(product);

            return RedirectToAction("Product", "Product");
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