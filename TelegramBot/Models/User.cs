using System;
using System.Collections.Generic;

namespace TelegramBot.Models
{
    public partial class User
    {
        public User()
        {
            ListProducts = new HashSet<ListProduct>();
        }

        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Parol { get; set; }

        public virtual ICollection<ListProduct> ListProducts { get; set; }
    }
}
