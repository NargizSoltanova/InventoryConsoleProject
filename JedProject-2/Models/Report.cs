using JedProject_2.DAL;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace JedProject_2.Models;

public class Report
{
    public static void Excel()
    {
        IWorkbook workbook = new XSSFWorkbook();
        ISheet sheet = workbook.CreateSheet("Sheet1");
        IRow row = sheet.CreateRow(0);
        ICell cell = row.CreateCell(0);
        cell.SetCellValue("Hello World!");
        string path = "C:\\Users\\User\\OneDrive\\Masaüstü\\JedProject-2\\JedProject-2\\Reports\\";
        string filePath = Path.Combine(path, "myworkbook.xlsx");
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        {
            workbook.Write(fileStream);
        }
    }

    public static void SalesPdf(int companyId)
    {
        using (AppDbContext context = new AppDbContext())
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics graphics = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 15, XFontStyle.Regular);
            XFont titleFont = new XFont("Verdana", 18, XFontStyle.Bold);

            string title = " Buyer               Product        Count        Date        ";
            XRect titleRect = new XRect(0, 10, page.Width - 20, 30);
            XStringFormat titleFormat = new XStringFormat();
            titleFormat.Alignment = XStringAlignment.Center;
            graphics.DrawString(title, titleFont, XBrushes.Black, titleRect, titleFormat);


            var sales = Sale.GetSales(companyId);
            double yOffset = 50;
            if (sales.Any())
            {
                sales.ForEach(x =>
                {
                    var product = context.Products.FirstOrDefault(y => y.Id == x.ProductId);
                    var user = context.Users.FirstOrDefault(y => y.Id == x.UserId);
                    string value = $"   {user.Fullname}          {product.Name}               {x.ProductCount}         {x.PurchaseDate}         ";
                    XRect rect = new XRect(10, yOffset, page.Width - 20, 20);
                    graphics.DrawString(value, font, XBrushes.Black, rect, XStringFormats.CenterLeft);

                    yOffset += 30;
                });
            }

            DateTime date = DateTime.UtcNow;
            string value = date.Day.ToString() + "_" + date.Month.ToString() + "_" + date.Year.ToString() + "_" + date.Hour.ToString() + "_" + date.Minute.ToString();

            string filePath = $"C:\\Users\\User\\OneDrive\\Masaüstü\\JedProject-2\\JedProject-2\\Reports\\SaleReport_{value}.pdf";
            document.Save(filePath);
        }
    }

    public static void CategoryReport(int categoryId, int companyId)
    {
        Category category;
        using (AppDbContext context = new AppDbContext())
        {
            var storages = context.Storages.Where(x => x.CompanyId == companyId).ToList();
            var products = context.Products.Where(x => x.CategoryId == categoryId).Include(x => x.Category).Include(x => x.Purveyor).Include(x => x.Storage).ToList();
            category = context.Categories.FirstOrDefault(x => x.Id == categoryId);
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics graphics = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 15, XFontStyle.Regular);
            XFont titleFont = new XFont("Verdana", 18, XFontStyle.Bold);

            string title = " Name     Price        Count      Purveyor      Storage   ";
            XRect titleRect = new XRect(0, 10, page.Width - 20, 30);
            XStringFormat titleFormat = new XStringFormat();
            titleFormat.Alignment = XStringAlignment.Center;
            graphics.DrawString(title, titleFont, XBrushes.Black, titleRect, titleFormat);

            double yOffset = 50;

            if (products.Any())
            {
                if (storages.Any())
                {
                    storages.ForEach(y =>
                    {
                        products.ForEach(prod =>
                        {
                            if (prod.StorageId == y.Id)
                            {
                                string value = $"        {prod.Name}           {prod.Price}             {prod.Count}             {prod.Purveyor.Name}           {prod.Storage.Name} ";
                                XRect rect = new XRect(10, yOffset, page.Width - 20, 20);
                                graphics.DrawString(value, font, XBrushes.Black, rect, XStringFormats.CenterLeft);

                                yOffset += 30;
                            }
                        });
                    });
                }
            }

            DateTime date = DateTime.UtcNow;
            string value = date.Day.ToString() + "_" + date.Month.ToString() + "_" + date.Year.ToString() + "_" + date.Hour.ToString() + "_" + date.Minute.ToString();

            string filePath = $"C:\\Users\\User\\OneDrive\\Masaüstü\\JedProject-2\\JedProject-2\\Reports\\{category.Name}Report_{value}.pdf";
            document.Save(filePath);
        }
    }
}
