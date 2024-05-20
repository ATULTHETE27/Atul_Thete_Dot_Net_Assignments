using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System
{
    public class Item
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        public Item(int id, string name, decimal price, int quantity)
        {
            ID = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public void UpdateName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name;
            }
        }

        public void UpdatePrice(decimal price)
        {
            if (price >= 0)
            {
                Price = price;
            }
        }

        public void UpdateQuantity(int quantity)
        {
            if (quantity >= 0)
            {
                Quantity = quantity;
            }
        }

        public override string ToString()
        {
            return $"ID: {ID}, Name: {Name}, Price: {Price}, Quantity: {Quantity}";
        }
    }
}