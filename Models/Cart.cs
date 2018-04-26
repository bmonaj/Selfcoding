using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace selfCoding.Models
{
    public class Cart
    {
        public Cart()
        {
            Items = new List<CartItem>();
        }

        public int Id { get; set; }
        public List<CartItem> Items { get; set; }
    }
}