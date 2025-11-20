using AspNetCore10RazorPages.ServiceDefaults.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore10RazorPages.Pages
{
    public class EditProductModel : PageModel
    {
        public List<Customers> GetCustomers;
        private readonly NorthwindContext db = new();
        public List<Products> GetProducts { get; set; }

        public List<Categories> GetCategories { get; set; }
        public List<Suppliers> GetSuppliers { get; set; }

        public Products GetProduct { get; set; }

        public List<SelectListItem> GetSuppliersSel { get; set; }
        public List<SelectListItem> GetCategoriesSel { get; set; }

        public int ID { get; set; }
        public async void OnGet(int id)
        {
            ID = id;
            if(id>0)
            {
                GetProduct = await db.Products?.FirstOrDefaultAsync(p => p.ProductID == id);
            }
            if (GetCustomers is null)
            {
                var customers = await db.Customers.ToListAsync();

                GetCustomers = customers;
            }

            if (GetProducts is null)
            {
                var products = await db.Products.ToListAsync();

                GetProducts = products;
            }
            if(GetCategories is null)
            {
                var categories = await db.Categories.ToListAsync();
                GetCategories = categories;
                foreach(var category in categories)
                {
                    GetCategoriesSel.Add(new SelectListItem
                    {
                        Value = category.CategoryID.ToString(),
                        Text = category.CategoryName
                    });
                }
            }

            if(GetSuppliers is null)
            {
                var suppliers = await db.Suppliers.ToListAsync();
                GetSuppliers = suppliers;
                foreach(var supplier in suppliers)
                {
                    GetSuppliersSel.Add(new SelectListItem
                    {
                        Value = supplier.SupplierID.ToString(),
                        Text = supplier.CompanyName
                    });
                }
            }
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("/");
        }

        public string GetCategoryName(int categoryId)
        {
            var category = db.Categories.FirstOrDefault(c => c.CategoryID == categoryId);
            return category != null ? category.CategoryName : "Unknown Category";
        }

        public string GetSupplierName(int supplierId)
        {
            var supplier = db.Suppliers.FirstOrDefault(s => s.SupplierID == supplierId);
            return supplier != null ? supplier.CompanyName : "Unknown Supplier";
        }
    }
}
