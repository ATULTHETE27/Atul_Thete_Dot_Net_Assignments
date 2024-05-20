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
                Console.WriteLine();
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

        public void UpdateItem(int id, string newName, string newPriceInput, string newQuantityInput)
        {
            var item = FindItemByID(id);
            if (item != null)
            {
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    item.UpdateName(newName);
                }
                if (decimal.TryParse(newPriceInput, out decimal newPrice))
                {
                    item.UpdatePrice(newPrice);
                }
                if (int.TryParse(newQuantityInput, out int newQuantity))
                {
                    item.UpdateQuantity(newQuantity);
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