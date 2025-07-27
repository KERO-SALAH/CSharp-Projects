using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniWarehouse_Transient
{
    class Program
    {
        static List<string> productCodes = new List<string>();
        static List<string> productNames = new List<string>();
        static List<int> productQuantities = new List<int>();
        static List<decimal> productPrices = new List<decimal>();

        private const string AdminUsername = "ENG : KE_RO";
        private const string AdminPassword = "12345678";

        static void Main()
        {
            if (!Login())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n[INFO] Exiting application due to login cancellation or failure.");
                Console.ResetColor();
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("┌══════════════════════════════┐");
                Console.WriteLine("│       Mini Warehouse System  │");
                Console.WriteLine("└══════════════════════════════┘");
                Console.WriteLine();
                Console.WriteLine("-> 1. Add Product");
                Console.WriteLine("-> 2. Show All Products");
                Console.WriteLine("-> 3. Search Product");
                Console.WriteLine("-> 4. Update Product");
                Console.WriteLine("-> 5. Increase Quantity");
                Console.WriteLine("-> 6. Delete Product");
                Console.WriteLine("-> 7. Sell Product");
                Console.WriteLine("-> 8. Exit");
                Console.WriteLine("───────────────────────────────");
                Console.ResetColor();

                Console.Write("Select option: ");
                string option = Console.ReadLine().Trim();

                switch (option)
                {
                    case "1":
                        AddProduct();
                        break;
                    case "2":
                        ShowProducts();
                        break;
                    case "3":
                        SearchProduct();
                        break;
                    case "4":
                        UpdateProduct();
                        break;
                    case "5":
                        IncreaseQuantity();
                        break;
                    case "6":
                        DeleteProduct();
                        break;
                    case "7":
                        SellProduct();
                        break;
                    case "8":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n[DONE] Exiting Mini Warehouse System...");
                        Console.ResetColor();
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n[ERROR] Invalid option. Please select a number from 1 to 8.");
                        Console.ResetColor();
                        Pause();
                        break;
                }
            }
        }

        static bool Login()
        {
            const int maxAttempts = 3;
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("┌══════════════════════════════┐");
                Console.WriteLine("│       Warehouse Login        │");
                Console.WriteLine("└══════════════════════════════┘");
                Console.ResetColor();

                Console.Write("Enter Username: ");
                string username = Console.ReadLine();
                Console.Write("Enter Password: ");
                string password = Console.ReadLine();

                if (username == AdminUsername && password == AdminPassword)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n[DONE] Login successful!");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue to the main menu...");
                    Console.ReadKey();
                    return true;
                }
                else
                {
                    attempts++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n[ERROR] Invalid username or password. Attempts left: {maxAttempts - attempts}");
                    Console.ResetColor();
                    Pause();
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n[ERROR] Too many failed login attempts. Application will exit.");
            Console.ResetColor();
            Pause();
            return false;
        }


        static void AddProduct()
        {
            Console.Clear();
            Console.WriteLine("┌───────────────── Add Product ────────────────┐");
            Console.Write("│ Enter product code: ");
            string code = Console.ReadLine().Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(code))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("│ [ERROR] Product code cannot be empty.");
                Console.ResetColor();
                Pause();
                return;
            }

            if (productCodes.Contains(code))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("│ [ERROR] Product with this code already exists.");
                Console.ResetColor();
                Pause();
                return;
            }

            Console.Write("│ Enter product name: ");
            string name = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("│ [ERROR] Product name cannot be empty.");
                Console.ResetColor();
                Pause();
                return;
            }

            Console.Write("│ Enter quantity: ");
            if (!int.TryParse(Console.ReadLine().Trim(), out int qty) || qty < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("│ [ERROR] Invalid quantity. Please enter a non-negative number.");
                Console.ResetColor();
                Pause();
                return;
            }

            Console.Write("│ Enter price: ");
            if (!decimal.TryParse(Console.ReadLine().Trim(), out decimal price) || price < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("│ [ERROR] Invalid price. Please enter a non-negative number.");
                Console.ResetColor();
                Pause();
                return;
            }

            productCodes.Add(code);
            productNames.Add(name);
            productQuantities.Add(qty);
            productPrices.Add(price);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("│ [DONE] Product added successfully.");
            Console.ResetColor();
            Pause();
        }

        static void ShowProducts()
        {
            Console.Clear();
            Console.WriteLine("┌───────────────── All Products ───────────────┐");
            if (productCodes.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("│ [INFO] No products found in the warehouse.");
                Console.ResetColor();
                Pause();
                return;
            }

            PrintTableHeaders();
            for (int i = 0; i < productCodes.Count; i++)
            {
                Console.WriteLine($"│ {productCodes[i],-10} │ {productNames[i],-20} │ {productQuantities[i],-10} │ {productPrices[i],-10:C} │");
            }
            PrintTableFooter();
            Pause();
        }

        static void SearchProduct()
        {
            Console.Clear();
            Console.WriteLine("┌───────────────── Search Product ─────────────┐");
            Console.Write("│ Enter product code to search: ");
            string code = Console.ReadLine().Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(code))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("│ [ERROR] Product code cannot be empty.");
                Console.ResetColor();
                Pause();
                return;
            }

            int index = FindProductIndex(code);
            if (index != -1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("│ [DONE] Product found:");
                Console.ResetColor();
                PrintTableHeaders();
                Console.WriteLine($"│ {productCodes[index],-10} │ {productNames[index],-20} │ {productQuantities[index],-10} │ {productPrices[index],-10:C} │");
                PrintTableFooter();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("│ [ERROR] Product not found.");
                Console.ResetColor();
            }
            Pause();
        }

        static void UpdateProduct()
        {
            Console.Clear();
            ShowProducts();
            Console.WriteLine("┌───────────────── Update Product ─────────────┐");
            Console.Write("│ Enter product code to update: ");
            string code = Console.ReadLine().Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(code))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("│ [ERROR] Product code cannot be empty.");
                Console.ResetColor();
                Pause();
                return;
            }

            int index = FindProductIndex(code);
            if (index == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("│ [ERROR] Product not found.");
                Console.ResetColor();
                Pause();
                return;
            }

            Console.WriteLine($"│ Current details for Code: {productCodes[index]}, Name: {productNames[index]}, Quantity: {productQuantities[index]}, Price: {productPrices[index]:C}");
            Console.Write("│ Enter new name (leave blank to keep current): ");
            string newName = Console.ReadLine().Trim();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                productNames[index] = newName;
            }

            Console.Write("│ Enter new quantity (leave blank to keep current): ");
            string qtyInput = Console.ReadLine().Trim();
            if (int.TryParse(qtyInput, out int newQty) && newQty >= 0)
            {
                productQuantities[index] = newQty;
            }
            else if (!string.IsNullOrWhiteSpace(qtyInput))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("│ [ERROR] Invalid quantity entered. Keeping current quantity.");
                Console.ResetColor();
            }

            Console.Write("│ Enter new price (leave blank to keep current): ");
            string priceInput = Console.ReadLine().Trim();
            if (decimal.TryParse(priceInput, out decimal newPrice) && newPrice >= 0)
            {
                productPrices[index] = newPrice;
            }
            else if (!string.IsNullOrWhiteSpace(priceInput))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("│ [ERROR] Invalid price entered. Keeping current price.");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("│ [DONE] Product updated successfully.");
            Console.WriteLine("│ Updated details:");
            Console.ResetColor();
            PrintTableHeaders();
            Console.WriteLine($"│ {productCodes[index],-10} │ {productNames[index],-20} │ {productQuantities[index],-10} │ {productPrices[index],-10:C} │");
            PrintTableFooter();
            Pause();
        }

        static void IncreaseQuantity()
        {
            Console.Clear();
            ShowProducts();
            Console.WriteLine("┌───────────── Increase Quantity ─────────────┐");
            Console.Write("│ Enter product code to increase quantity: ");
            string code = Console.ReadLine().Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(code))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("│ [ERROR] Product code cannot be empty.");
                Console.ResetColor();
                Pause();
                return;
            }

            int index = FindProductIndex(code);
            if (index == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("│ [ERROR] Product not found.");
                Console.ResetColor();
                Pause();
                return;
            }

            Console.Write($"│ Enter amount to add (current: {productQuantities[index]}): ");
            if (!int.TryParse(Console.ReadLine().Trim(), out int amountToAdd) || amountToAdd <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("│ [ERROR] Invalid quantity. Please enter a positive number.");
                Console.ResetColor();
                Pause();
                return;
            }

            productQuantities[index] += amountToAdd;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("│ [DONE] Quantity updated successfully.");
            Console.WriteLine("│ Updated details:");
            Console.ResetColor();
            PrintTableHeaders();
            Console.WriteLine($"│ {productCodes[index],-10} │ {productNames[index],-20} │ {productQuantities[index],-10} │ {productPrices[index],-10:C} │");
            PrintTableFooter();
            Console.ResetColor();
            Pause();
        }

        static void DeleteProduct()
        {
            Console.Clear();
            ShowProducts();
            Console.WriteLine("┌──────────────── Delete Product ─────────────┐");
            Console.Write("│ Enter product code to delete: ");
            string code = Console.ReadLine().Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(code))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("│ [ERROR] Product code cannot be empty.");
                Console.ResetColor();
                Pause();
                return;
            }

            int index = FindProductIndex(code);
            if (index != -1)
            {
                productCodes.RemoveAt(index);
                productNames.RemoveAt(index);
                productQuantities.RemoveAt(index);
                productPrices.RemoveAt(index);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("│ [DONE] Product deleted successfully.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("│ [ERROR] Product not found.");
                Console.ResetColor();
            }
            Pause();
        }

        static void SellProduct()
        {
            Console.Clear();
            ShowProducts();
            Console.WriteLine("┌───────────────── Sell Product ───────────────┐");
            Console.Write("│ Enter product code to sell: ");
            string code = Console.ReadLine().Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(code))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("│ [ERROR] Product code cannot be empty.");
                Console.ResetColor();
                Pause();
                return;
            }

            int index = FindProductIndex(code);
            if (index == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("│ [ERROR] Product not found.");
                Console.ResetColor();
                Pause();
                return;
            }

            Console.Write($"│ Enter quantity to sell (available: {productQuantities[index]}): ");
            if (!int.TryParse(Console.ReadLine().Trim(), out int quantityToSell) || quantityToSell <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("│ [ERROR] Invalid quantity. Please enter a positive number.");
                Console.ResetColor();
                Pause();
                return;
            }

            if (quantityToSell > productQuantities[index])
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("│ [ERROR] Insufficient quantity in stock.");
                Console.ResetColor();
                Pause();
                return;
            }

            productQuantities[index] -= quantityToSell;
            decimal totalSaleValue = quantityToSell * productPrices[index];
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"│ [DONE] Product sold successfully. Total sale value: {totalSaleValue:C}");
            Console.WriteLine("│ Updated details:");
            Console.ResetColor();
            PrintTableHeaders();
            Console.WriteLine($"│ {productCodes[index],-10} │ {productNames[index],-20} │ {productQuantities[index],-10} │ {productPrices[index],-10:C} │");
            PrintTableFooter();
            if (productQuantities[index] == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("│ [WARNING] Product quantity reached zero. Consider deleting it from the warehouse.");
                Console.ResetColor();
            }
            Console.ResetColor();
            Pause();
        }

        static int FindProductIndex(string code)
        {
            return productCodes.FindIndex(pCode => pCode == code.ToUpper());
        }

        static void PrintTableHeaders()
        {
            Console.WriteLine("┬────────────┬──────────────────────┬────────────┬────────────┬");
            Console.WriteLine("│ Code       │ Name                 │ Quantity   │ Price      │");
            Console.WriteLine("├────────────┼──────────────────────┼────────────┼────────────┤");
        }

        static void PrintTableFooter()
        {
            Console.WriteLine("┴────────────┴──────────────────────┴────────────┴────────────┴");
        }

        static void Pause()
        {
            Console.WriteLine("└──────────────────────────────────────────────┘");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }
    }
}