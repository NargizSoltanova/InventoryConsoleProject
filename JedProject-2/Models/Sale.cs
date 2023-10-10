using JedProject_2.DAL;
using JedProject_2.Dto_s;
using JedProject_2.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace JedProject_2.Models;

public class Sale : BaseEntity
{
    public User? User { get; set; }
    public int UserId { get; set; }
    public Product? Product { get; set; }
    public int ProductId { get; set; }
    public int ProductCount { get; set; }
    public DateTime PurchaseDate { get; set; }

    public static List<Sale> GetSales(int companyId)
    {
        AppDbContext context = new AppDbContext();
        var storages = context.Storages.Where(x => x.CompanyId == companyId).ToList();
        var sales = context.Sales.Include(x => x.User).Include(x => x.Product).ToList();
        if (sales.Any() || storages.Any())
        {
            storages.ForEach(y =>
            {
                sales.ForEach(x =>
                {
                    if (x.Product.StorageId == y.Id)
                    {
                        Console.WriteLine($"Buyer: {x.User.Fullname}  Product: {x.Product.Name}  Product Count: {x.ProductCount}  Date: {x.PurchaseDate}");
                    }
                });
            });
            return sales;
        }
        else Console.WriteLine("Not Found!");
        return sales;
    }

    public static void Search(DateTime startDate, int companyId)
    {
        AppDbContext context = new AppDbContext();
        if (startDate > DateTime.Now)
        {
            Console.WriteLine("Start date must be less than now"); return;
        }
        var storages = context.Storages.Where(x => x.CompanyId == companyId).ToList();
        var sales = context.Sales.Include(x => x.Product).Include(x => x.User).Where(x => x.PurchaseDate >= startDate).ToList();
        if (sales.Any() || storages.Any())
        {
            storages.ForEach(y =>
            {
                sales.ForEach(x =>
                {
                    if (x.Product.StorageId == y.Id)
                    {
                        Console.WriteLine($"Buyer: {x.User.Fullname}  Product: {x.Product.Name}  Product Count: {x.ProductCount}  Date: {x.PurchaseDate}");
                    }
                });
            });
        }
        else Console.WriteLine("Sale not found for this date!");
    }

    public static int GetProducts(int companyId)
    {
        AppDbContext context = new AppDbContext();
        var storages = context.Storages.Where(x => x.CompanyId == companyId).ToList();
        List<Product> allproducts = new List<Product>();
        if (storages.Any())
        {
            storages.ForEach(s =>
            {
                var products = context.Products.Where(x => x.StorageId == s.Id && !x.IsDeleted).ToList();
                if (products != null) allproducts.AddRange(products);
            });
        }
        Console.WriteLine("=====Products=====");
        if (allproducts.Any())
        {
            allproducts.ForEach(x => { Console.WriteLine($"Name: {x.Name}  Price: {x.Price} "); });
            return allproducts.Count;
        }
        else
        {
            Console.WriteLine("Not Found!");
            return 0;
        }
    }

    public static void AddSale(SaleDto saleDto)
    {
        AppDbContext context = new AppDbContext();

        var productCheck = context.Products.FirstOrDefault(x => x.Id == saleDto.ProductId);

        if (productCheck == null)
        {
            Console.WriteLine("Product Not Found!");
            return;
        }
        int id = Product.FindProduct(productCheck.Name, saleDto.CompanyId);
        var product = context.Products.FirstOrDefault(x => x.Id == id);
        if (product == null)
        {
            Console.WriteLine("Product Not Found!");
            return;
        }
        if (product.Count < saleDto.ProductCount)
        {
            Console.WriteLine($"Not yet product in stock. Stock:{product.Count}");
            return;
        }
        context.Sales.Add(new Sale()
        {
            ProductCount = saleDto.ProductCount,
            PurchaseDate = saleDto.PurchaseDate,
            ProductId = saleDto.ProductId,
            UserId = saleDto.UserId
        });
        product.Count = product.Count - saleDto.ProductCount;
        context.Products.Update(product);
        context.SaveChanges();
        Console.WriteLine($"Sold!You will pay {saleDto.ProductCount * product.Price} $");
    }
}

