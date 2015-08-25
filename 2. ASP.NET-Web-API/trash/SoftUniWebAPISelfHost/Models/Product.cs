using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftUniWebAPISelfHost
{
    public class Product
    {
        public Product(string name)
        {
            this.Id = 0;
            this.Name = name;
        }

        public int Id;

        public string Name;

        public double Price;
    }
}