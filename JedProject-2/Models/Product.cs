using JedProject_2.DAL;
using JedProject_2.Dto_s;
using JedProject_2.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace JedProject_2.Models;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int Count { get; set; }
    public Category? Category { get; set; }
    public int CategoryId { get; set; }
    public Purveyor? Purveyor { get; set; }
    public int PurveyorId { get; set; }
    public Storage? Storage { get; set; }
    public int StorageId { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<Sale>? Sales { get; set; }
    public Product()
    {
        Sales = new HashSet<Sale>();
    }

    public static void ProductMenu()
    {
        Console.WriteLine("1 - Add Product");
        Console.WriteLine("2 - Update Product");
        Console.WriteLine("3 - Delete Product");
    }

    public enum MenuProduct
    {
        AddProduct = 1,
        UpdateProduct,
        DeleteProduct
    }

    public static void UpdateMenu()
    {
        Console.WriteLine("1 - Update product name");
        Console.WriteLine("2 - Update product price");
        Console.WriteLine("3 - Update product count");
        Console.WriteLine("4 - Update product's category");
        Console.WriteLine("5 - Update product's purveyor");
        Console.WriteLine("6 - Update product's storage");
    }

    public enum MenuUpdate
    {
        UpdateName = 1,
        UpdatePrice,
        UpdateCount,
        UpdateCategory,
        UpdatePurveyor,
        UpdateStorage,
    }

    public static bool CheckProduct(int id, int companyId)
    {
        AppDbContext context = new AppDbContext();
        var storages = context.Storages.Where(x => x.CompanyId == companyId).ToList();
        var product = context.Products.Include(x => x.Storage).FirstOrDefault(x => x.Id == id);
        bool exists = false;
        if (storages.Any())
        {
            storages.ForEach(y =>
            {
                if (y.Id == product.StorageId) exists = true;
            });
        }
        return exists;
    }

    public static bool ExistsProduct(string name)
    {
        AppDbContext context = new AppDbContext();
        var product = context.Products.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        if (product != null)
        {
            Console.WriteLine("Product alredy exists!");
            return true;
        }
        return false;
    }

    public static int FindProduct(string name, int companyId)
    {
        AppDbContext context = new AppDbContext();
        var storages = context.Storages.Where(x => x.CompanyId == companyId).ToList();
        Product product;
        int id = 0;
        if (storages.Any())
        {
            storages.ForEach(s =>
            {
                product = context.Products.Where(x => x.StorageId == s.Id && !x.IsDeleted).FirstOrDefault(x => x.Name == name);
                if (product != null) id = product.Id;
            });
        }
        return id;
    }

    public static void SearchByName(string name, int companyId)
    {
        AppDbContext context = new AppDbContext();
        var storages = context.Storages.Where(x => x.CompanyId == companyId).ToList();
        var product = context.Products.Include(x => x.Storage).Include(x => x.Category).Include(x => x.Purveyor).FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        if (product == null)
        {
            Console.WriteLine("Product not found!"); return;
        }
        if (storages.Any())
        {
            storages.ForEach(y =>
            {
                if (y.Id == product.StorageId)
                {
                    Console.WriteLine($"Name: {product.Name}  Price: {product.Price}  Count: {product.Price}");
                    Console.WriteLine($"Category: {product.Category.Name}  Purveyor: {product.Purveyor.Name}  Storage: {product.Storage.Name}");
                }
            });
        }
        else Console.WriteLine("Product not found!");
    }

    public static void SearchByCategory(int categoryId, int companyId)
    {
        AppDbContext context = new AppDbContext();
        var storages = context.Storages.Where(x => x.CompanyId == companyId).ToList();
        var products = context.Products.Include(x => x.Category).Include(x => x.Purveyor).Include(x => x.Storage).Where(x => x.CategoryId == categoryId).ToList();
        if (products.Any() || storages.Any())
        {
            products.ForEach(product =>
            {
                storages.ForEach(y =>
                {
                    if (y.Id == product.StorageId)
                    {
                        Console.WriteLine($"Name: {product.Name}  Price: {product.Price}  Count: {product.Price}");
                        Console.WriteLine($"Category: {product.Category.Name}  Purveyor: {product.Purveyor.Name}  Storage: {product.Storage.Name}");
                    }
                });
            });
        }
        else Console.WriteLine("Product not found!");
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
        if (allproducts.Any())
        {
            allproducts.ForEach(pro => { Console.WriteLine($"{pro.Id} -- {pro.Name}"); });
            return allproducts.Count();
        }
        else
        {
            Console.WriteLine("Not Found!"); return 0;
        }
    }

    public static void AddProduct(ProductDto productDto)
    {
        AppDbContext context = new AppDbContext();
        var product = context.Products.Where(x => x.CategoryId == productDto.CategoryId && x.PurveyorId == productDto.PurveyorId && x.StorageId == productDto.StorageId).FirstOrDefault(x => x.Name == productDto.Name);
        if (product != null)
        {
            product.Count += productDto.Count;
            if (product.IsDeleted == true) product.IsDeleted = false;
            Console.WriteLine("Product Count Added!");
        }
        else
        {
            context.Products.Add(new Product()
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Count = productDto.Count,
                CategoryId = productDto.CategoryId,
                PurveyorId = productDto.PurveyorId,
                StorageId = productDto.StorageId,
                IsDeleted = false
            });
            Console.WriteLine("Product Added!");
        }
        context.SaveChanges();
    }

    public static void DeleteProduct(int id)
    {
        AppDbContext context = new AppDbContext();
        var product = context.Products.FirstOrDefault(x => x.Id == id);
        if (product == null)
        {
            Console.WriteLine("Product not found!"); return;
        }
        product.IsDeleted = true;
        context.SaveChanges();
        Console.WriteLine("Product Deleted!");
    }

    public static void UpdateName(int productId, string name)
    {
        AppDbContext context = new AppDbContext();
        var product = context.Products.FirstOrDefault(x => x.Id == productId);
        if (product == null)
        {
            Console.WriteLine("Product Not Found!"); return;
        }
        product.Name = name;
        context.Products.Update(product);
        context.SaveChanges();
        Console.WriteLine("Product Updated!");
    }

    public static void UpdatePrice(int productId, double price)
    {
        AppDbContext context = new AppDbContext();
        var product = context.Products.FirstOrDefault(x => x.Id == productId);
        if (product == null)
        {
            Console.WriteLine("Product Not Found!"); return;
        }
        product.Price = price;
        context.Products.Update(product);
        context.SaveChanges();
        Console.WriteLine("Product Updated!");
    }

    public static void UpdateCount(int productId, int count)
    {
        AppDbContext context = new AppDbContext();
        var product = context.Products.FirstOrDefault(x => x.Id == productId);
        if (product == null)
        {
            Console.WriteLine("Product Not Found!"); return;
        }
        product.Count = count;
        context.Products.Update(product);
        context.SaveChanges();
        Console.WriteLine("Product Updated!");
    }

    public static void UpdateCategory(int productId, int categoryId)
    {
        AppDbContext context = new AppDbContext();
        var product = context.Products.FirstOrDefault(x => x.Id == productId);
        if (product == null)
        {
            Console.WriteLine("Product Not Found!"); return;
        }
        product.CategoryId = categoryId;
        context.Products.Update(product);
        context.SaveChanges();
        Console.WriteLine("Product Updated!");
    }

    public static void UpdatePurveyor(int productId, int purveyorId)
    {
        AppDbContext context = new AppDbContext();
        var product = context.Products.FirstOrDefault(x => x.Id == productId);
        if (product == null)
        {
            Console.WriteLine("Product Not Found!"); return;
        }
        product.PurveyorId = purveyorId;
        context.Products.Update(product);
        context.SaveChanges();
        Console.WriteLine("Product Updated!");
    }

    public static void UpdateStorage(int productId, int storageId)
    {
        AppDbContext context = new AppDbContext();
        var product = context.Products.FirstOrDefault(x => x.Id == productId);
        if (product == null)
        {
            Console.WriteLine("Product Not Found!"); return;
        }
        product.StorageId = storageId;
        context.Products.Update(product);
        context.SaveChanges();
        Console.WriteLine("Product Updated!");
    }
}
