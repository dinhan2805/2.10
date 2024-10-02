using System;
using System.Collections.Generic;
using System.Linq;

// Lop truu tuong Product
public abstract class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public Product(string name, decimal price, int stock)
    {
        Name = name;
        Price = price;
        Stock = stock;
    }

    public abstract void DisplayProductInfo();
    public void ApplyDiscount(decimal percentage)
    {
        Price -= Price * percentage / 100;
    }
}

// Giao dien ISellable
public interface ISellable
{
    void Sell(int quantity);
    bool IsInStock();
}

// MobilePhone class ke thua Product va trien khai ISellable
public class MobilePhone : Product, ISellable
{
    public string OperatingSystem { get; set; }

    public MobilePhone(string name, decimal price, int stock, string operatingSystem)
        : base(name, price, stock)
    {
        OperatingSystem = operatingSystem;
    }

    public override void DisplayProductInfo()
    {
        Console.WriteLine($"Mobile Phone: {Name}, Price: {Price}, Stock: {Stock}, OS: {OperatingSystem}");
    }

    public void Sell(int quantity)
    {
        if (quantity <= Stock)
        {
            Stock -= quantity;
            Console.WriteLine($"{quantity} units of {Name} sold.");
        }
        else
        {
            Console.WriteLine("Insufficient stock.");
        }
    }

    public bool IsInStock()
    {
        return Stock > 0;
    }
}

// Laptop class ke thua Product va trien khai ISellable
public class Laptop : Product, ISellable
{
    public string Processor { get; set; }

    public Laptop(string name, decimal price, int stock, string processor)
        : base(name, price, stock)
    {
        Processor = processor;
    }

    public override void DisplayProductInfo()
    {
        Console.WriteLine($"Laptop: {Name}, Price: {Price}, Stock: {Stock}, Processor: {Processor}");
    }

    public void Sell(int quantity)
    {
        if (quantity <= Stock)
        {
            Stock -= quantity;
            Console.WriteLine($"{quantity} units of {Name} sold.");
        }
        else
        {
            Console.WriteLine("Insufficient stock.");
        }
    }

    public bool IsInStock()
    {
        return Stock > 0;
    }
}

// Accessory class ke thua Product va trien khai ISellable
public class Accessory : Product, ISellable
{
    public string Type { get; set; }

    public Accessory(string name, decimal price, int stock, string type)
        : base(name, price, stock)
    {
        Type = type;
    }

    public override void DisplayProductInfo()
    {
        Console.WriteLine($"Accessory: {Name}, Price: {Price}, Stock: {Stock}, Type: {Type}");
    }

    public void Sell(int quantity)
    {
        if (quantity <= Stock)
        {
            Stock -= quantity;
            Console.WriteLine($"{quantity} units of {Name} sold.");
        }
        else
        {
            Console.WriteLine("Insufficient stock.");
        }
    }

    public bool IsInStock()
    {
        return Stock > 0;
    }
}

// Lop quan ly san pham
public class ProductManager
{
    private List<Product> products = new List<Product>();

    // Them san pham
    public void AddProduct(Product product)
    {
        products.Add(product);
        Console.WriteLine($"Product {product.Name} added.");
    }

    // Sua san pham
    public void UpdateProduct(string name, Product newProduct)
    {
        var product = products.FirstOrDefault(p => p.Name == name);
        if (product != null)
        {
            product.Name = newProduct.Name;
            product.Price = newProduct.Price;
            product.Stock = newProduct.Stock;
            Console.WriteLine($"Product {name} updated.");
        }
        else
        {
            Console.WriteLine($"Product {name} not found.");
        }
    }

    // Xoa san pham
    public void DeleteProduct(string name)
    {
        var product = products.FirstOrDefault(p => p.Name == name);
        if (product != null)
        {
            products.Remove(product);
            Console.WriteLine($"Product {name} deleted.");
        }
        else
        {
            Console.WriteLine($"Product {name} not found.");
        }
    }

    // Hien thi thong tin tat ca san pham
    public void DisplayAllProducts()
    {
        foreach (var product in products)
        {
            product.DisplayProductInfo();
        }
    }

    // Lay loai san pham
    public Type GetProductType(string name)
    {
        var product = products.FirstOrDefault(p => p.Name == name);
        return product?.GetType();
    }
}

// Chuong trinh chinh
class Program
{
    static void Main(string[] args)
    {
        ProductManager manager = new ProductManager();
        bool keepGoing = true;

        while (keepGoing)
        {
            Console.WriteLine("Chon loai san pham muon nhap:");
            Console.WriteLine("1. MobilePhone");
            Console.WriteLine("2. Laptop");
            Console.WriteLine("3. Accessory");
            Console.WriteLine("4. Thoat");
            Console.Write("Lua chon: ");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
            {
                Console.WriteLine("Lua chon khong hop le. Vui long chon lai.");
            }

            if (choice == 4)
            {
                break;
            }

            string name;
            decimal price;
            int stock;

            while (true)
            {
                try
                {
                    Console.Write("Ten san pham: ");
                    name = Console.ReadLine();
                    Console.Write("Gia san pham: ");
                    price = decimal.Parse(Console.ReadLine());
                    Console.Write("So luong ton kho: ");
                    stock = int.Parse(Console.ReadLine());
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Du lieu nhap vao khong hop le. Vui long nhap lai.");
                }
            }

            switch (choice)
            {
                case 1:
                    Console.Write("He dieu hanh: ");
                    string os = Console.ReadLine();
                    manager.AddProduct(new MobilePhone(name, price, stock, os));
                    break;
                case 2:
                    Console.Write("Bo xu ly: ");
                    string processor = Console.ReadLine();
                    manager.AddProduct(new Laptop(name, price, stock, processor));
                    break;
                case 3:
                    Console.Write("Loai phu kien: ");
                    string type = Console.ReadLine();
                    manager.AddProduct(new Accessory(name, price, stock, type));
                    break;
            }

            while (true)
            {
                Console.WriteLine("Lua chon tiep theo:");
                Console.WriteLine("1. Nhap san pham tiep");
                Console.WriteLine("2. Sua san pham");
                Console.WriteLine("3. Hoan thanh");
                Console.Write("Lua chon: ");
                int nextChoice;
                while (!int.TryParse(Console.ReadLine(), out nextChoice) || nextChoice < 1 || nextChoice > 3)
                {
                    Console.WriteLine("Lua chon khong hop le. Vui long chon lai.");
                }

                if (nextChoice == 1)
                {
                    break; // Quay lai vong lap ngoai de nhap san pham tiep
                }
                else if (nextChoice == 2)
                {
                    Console.Write("Nhap ten san pham can sua: ");
                    string productName = Console.ReadLine();
                    Console.Write("Ten san pham moi: ");
                    string newName = Console.ReadLine();
                    Console.Write("Gia san pham moi: ");
                    decimal newPrice;
                    while (!decimal.TryParse(Console.ReadLine(), out newPrice))
                    {
                        Console.WriteLine("Gia san pham khong hop le. Vui long nhap lai.");
                    }
                    Console.Write("So luong ton kho moi: ");
                    int newStock;
                    while (!int.TryParse(Console.ReadLine(), out newStock))
                    {
                        Console.WriteLine("So luong ton kho khong hop le. Vui long nhap lai.");
                    }
                    Product newProduct;
                    if (manager.GetProductType(productName) == typeof(MobilePhone))
                    {
                        Console.Write("He dieu hanh moi: ");
                        string newOS = Console.ReadLine();
                        newProduct = new MobilePhone(newName, newPrice, newStock, newOS);
                    }
                    else if (manager.GetProductType(productName) == typeof(Laptop))
                    {
                        Console.Write("Bo xu ly moi: ");
                        string newProcessor = Console.ReadLine();
                        newProduct = new Laptop(newName, newPrice, newStock, newProcessor);
                    }
                    else
                    {
                        Console.Write("Loai phu kien moi: ");
                        string newType = Console.ReadLine();
                        newProduct = new Accessory(newName, newPrice, newStock, newType);
                    }
                    manager.UpdateProduct(productName, newProduct);
                    manager.DisplayAllProducts();
                }
                else if (nextChoice == 3)
                {
                    manager.DisplayAllProducts();
                    keepGoing = false;
                    break;
                }
            }
        }
    }
}
