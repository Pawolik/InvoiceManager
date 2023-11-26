using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceManager.Models.Repositories
{
    public class ProductRepository
    {
        public List<Product> GetProducts(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Products.Where(x => x.UserId == userId).ToList();
            }
        }

        public Product GetProduct(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Products.Single(x => x.Id == id && x.UserId == userId);
            }
        }

        public void AddProduct(Product product)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var context = new ApplicationDbContext())
            {
                var productToUpdate = context.Products.Single(p => p.Id == product.Id && p.UserId == product.UserId);
                productToUpdate.Name = product.Name;
                productToUpdate.Value = product.Value;
                context.SaveChanges();
            }
        }

        public void DeleteProduct(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var productToDelete = context.Products.Single(p => p.Id == id && p.UserId == userId);
                context.Products.Remove(productToDelete);
                context.SaveChanges();
            }            
        }
    }
}