using System;
using System.Collections.Generic;

namespace TelegramBot.Models
{
    public partial class Product
    {
        public Product()
        {
            ListProducts = new HashSet<ListProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<ListProduct> ListProducts { get; set; }
    }
}
