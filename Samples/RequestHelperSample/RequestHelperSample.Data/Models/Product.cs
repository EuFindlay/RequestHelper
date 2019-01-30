using System;
using System.Collections.Generic;
using System.Text;

namespace RequestHelperSample.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public int ManufacturerId { get; set; }
    }
}
