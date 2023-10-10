using JedProject_2.Dto_s;
using JedProject_2.Models;

namespace JedProject_2
{
    internal class Program
    {
        public static User AppUser = new User();
        public static int productId = 0;
        public static int companyId = 0;
        static void Main(string[] args)
        {
            int answer;
            string answerS;
            string companyName;
            Company.GetCompanies();
            do
            {
                Console.Write("Enter Company Name: ");
                companyName = Console.ReadLine();
            } while (!Company.CheckCompany(companyName).check);
            companyId = Company.CheckCompany(companyName).id;
            do
            {
                Menu.GetMenu();
                do
                {
                    Console.Write("Choose operation: ");
                    answerS = Console.ReadLine();
                } while (!int.TryParse(answerS, out answer));
                switch ((Menu.MainMenu)answer)
                {
                    case Menu.MainMenu.Register:
                        Console.Clear();
                        Console.WriteLine("Welcome to Register!");
                        Console.WriteLine();
                        string fullname = null;
                        string username = null;
                        string email = null;
                        string password = null;
                        string confirmPassword = null;
                        string rolee;
                        int role;
                        do
                        {
                            User.GetRoles();
                            Console.WriteLine();
                            Console.Write("Choose Role: ");
                            rolee = Console.ReadLine();
                        } while (!int.TryParse(rolee, out role) || !User.CheckRole(role));
                        do
                        {
                            Console.Write("Enter your fullname: ");
                            fullname = Console.ReadLine();
                        } while (!Menu.CheckLength(fullname.Trim()));
                        do
                        {
                            Console.Write("Enter your username: ");
                            username = Console.ReadLine();
                        } while (!Menu.CheckLength(username.Trim()));
                        do
                        {
                            Console.Write("Enter your email: ");
                            email = Console.ReadLine();
                        } while (!Menu.CheckEmail(email.Trim()));
                        do
                        {
                            Console.Write("Enter your password: ");
                            password = Console.ReadLine();
                        } while (!Menu.CheckPassword(password.Trim()));
                        do
                        {
                            Console.Write("Enter your confirm password: ");
                            confirmPassword = Console.ReadLine();
                        } while (!Menu.CheckPassword(confirmPassword.Trim()));
                        RegisterDto registerDto = new RegisterDto()
                        {
                            Role = role,
                            Fullname = fullname,
                            Email = email,
                            Username = username,
                            Password = password,
                            ConfirmPassword = confirmPassword
                        };
                        Console.WriteLine();
                        User.AddUser(registerDto);
                        break;
                    case Menu.MainMenu.Login:
                        Console.Clear();
                        Console.WriteLine("Welcome to Login!");
                        Console.WriteLine();
                        do
                        {
                            Console.Write("Enter your email: ");
                            email = Console.ReadLine();
                        } while (email.Trim() == "");
                        do
                        {
                            Console.Write("Enter your password: ");
                            password = Console.ReadLine();
                        } while (password.Trim() == "");
                        Console.WriteLine();
                        LoginDto loginDto = new LoginDto()
                        {
                            Email = email,
                            Password = password
                        };
                        var log = User.Login(loginDto);
                        if (log.check == true) { AppUser = log.user; }
                        break;
                    default:
                        return;
                }

                switch (AppUser.Role)
                {
                    case (int)User.Roles.Admin:
                        string valueS;
                        int value;
                        do
                        {
                            Menu.AdminMenu();
                            do
                            {
                                Console.Write("Choose operation: ");
                                valueS = Console.ReadLine();
                            } while (!int.TryParse(valueS, out value));

                            switch (value)
                            {
                                case (int)Menu.MenuAdmin.Products:
                                    Console.Clear();
                                    string valS;
                                    int val;
                                    Product.ProductMenu();
                                    do
                                    {
                                        Console.Write("Choose operation: ");
                                        valS = Console.ReadLine();
                                    } while (!int.TryParse(valS, out val));

                                    switch (val)
                                    {
                                        case (int)Product.MenuProduct.AddProduct:
                                            Console.Clear();
                                            string namee;
                                            string priceS;
                                            double price;
                                            string countSs;
                                            int countt;
                                            string categoryIdS;
                                            int categoryId;
                                            string purveyorIdS;
                                            int purveyorId;
                                            string storageIdS;
                                            int storageId;
                                            do
                                            {
                                                Console.Write("Enter product's name: ");
                                                namee = Console.ReadLine();
                                            } while (namee.Trim() == "");
                                            do
                                            {
                                                Console.Write("Enter product's price: ");
                                                priceS = Console.ReadLine();
                                            } while (!double.TryParse(priceS, out price));
                                            do
                                            {
                                                Console.Write("Enter product's count: ");
                                                countSs = Console.ReadLine();
                                            } while (!int.TryParse(countSs, out countt));
                                            do
                                            {
                                                Category.GetCategories();
                                                Console.Write("Enter product's categoryId: ");
                                                categoryIdS = Console.ReadLine();
                                            } while (!int.TryParse(categoryIdS, out categoryId) || !Category.CheckCategory(categoryId));
                                            do
                                            {
                                                Purveyor.GetPurveyors();
                                                Console.Write("Enter product's purveyorId: ");
                                                purveyorIdS = Console.ReadLine();
                                            } while (!int.TryParse(purveyorIdS, out purveyorId) || !Purveyor.CheckPurveyor(purveyorId));
                                            do
                                            {
                                                if (Storage.GetStorages(companyId) == 0) { return; }
                                                Console.Write("Enter product's storageId: ");
                                                storageIdS = Console.ReadLine();
                                            } while (!int.TryParse(storageIdS, out storageId) || !Storage.CheckStorage(storageId));
                                            ProductDto productDto = new ProductDto()
                                            {
                                                Name = namee,
                                                Price = price,
                                                Count = countt,
                                                CategoryId = categoryId,
                                                StorageId = storageId,
                                                PurveyorId = purveyorId
                                            };
                                            Product.AddProduct(productDto);
                                            break;
                                        case (int)Product.MenuProduct.UpdateProduct:
                                            Console.Clear();
                                            int ans;
                                            string ansS;
                                            string productIdS;
                                            do
                                            {
                                                if (Product.GetProducts(companyId) == 0) { return; }
                                                Console.Write("Enter product id for update: ");
                                                productIdS = Console.ReadLine();
                                            } while (!int.TryParse(productIdS, out productId) || !Product.CheckProduct(productId, companyId));
                                            Console.Clear();
                                            Product.UpdateMenu();

                                            do
                                            {
                                                Console.Write("Choose operation: ");
                                                ansS = Console.ReadLine();
                                            } while (!int.TryParse(ansS, out ans));

                                            switch (ans)
                                            {
                                                case (int)Product.MenuUpdate.UpdateName:
                                                    do
                                                    {
                                                        Console.Write("Enter new name: ");
                                                        namee = Console.ReadLine();
                                                    } while (namee.Trim() == "" || Product.ExistsProduct(namee));
                                                    Product.UpdateName(productId, namee);
                                                    break;
                                                case (int)Product.MenuUpdate.UpdatePrice:
                                                    do
                                                    {
                                                        Console.Write("Enter product's price: ");
                                                        priceS = Console.ReadLine();
                                                    } while (!double.TryParse(priceS, out price));
                                                    Product.UpdatePrice(productId, price);
                                                    break;
                                                case (int)Product.MenuUpdate.UpdateCount:
                                                    do
                                                    {
                                                        Console.Write("Enter product's count: ");
                                                        countSs = Console.ReadLine();
                                                    } while (!int.TryParse(countSs, out countt));
                                                    Product.UpdateCount(productId, countt);
                                                    break;
                                                case (int)Product.MenuUpdate.UpdateCategory:
                                                    do
                                                    {
                                                        Category.GetCategories();
                                                        Console.Write("Enter product's categoryId: ");
                                                        categoryIdS = Console.ReadLine();
                                                    } while (!int.TryParse(categoryIdS, out categoryId) || !Category.CheckCategory(categoryId));
                                                    Product.UpdateCategory(productId, categoryId);
                                                    break;
                                                case (int)Product.MenuUpdate.UpdatePurveyor:
                                                    do
                                                    {
                                                        Purveyor.GetPurveyors();
                                                        Console.Write("Enter product's purveyorId: ");
                                                        purveyorIdS = Console.ReadLine();
                                                    } while (!int.TryParse(purveyorIdS, out purveyorId) || !Purveyor.CheckPurveyor(purveyorId));
                                                    Product.UpdatePurveyor(productId, purveyorId);
                                                    break;
                                                case (int)Product.MenuUpdate.UpdateStorage:
                                                    do
                                                    {
                                                        if (Storage.GetStorages(companyId) == 0) { return; }
                                                        Console.Write("Enter product's storageId: ");
                                                        storageIdS = Console.ReadLine();
                                                    } while (!int.TryParse(storageIdS, out storageId) || !Storage.CheckStorage(storageId));
                                                    Product.UpdateStorage(productId, storageId);
                                                    break;
                                                default:
                                                    break;
                                            }
                                            break;
                                        case (int)Product.MenuProduct.DeleteProduct:
                                            Console.Clear();
                                            int id;
                                            string idS;
                                            do
                                            {
                                                if (Product.GetProducts(companyId) == 0) { return; }
                                                Console.Write("Enter product's id for delete: ");
                                                idS = Console.ReadLine();
                                            } while (!int.TryParse(idS, out id));
                                            Product.DeleteProduct(id);
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case (int)Menu.MenuAdmin.Purveyors:
                                    Console.Clear();
                                    string vaS;
                                    int va;
                                    Purveyor.PurveyorMenu();
                                    do
                                    {
                                        Console.Write("Choose operation: ");
                                        vaS = Console.ReadLine();
                                    } while (!int.TryParse(vaS, out va));
                                    switch (va)
                                    {
                                        case (int)Purveyor.MenuPurveyor.AddPurveyor:
                                            Console.Clear();
                                            string namee;
                                            do
                                            {
                                                Console.Write("Enter purveyor's name: ");
                                                namee = Console.ReadLine();
                                            } while (namee.Trim() == "" || Purveyor.ExistPurveyor(namee));
                                            Purveyor.AddPurveyor(namee);
                                            break;
                                        case (int)Purveyor.MenuPurveyor.UpdatePurveyor:
                                            Console.Clear();
                                            string purveyorIds;
                                            int purveyorId;
                                            do
                                            {
                                                Purveyor.GetPurveyors();
                                                Console.Write("Enter purveyor id for update: ");
                                                purveyorIds = Console.ReadLine();
                                            } while (!int.TryParse(purveyorIds, out purveyorId) || !Purveyor.CheckPurveyor(purveyorId));
                                            do
                                            {
                                                Console.Write("Enter purveyor's new name: ");
                                                namee = Console.ReadLine();
                                            } while (namee.Trim() == "" || Purveyor.ExistPurveyor(namee));
                                            Purveyor.UpdateName(purveyorId, namee);
                                            break;
                                        case (int)Purveyor.MenuPurveyor.DeletePurveyor:
                                            Console.Clear();
                                            int id;
                                            string idS;
                                            Purveyor.GetPurveyors();
                                            do
                                            {
                                                Console.Write("Enter id for delete: ");
                                                idS = Console.ReadLine();
                                            } while (!int.TryParse(idS, out id));
                                            Purveyor.DeletePurveyor(id);
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case (int)Menu.MenuAdmin.Sales:
                                    Console.Clear();
                                    Sale.GetSales(companyId);
                                    break;
                                case (int)Menu.MenuAdmin.Reports:
                                    Console.Clear();
                                    Menu.ReportMenu();
                                    do
                                    {
                                        Console.Write("Choose operation: ");
                                        answerS = Console.ReadLine();
                                    } while (!int.TryParse(answerS, out answer));
                                    switch (answer)
                                    {
                                        case (int)Menu.MenuReport.CategoryReport:
                                            Console.Clear();
                                            string categoryS;
                                            int category;
                                            Category.GetCategories();
                                            do
                                            {
                                                Console.Write("Choose category for report: ");
                                                categoryS = Console.ReadLine();
                                            } while (!int.TryParse(categoryS, out category) || !Category.CheckCategory(category));
                                            Report.CategoryReport(category, companyId);
                                            break;
                                        case (int)Menu.MenuReport.SaleReport:
                                            Console.Clear();
                                            Report.SalesPdf(companyId);
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case (int)Menu.MenuAdmin.Search:
                                    int op;
                                    string opS;
                                    Console.Clear();
                                    Menu.SearchMenu();
                                    do
                                    {
                                        Console.Write("Choose operation: ");
                                        opS = Console.ReadLine();
                                    } while (!int.TryParse(opS, out op));
                                    switch (op)
                                    {
                                        case (int)Menu.MenuSearch.SearchProductName:
                                            string namee;
                                            Console.Clear();
                                            do
                                            {
                                                Console.Write("Enter product name: ");
                                                namee = Console.ReadLine();
                                            } while (namee.Trim() == "");
                                            Product.SearchByName(namee, companyId);
                                            break;
                                        case (int)Menu.MenuSearch.SearchProductCategory:
                                            int categoryId;
                                            string categoryIdS;
                                            Console.Clear();
                                            do
                                            {
                                                Category.GetCategories();
                                                Console.Write("Enter product's categoryId: ");
                                                categoryIdS = Console.ReadLine();
                                            } while (!int.TryParse(categoryIdS, out categoryId) || !Category.CheckCategory(categoryId));
                                            Product.SearchByCategory(categoryId, companyId);
                                            break;
                                        case (int)Menu.MenuSearch.SearchPurveyors:
                                            string purveyorName;
                                            Console.Clear();
                                            do
                                            {
                                                Console.Write("Enter purveyor name: ");
                                                purveyorName = Console.ReadLine();
                                            } while (purveyorName.Trim() == "");
                                            Purveyor.SearchByName(purveyorName);
                                            break;
                                        case (int)Menu.MenuSearch.SearchUsers:
                                            string user;
                                            Console.Clear();
                                            do
                                            {
                                                Console.Write("Enter user email or fullname: ");
                                                user = Console.ReadLine();
                                            } while (user.Trim() == "");
                                            User.Search(user);
                                            break;
                                        case (int)Menu.MenuSearch.SearchSales:
                                            string startDateS;
                                            DateTime startDate;
                                            Console.Clear();
                                            do
                                            {
                                                Console.Write("Enter start date for search: ");
                                                startDateS = Console.ReadLine();
                                            } while (!DateTime.TryParse(startDateS, out startDate));
                                            Sale.Search(startDate, companyId);
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        } while (value != 0);
                        break;

                    case (int)User.Roles.Member:
                        int quit;
                        string quitS;
                        string name;
                        int count;
                        string countS;
                        int productCount;
                        Console.Clear();
                        do
                        {
                            do
                            {
                                if (Sale.GetProducts(companyId) == 0) { return; }
                                Console.Write("Enter product name for buy: ");
                                name = Console.ReadLine();
                            } while (name.Trim() == "" || Product.FindProduct(name, companyId) == 0);
                            do
                            {
                                Console.Write("Enter product count: ");
                                countS = Console.ReadLine();
                            } while (!int.TryParse(countS, out count));
                            SaleDto saleDto = new SaleDto()
                            {
                                CompanyId = companyId,
                                UserId = AppUser.Id,
                                ProductId = Product.FindProduct(name, companyId),
                                ProductCount = count,
                                PurchaseDate = DateTime.Now,
                            };
                            Sale.AddSale(saleDto);

                            do
                            {
                                Console.WriteLine("1 - Buy Product");
                                Console.WriteLine("0 - Quit");
                                quitS = Console.ReadLine();
                            } while (!int.TryParse(quitS, out quit));
                            Console.Clear();
                        } while (quit != 0);
                        break;

                    default:
                        return;
                }

            } while (answer != 0);
        }
    }
}