using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System
{
    public class Inventory
    {
        private List<Item> items;
        private int nextId;

        public Inventory()
        {
            items = new List<Item>();
            nextId = 1; 
        }

        public void AddItem(string name, decimal price, int quantity)
        {
            Item item = new Item(nextId, name, price, quantity);
            items.Add(item);
            nextId++;
            Console.WriteLine("\nItem added successfully.");
        }

        public void DisplayAllItems()
        {
            if (items.Count == 0)
            {
                Console.WriteLine("\nNo items in inventory.");
            }
            else
            {
                foreach (var item in items)
                {
                    Console.WriteLine(item);
                }
            }
        }

        public Item FindItemByID(int id)
        {
            return items.Find(item => item.ID == id);
        }

        public void UpdateItem(int id, string newName, decimal newPrice, int newQuantity)
        {
            var item = FindItemByID(id);
            if (item != null)
            {
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    item.Name = newName;
                }
                if (newPrice >= 0)
                {
                    item.Price = newPrice;
                }
                if (newQuantity >= 0)
                {
                    item.Quantity = newQuantity;
                }
                Console.WriteLine("\nItem updated successfully.");
            }
            else
            {
                Console.WriteLine("\nItem not found.");
            }
        }

        public void DeleteItem(int id)
        {
            var item = FindItemByID(id);
            if (item != null)
            {
                items.Remove(item);
                Console.WriteLine("\nItem deleted successfully.");
            }
            else
            {
                Console.WriteLine("\nItem not found.");
            }
        }
    }

}