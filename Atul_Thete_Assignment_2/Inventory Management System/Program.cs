using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Inventory inventory = new Inventory();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nInventory Management System");
                Console.WriteLine("\n1. Add Item");
                Console.WriteLine("2. Display All Items");
                Console.WriteLine("3. Find Item by ID");
                Console.WriteLine("4. Update Item");
                Console.WriteLine("5. Delete Item");
                Console.WriteLine("6. Exit");
                Console.Write("\nChoose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddNewItem(inventory);
                        break;
                    case "2":
                        inventory.DisplayAllItems();
                        break;
                    case "3":
                        FindItemByID(inventory);
                        break;
                    case "4":
                        UpdateExistingItem(inventory);
                        break;
                    case "5":
                        DeleteItem(inventory);
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("\nInvalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddNewItem(Inventory inventory)
        {
            Console.Write("\nEnter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            inventory.AddItem(name, price, quantity);
        }

        static void FindItemByID(Inventory inventory)
        {
            Console.Write("\nEnter ID: ");
            int id = int.Parse(Console.ReadLine());

            Item item = inventory.FindItemByID(id);
            if (item != null)
            {
                Console.WriteLine(item);
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
        }

        static void UpdateExistingItem(Inventory inventory)
        {
            Console.Write("\nEnter ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter New Name (leave blank to keep current): ");
            string newName = Console.ReadLine();
            Console.Write("Enter New Price (leave blank to keep current): ");
            string newPriceInput = Console.ReadLine();
            Console.Write("Enter New Quantity (leave blank to keep current): ");
            string newQuantityInput = Console.ReadLine();

            string currentName = newName;
            decimal newPrice = -1;
            int newQuantity = -1;

            if (!string.IsNullOrWhiteSpace(newPriceInput))
            {
                newPrice = decimal.Parse(newPriceInput);
            }
            if (!string.IsNullOrWhiteSpace(newQuantityInput))
            {
                newQuantity = int.Parse(newQuantityInput);
            }

            inventory.UpdateItem(id, currentName, newPrice, newQuantity);
        }

        static void DeleteItem(Inventory inventory)
        {
            Console.Write("\nEnter ID: ");
            int id = int.Parse(Console.ReadLine());

            inventory.DeleteItem(id);
        }
    }

}