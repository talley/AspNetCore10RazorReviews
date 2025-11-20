using AspNetCore10RazorPages.ServiceDefaults.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AspNetCore10RazorPages.Pages;

public class IndexModel : PageModel
{
    public List<Customers> GetCustomers;
    private readonly NorthwindContext db=new NorthwindContext();
    public List<Products> GetProducts;
    public async Task OnGet()
    {
        if(GetCustomers is null)
        {
            var customers =await db.Customers.ToListAsync();

            GetCustomers = customers;
        }

        if (GetProducts is null)
        {
            var products = await db.Products.ToListAsync();

            GetProducts = products;
        }
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
